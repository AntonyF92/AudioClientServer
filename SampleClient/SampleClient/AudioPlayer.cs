﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System.IO;
using System.Threading;
using System.Net.Sockets;

namespace SampleClient
{
    public class AudioPlayer
    {
        private WaveOut player = null;
        WaveStream blockAlignedStream = null;
        public PlaylistManager playlistManager { get; private set; }
        private Playlist currentPlaylist = null;
        private NetworkFileInfo currentFile = null;
        private Mp3FileReader mp3Reader = null;
        TcpClient client = new TcpClient();
        AutoResetEvent triggerWait = new AutoResetEvent(false);
        Thread triggerThread;
        bool manuallyStopped = false;

        public AudioPlayer()
        {
            //client.Connect(ConfigManager.Instance.config.audio_server_dns, ConfigManager.Instance.config.audio_port);
            playlistManager = new PlaylistManager((pl, file) => Play(file, pl));
            //triggerThread = new Thread(TriggerNextTrack);
            //triggerThread.Start();
        }

        public void Play(NetworkFileInfo fi, Playlist pl)
        {
            ThreadPool.QueueUserWorkItem(o =>
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
                    while (!client.GetStream().DataAvailable)
                    {
                        Thread.Sleep(10);
                    }
                    currentFile = fi;
                    currentPlaylist = pl;
                    PlayMp3FromStream(client.GetStream());
                    //client.Close();
                }
            });
        }

        private void PlayMp3FromStream(Stream stream)
        {
            Stream ms = new MemoryStream();
            new Thread(delegate(object o)
            {
                try
                {
                    byte[] buffer = new byte[ConfigManager.Instance.config.audio_buffer_size]; // 64KB chunks
                    int read;
                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        var pos = ms.Position;
                        ms.Position = ms.Length;
                        ms.Write(buffer, 0, read);
                        ms.Position = pos;
                    }
                }
                catch { }
            }).Start();

            // Pre-buffering some data to allow NAudio to start playing
            while (ms.Length < 65536 * 2)
                Thread.Sleep(100);

            ms.Position = 0;
            mp3Reader = new Mp3FileReader(ms);
            blockAlignedStream = new BlockAlignReductionStream(WaveFormatConversionStream.CreatePcmStream(mp3Reader));
            player = new WaveOut(WaveCallbackInfo.FunctionCallback());
            player.PlaybackStopped += player_PlaybackStopped;
            player.Init(blockAlignedStream);
            player.Play();
            /*while (player.PlaybackState != PlaybackState.Stopped)
            {
                System.Threading.Thread.Sleep(100);
            }
            blockAlignedStream.Dispose();*/
            //NextTrack();
        }

        void player_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (blockAlignedStream != null)
                blockAlignedStream.Dispose();
            blockAlignedStream = null;
            if (mp3Reader != null)
                mp3Reader.Close();
            mp3Reader = null;
            /*if (player != null)
                player.Dispose();
            player = null;*/
            client.Close();
            if (!manuallyStopped)
                NextTrack();
        }

        void TriggerNextTrack()
        {
            while (true)
            {
                triggerWait.WaitOne();
                NextTrack();
            }
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
            NetworkFileInfo file = null;
            if (currentFile != null && currentPlaylist != null)
            {
                int index = currentPlaylist.FileList.IndexOf(currentFile);
                if (index != -1)
                {
                    if (index < currentPlaylist.FileList.Count - 1)
                        file = currentPlaylist.FileList[index + 1];
                    else
                        file = currentPlaylist.FileList[0];
                    playlistManager.ChangeTrack(currentPlaylist, file);
                    Play(file, currentPlaylist);
                }
            }
        }

        public void PreviousTrack()
        {
            NetworkFileInfo file = null;
            if (currentFile != null && currentPlaylist != null)
            {
                int index = currentPlaylist.FileList.IndexOf(currentFile);
                if (index != -1)
                {
                    if (index > 0)
                        file = currentPlaylist.FileList[index - 1];
                    else
                        file = currentPlaylist.FileList[currentPlaylist.FileList.Count - 1];
                    playlistManager.ChangeTrack(currentPlaylist, file);
                    Play(file, currentPlaylist);
                }
            }
        }

        public void Dispose()
        {
            try
            {
                manuallyStopped = true;
                StopPlayer();
                client.Close();
                triggerThread.Abort();
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
