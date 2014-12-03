﻿using System;
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
        private WaveOut player = null;
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

        bool randomOrder = false;
        Random rnd = new Random();

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
            serviceTimer = new Timer(TimerTick, null, 0, timerInterval);
        }

        void TimerTick(object state)
        {
            if (player != null && player.PlaybackState == PlaybackState.Playing && PlaybackProgressChangeEvent != null)
                PlaybackProgressChangeEvent(blockAlignedStream.CurrentTime, TimeSpan.FromSeconds(currentFile.length), blockAlignedStream.Position);
        }

        public void Play(AudioFileInfo fi, Playlist pl)
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                try
                {
                    if (player != null && currentFile == fi && currentPlaylist == pl && player.PlaybackState == PlaybackState.Paused)
                        player.Resume();
                    else
                    {
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
                        if (currentPlaylist != pl)
                            history.Clear();
                        if (currentFile != null)
                            history.Push(currentFile);
                        currentFile = fi;
                        currentPlaylist = pl;
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
            });
        }

        public void SetPosition(long value)
        {
            try
            {
                if (value % blockAlignedStream.WaveFormat.BlockAlign != 0)
                    value -= value % blockAlignedStream.WaveFormat.BlockAlign;
                value = Math.Max(0, Math.Min(currentFileLength/bytesPerSecond*blockAlignedStream.WaveFormat.AverageBytesPerSecond, value));
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
                Stream ms = new MemoryStream();
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
                while (ms.Length < 65536 * 2)
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
                player = new WaveOut(WaveCallbackInfo.FunctionCallback());
                player.PlaybackStopped += player_PlaybackStopped;
                player.Init(blockAlignedStream);
                player.Play();
                if (PlaybackStartEvent != null)
                    PlaybackStartEvent(TimeSpan.FromSeconds(currentFile.length), (long)(currentFile.length * blockAlignedStream.WaveFormat.AverageBytesPerSecond), currentFile);
            }
            catch (Exception ex)
            {
                if (OnExceptionEvent != null)
                    OnExceptionEvent(ex);
            }
        }

        public void CloseConnection()
        {
            if (player != null && player.PlaybackState == PlaybackState.Playing)
                player.Stop();
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
            if (player != null && player.PlaybackState == PlaybackState.Playing)
                player.Stop();
        }

        public void StopPlayer()
        {
            if (player != null && player.PlaybackState == PlaybackState.Playing)
            {
                manuallyStopped = true;
                player.Stop();
                manuallyStopped = false;
            }
        }

        public void PausePlayer()
        {
            if (player != null && player.PlaybackState == PlaybackState.Playing)
                player.Pause();
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
            if (player != null)
                player.Volume = value;
        }
    }
}
