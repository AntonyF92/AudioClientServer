using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using AxWMPLib;
using PlaylistControls;

namespace AudioPlayer
{
    class AudioPlayer
    {
        enum PlaybackState
        {
            stopped,
            playing,
            paused
        }

        AxWindowsMediaPlayer wmp = null;
        public PlaylistManager playlistManager;
        private Playlist currentPlaylist = null;
        private AudioFileInfo currentFile = null;
        private Stack<AudioFileInfo> history = new Stack<AudioFileInfo>();
        private bool randomOrder = false;
        private Random rnd = new Random();
        private bool repeat = false;
        private Timer serviceTimer;
        private PlaybackState currentState = PlaybackState.stopped;
        private bool isBusy = false;

        public delegate void StopAndClearHandler(AudioFileInfo file);
        public event StopAndClearHandler OnStopAndClear = null;
        public delegate void PlaybackStartHandler(AudioFileInfo file);
        public event PlaybackStartHandler OnPlaybackStart = null;
        public delegate void PlaybackStopHandler(AudioFileInfo file);
        public event PlaybackStopHandler OnPlaybackStop = null;
        public delegate void PlaybackProgressHandler(double position);
        public event PlaybackProgressHandler PlaybackProgressChanged = null;
        public delegate void ExceptionHandler(Exception ex);
        public event ExceptionHandler OnException = null;

        public AudioPlayer()
        {
            this.wmp = new AxWindowsMediaPlayer();
            playlistManager = new PlaylistManager();
            serviceTimer = new Timer(timerTick, null, 0, 100);
            wmp.PlayStateChange += Wmp_PlayStateChange;
        }

        private void Wmp_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == (int)WMPLib.WMPPlayState.wmppsStopped && currentState == PlaybackState.playing)
                NextTrack();
        }

        void timerTick(object state)
        {
            if (isBusy)
                return;
            isBusy = true;
            try
            {
                if (currentState == PlaybackState.playing)
                {
                    if (PlaybackProgressChanged != null)
                        PlaybackProgressChanged(wmp.Ctlcontrols.currentPosition);
                }
            }
            catch (Exception ex)
            {

            }
            isBusy = false;
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
                    if (history.Count > 0 && history.Peek() != currentFile)
                        history.Push(currentFile);
                    playlistManager.ChangeTrack(currentPlaylist, file);
                    Stop();
                    Play(file, currentPlaylist);
                }
            }
        }

        void StopAndClear()
        {
            currentState = PlaybackState.stopped;
            wmp.Ctlcontrols.stop();
            currentPlaylist = null;
            if (OnStopAndClear != null)
                OnStopAndClear(currentFile);
            currentFile = null;
        }

        public void Stop()
        {
            currentState = PlaybackState.stopped;
            wmp.Ctlcontrols.stop();
            if (OnPlaybackStop != null)
                OnPlaybackStop(currentFile);
        }

        public void Pause()
        {
            currentState = PlaybackState.paused;
            wmp.Ctlcontrols.pause();
        }

        public void Play(AudioFileInfo file, Playlist pl)
        {
            StopAndClear();
            wmp.URL = file.path;
            currentState = PlaybackState.playing;
            wmp.Ctlcontrols.play();
            currentFile = file;
            currentPlaylist = pl;
            if (OnPlaybackStart != null)
                OnPlaybackStart(currentFile);
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
                    Stop();
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
                StopAndClear();
            }
            catch { }
        }

        public void SetVolume(int value)
        {
            wmp.settings.volume = value;
        }

        public void SetProgress(double value)
        {
            wmp.Ctlcontrols.currentPosition = value;
        }
    }
}
