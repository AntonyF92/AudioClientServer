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
            /*if (waveOut != null && waveOut.PlaybackState == PlaybackState.Playing && PlaybackProgressChangeEvent != null)
                PlaybackProgressChangeEvent(, TimeSpan.FromSeconds(currentFile.length), blockAlignedStream.Position);
            if (waveOut != null && blockAlignedStream != null && currentFile != null && waveOut.PlaybackState == PlaybackState.Playing && blockAlignedStream.CurrentTime.TotalSeconds >= currentFile.length)
                waveOut.Stop();*/


            if (playbackState != StreamingPlaybackState.Stopped)
            {
                if (waveOut == null && bufferedWaveProvider != null)
                {
                    waveOut = new WaveOut();
                    waveOut.PlaybackStopped += player_PlaybackStopped;
                    volumeProvider = new VolumeWaveProvider16(bufferedWaveProvider);
                    waveOut.Init(volumeProvider);
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
                    }
                }

            }

        }

        public void PlayFile(AudioFileInfo fi, Playlist pl)
        {
            if (currentPlaylist != null && currentPlaylist.Name != pl.Name)
                history.Clear();
            history.Push(fi);
            currentPlaylist = pl;
            if (playbackState == StreamingPlaybackState.Playing)
                StopPlayback();
            Play(fi, pl);

        }

        private void Play(AudioFileInfo fi, Playlist pl)
        {
            try
            {

                if (playbackState == StreamingPlaybackState.Stopped)
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
                        NextTrack();                    
                }
                else if (waveOut != null && currentFile == fi && currentPlaylist == pl&&playbackState == StreamingPlaybackState.Paused)
                {
                    playbackState = StreamingPlaybackState.Buffering;
                }



                else
                {
                    if (waveOut != null && waveOut.PlaybackState != PlaybackState.Stopped)
                        StopPlayer();
                    client = new TcpClient();
                    client.Connect(ConfigManager.Instance.config.audio_server_dns, ConfigManager.Instance.config.audio_port);
                    var stream = client.GetStream();
                    byte[] data = Encoding.UTF8.GetBytes(fi.path);
                    stream.Write(data, 0, data.Length);
                    //stream.Flush();
                    /*while (!stream.DataAvailable)
                    {
                        Thread.Sleep(10);
                    }*/

                    currentFile = fi;
                    byte[] res = new byte[1];
                    stream.Read(res, 0, 1);
                    if (Convert.ToBoolean(res[0]))
                        PlayMp3FromStream(stream);
                    else
                        NextTrack();
                    //client.Close();
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

        private void PlayMp3FromStream(Stream stream)
        {
            try
            {
                currentFileLength = 0;
                MemoryStream ms = new MemoryStream();
                new Thread(delegate(object o)
                {
                    try
                    {
                        byte[] buffer = new byte[ConfigManager.Instance.config.audio_buffer_size]; // 64KB chunks
                        int read;
                        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            currentFileLength += read;
                            var pos = ms.Position;
                            ms.Position = ms.Length;
                            ms.Write(buffer, 0, read);
                            ms.Position = pos;
                        }
                    }
                    catch { }
                }).Start();

                bytesPerSecond = (int)(currentFile.size / currentFile.length);
                // Pre-buffering some data to allow NAudio to start playing
                while (ms.Length < 65536 * 4)
                    Thread.Sleep(100);
                ms.Position = 0;
                if (currentFile.exstension == "mp3")
                {
                    mp3Reader = new Mp3FileReader(ms);
                    blockAlignedStream = new BlockAlignReductionStream(WaveFormatConversionStream.CreatePcmStream(mp3Reader));
                }
                else
                {
                    waveReader = new WaveFileReader(ms);
                    blockAlignedStream = new BlockAlignReductionStream(new WaveChannel32(waveReader));
                }
                serviceTimer.Change(0, timerInterval);
                waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback());
                waveOut.PlaybackStopped += player_PlaybackStopped;
                waveOut.Init(blockAlignedStream);
                waveOut.Play();
                if (PlaybackStartEvent != null)
                    PlaybackStartEvent(TimeSpan.FromSeconds(currentFile.length), (long)(currentFile.length * blockAlignedStream.WaveFormat.AverageBytesPerSecond), currentFile);
            }
            catch (Exception ex)
            {
                NextTrack();
                if (OnExceptionEvent != null)
                    OnExceptionEvent(ex);
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
            serviceTimer.Change(Timeout.Infinite, timerInterval);
            if (PlaybackStopEvent != null)
                PlaybackStopEvent();
            if (blockAlignedStream != null)
                blockAlignedStream.Dispose();
            blockAlignedStream = null;
            if (mp3Reader != null)
                mp3Reader.Close();
            mp3Reader = null;
            if (waveReader != null)
                waveReader.Dispose();
            waveReader = null;

            /*if (player != null)
                player.Dispose();
            player = null;*/
            client.Close();
            if (!manuallyStopped)
                NextTrack();
        }

        private void Stop()
        {
            if (waveOut != null && waveOut.PlaybackState == PlaybackState.Playing)
                waveOut.Stop();
        }

        public void StopPlayer()
        {
            if (waveOut != null && waveOut.PlaybackState == PlaybackState.Playing)
            {
                manuallyStopped = true;
                waveOut.Stop();
                manuallyStopped = false;
            }
        }

        public void PausePlayer()
        {
            if (waveOut != null && waveOut.PlaybackState == PlaybackState.Playing)
                waveOut.Pause();
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
                    if (history.Peek() != currentFile)
                        history.Push(currentFile);
                    playlistManager.ChangeTrack(currentPlaylist, file);
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
                StopPlayer();
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
                        // reached the end of the MP3 file / stream
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
                        bufferedWaveProvider.BufferDuration = TimeSpan.FromSeconds(20); // allow us to get well ahead of ourselves
                        //this.bufferedWaveProvider.BufferedDuration = 250;
                    }
                    int decompressed = decompressor.DecompressFrame(frame, buffer, 0);
                    //Debug.WriteLine(String.Format("Decompressed a frame {0}", decompressed));
                    bufferedWaveProvider.AddSamples(buffer, 0, decompressed);


                } while (playbackState != StreamingPlaybackState.Stopped);
                // was doing this in a finally block, but for some reason
                // we are hanging on response stream .Dispose so never get there
                decompressor.Dispose();

            }
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

        private void StopPlayback()
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
                serviceTimer.Change(Timeout.Infinite, timerInterval);
            }
        }

        private void Play()
        {
            waveOut.Play();
            playbackState = StreamingPlaybackState.Playing;
        }

        private void Pause()
        {
            playbackState = StreamingPlaybackState.Buffering;
            waveOut.Pause();
        }
    }
}
