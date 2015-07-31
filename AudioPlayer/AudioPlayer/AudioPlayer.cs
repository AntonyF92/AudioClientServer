using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AxWMPLib;
using PlaylistControls;

namespace AudioPlayer
{
    class AudioPlayer
    {
        AxWindowsMediaPlayer wmp = null;
        public PlaylistManager playlistManager;
        private Playlist currentPlaylist = null;
        private AudioFileInfo currentFile = null;

        public AudioPlayer(AxWindowsMediaPlayer wmp)
        {
            this.wmp = wmp;
            playlistManager = new PlaylistManager();
        }

        public void Play()
        {
            
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
    }
}
