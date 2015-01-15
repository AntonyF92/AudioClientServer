using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System.IO;
using System.Threading;
using System.Net.Sockets;
using PlaylistControls;

namespace SampleClient
{
    public class AudioPlayer
    {
        enum StreamingPlaybackState
        {
            Stopped,
            Playing,
            Buffering,
            Paused
        }
        private BufferedWaveProvider bufferedWaveProvider;
        private WaveOut waveOut = null;
        private VolumeWaveProvider16 volumeProvider;
        WaveStream blockAlignedStream = null;
        public PlaylistManager playlistManager { get; private set; }
        private Playlist currentPlaylist = null;
        private AudioFileInfo currentFile = null;
        private Stack<AudioFileInfo> history = new Stack<AudioFileInfo>();
        private Mp3FileReader mp3Reader = null;
        private WaveFileReader waveReader = null;
        TcpClient client = new TcpClient();
        AutoResetEvent triggerWait = new AutoResetEvent(false);
        bool manuallyStopped = false;
        int timerInterval = 100;

        int bytesPerSecond = 0;
        int currentFileLength = 0;

        bool repeat = false;

        bool randomOrder = false;
        Random rnd = new Random();

        private volatile StreamingPlaybackState playbackState;
        private volatile bool fullyDownloaded;

        Timer serviceTimer;
        bool timerIsBusy = false;
        double playbackDuration = 0;
        DateTime lastTime = default(DateTime);
        TimeSpan deltaTime
        {
            get
            {
                return DateTime.Now - lastTime;
            }
        }

        public delegate void ExceptionEventHandler(Exception e);
        public event ExceptionEventHandler OnExceptionEvent;
        public delegate void PlaybackProgressChangeEventHandler(TimeSpan currentTime, TimeSpan totalTime, long position);
        public event PlaybackProgressChangeEventHandler PlaybackProgressChangeEvent;
        public delegate void PlaybackStartEventHandler(TimeSpan totalTime, long length, AudioFileInfo file);
        public event PlaybackStartEventHandler PlaybackStartEvent;
        public delegate void PlaybackStopEventHandler();
        public event PlaybackStopEventHandler PlaybackStopEvent;

        public AudioPlayer()
        {
            //client.Connect(ConfigManager.Instance.config.audio_server_dns, ConfigManager.Instance.config.audio_port);
            playlistManager = new PlaylistManager();
            //triggerThread = new Thread(TriggerNextTrack);
            //triggerThread.Start();
            serviceTimer = new Timer(TimerTick, null, Timeout.Infinite, timerInterval);
        }

        void TimerTick(object state)
        {
            if (!timerIsBusy)
            {
                timerIsBusy = true;
                try
                {
                    if (playbackState != StreamingPlaybackState.Stopped)
                    {
                        if (playbackState == StreamingPlaybackState.Playing)
                        {
                            playbackDuration += deltaTime.TotalMilliseconds;
                            if (PlaybackProgressChangeEvent != null)
                                PlaybackProgressChangeEvent(TimeSpan.FromMilliseconds(playbackDuration), TimeSpan.FromSeconds(currentFile.length), 0);
                        }
                        if (waveOut == null && bufferedWaveProvider != null)
                        {
                            waveOut = new WaveOut();
                            waveOut.PlaybackStopped += player_PlaybackStopped;
                            volumeProvider = new VolumeWaveProvider16(bufferedWaveProvider);
                            waveOut.Init(volumeProvider);
                            if (PlaybackStartEvent != null)
                                PlaybackStartEvent(TimeSpan.FromSeconds(currentFile.length), (long)currentFile.length*1000, currentFile);
                        }
                        else if (bufferedWaveProvider != null)
                        {
                            var bufferedSeconds = bufferedWaveProvider.BufferedDuration.TotalSeconds;
                            
                            //PlaybackProgressChangeEvent(bufferedWaveProvider.WaveFormat., TimeSpan.FromSeconds(currentFile.length), blockAlignedStream.Position);
                            // make it stutter less if we buffer up a decent amount before playing
                            if (bufferedSeconds < 0.5 && playbackState == StreamingPlaybackState.Playing && !fullyDownloaded)
                            {
                                Pause();
                            }
                            else if (bufferedSeconds > 4 && playbackState == StreamingPlaybackState.Buffering)
                            {
                                Play();
                            }
                            else if (fullyDownloaded && bufferedSeconds == 0)
                            {
                                StopPlayback();
                                NextTrack();
                            }
                        }

                    }
                }
                catch
                {
                }
                lastTime = DateTime.Now;
                timerIsBusy = false;
            }

        }

        public void PlayFile(AudioFileInfo fi, Playlist pl)
        {
            if (currentPlaylist != null && currentPlaylist.Name != pl.Name)
                history.Clear();
            if (history.Count==0 || (history.Count > 0 && history.Peek() != fi))
                history.Push(fi);
            currentPlaylist = pl;
            if (playbackState != StreamingPlaybackState.Stopped&&currentFile!=fi)
                StopPlayback();
            Play(fi, pl);

        }

        private void Play(AudioFileInfo fi, Playlist pl)
        {
            try
            {
                if (waveOut != null && currentFile == fi && currentPlaylist == pl&&playbackState == StreamingPlaybackState.Paused)
                {
                    Resume();
                }
                else if (playbackState == StreamingPlaybackState.Stopped)
                {
                    playbackState = StreamingPlaybackState.Buffering;
                    bufferedWaveProvider = null;
                    client = new TcpClient();
                    client.Connect(ConfigManager.Instance.config.audio_server_dns, ConfigManager.Instance.config.audio_port);
                    var stream = client.GetStream();
                    byte[] data = Encoding.UTF8.GetBytes(fi.path);
                    stream.Write(data, 0, data.Length);
                    currentFile = fi;
                    byte[] res = new byte[1];
                    stream.Read(res, 0, 1);
                    if (Convert.ToBoolean(res[0]))
                    {
                        ThreadPool.QueueUserWorkItem(StreamMp3, stream);
                        serviceTimer.Change(0, timerInterval);
                    }
                    else
                    {
                        client.Close();
                        playbackState = StreamingPlaybackState.Stopped;
                        NextTrack();
                    }                 
                }
            }
            catch (Exception ex)
            {
                if (OnExceptionEvent != null)
                    OnExceptionEvent(ex);
            }
        }

        public void SetPosition(long value)
        {
            try
            {
                if (value % blockAlignedStream.WaveFormat.BlockAlign != 0)
                    value -= value % blockAlignedStream.WaveFormat.BlockAlign;
                value = Math.Max(0, Math.Min(currentFileLength / bytesPerSecond * blockAlignedStream.WaveFormat.AverageBytesPerSecond, value));
                blockAlignedStream.Position = value;
            }
            catch (Exception ex)
            {
            }
        }

        public void CloseConnection()
        {
            if (waveOut != null && waveOut.PlaybackState == PlaybackState.Playing)
                waveOut.Stop();
            if (client != null && client.Connected)
            {
                var data = Encoding.UTF8.GetBytes("<EndOfSession>");
                client.GetStream().Write(data, 0, data.Length);
            }
        }

        void player_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (PlaybackStopEvent != null)
                PlaybackStopEvent();
        }

        public void NextTrack()
        {
            AudioFileInfo file = null;
            if (currentFile != null && currentPlaylist != null)
            {
                int index = currentPlaylist.FileList.IndexOf(currentFile);
                if (index != -1)
                {
                    if (randomOrder)
                        index = GetRandomIndex(index);
                    else if (index < currentPlaylist.FileList.Count - 1)
                        index++;
                    else
                        index = 0;
                    file = currentPlaylist.FileList[index];
                    if (history.Count>0 && history.Peek() != currentFile)
                        history.Push(currentFile);
                    playlistManager.ChangeTrack(currentPlaylist, file);
                    StopPlayback();
                    Play(file, currentPlaylist);
                }
            }
        }

        int GetRandomIndex(int currentIndex)
        {
            int res;
            while ((res = rnd.Next(currentPlaylist.FileList.Count)) == currentIndex)
            {
            }
            return res;
        }

        public void PreviousTrack()
        {
            AudioFileInfo file = null;
            if (currentFile != null && currentPlaylist != null)
            {
                int index = currentPlaylist.FileList.IndexOf(currentFile);
                if (index != -1)
                {
                    if (history.Count > 0)
                        file = history.Pop();
                    else if (index > 0)
                        file = currentPlaylist.FileList[index - 1];
                    else
                        file = currentPlaylist.FileList[currentPlaylist.FileList.Count - 1];
                    playlistManager.ChangeTrack(currentPlaylist, file);
                    StopPlayback();
                    Play(file, currentPlaylist);
                }
            }
        }

        public void SetRandomOrder(bool value)
        {
            randomOrder = value;
        }

        public void SetRepeat(bool value)
        {
            repeat = value;
        }

        public void Dispose()
        {
            try
            {
                manuallyStopped = true;
                StopPlayback();
                client.Close();
            }
            catch { }
        }

        public void SetVolume(float value)
        {
            if (waveOut != null)
                waveOut.Volume = value;
        }

        private void StreamMp3(object state)
        {
            fullyDownloaded = false;
            Stream webStream = state as Stream;

            var buffer = new byte[16384 * 4]; // needs to be big enough to hold a decompressed frame

            IMp3FrameDecompressor decompressor = null;
            try
            {
                var readFullyStream = new ReadFullyStream(webStream);
                do
                {
                    Mp3Frame frame;
                    try
                    {
                        frame = Mp3Frame.LoadFromStream(readFullyStream);
                    }
                    catch (EndOfStreamException)
                    {
                        fullyDownloaded = true;
                        break;
                    }
                    catch (Exception)
                    {
                        // probably we have aborted download from the GUI thread
                        break;
                    }
                    if (decompressor == null)
                    {
                        // don't think these details matter too much - just help ACM select the right codec
                        // however, the buffered provider doesn't know what sample rate it is working at
                        // until we have a frame

                        decompressor = CreateFrameDecompressor(frame);
                        bufferedWaveProvider = new BufferedWaveProvider(decompressor.OutputFormat);
                        bufferedWaveProvider.BufferDuration = TimeSpan.FromSeconds(currentFile.length); // allow us to get well ahead of ourselves
                        //this.bufferedWaveProvider.BufferedDuration = 250;
                    }
                    int decompressed = decompressor.DecompressFrame(frame, buffer, 0);
                    //Debug.WriteLine(String.Format("Decompressed a frame {0}", decompressed));
                    //try
                    {
                        bufferedWaveProvider.AddSamples(buffer, 0, decompressed);
                    }
                    //catch { }

                } while (playbackState != StreamingPlaybackState.Stopped);
                // was doing this in a finally block, but for some reason
                // we are hanging on response stream .Dispose so never get there
                decompressor.Dispose();
                if (client != null && client.Connected)
                    client.Close();

            }
            catch { fullyDownloaded = true; }
            finally
            {
                if (decompressor != null)
                {
                    decompressor.Dispose();
                }
            }
        }

        private static IMp3FrameDecompressor CreateFrameDecompressor(Mp3Frame frame)
        {
            WaveFormat waveFormat = new Mp3WaveFormat(frame.SampleRate, frame.ChannelMode == ChannelMode.Mono ? 1 : 2,
                frame.FrameLength, frame.BitRate);
            return new AcmMp3FrameDecompressor(waveFormat);
        }

        public void StopPlayback()
        {
            if (playbackState != StreamingPlaybackState.Stopped)
            {
                playbackState = StreamingPlaybackState.Stopped;
                if (waveOut != null)
                {
                    waveOut.Stop();
                    waveOut.Dispose();
                    waveOut = null;
                }
                if (bufferedWaveProvider != null)
                {
                    bufferedWaveProvider.ClearBuffer();
                    bufferedWaveProvider = null;
                }
                volumeProvider = null;
                playbackDuration = 0;
                serviceTimer.Change(Timeout.Infinite, timerInterval);
                if (client != null && client.Connected)
                    client.Close();
            }
        }

        private void Resume()
        {
            waveOut.Resume();
            playbackState = StreamingPlaybackState.Playing;
        }

        private void Play()
        {
            waveOut.Play();
            playbackState = StreamingPlaybackState.Playing;
        }

        public void Pause()
        {
            playbackState = StreamingPlaybackState.Paused;
            waveOut.Pause();
        }
    }
}
