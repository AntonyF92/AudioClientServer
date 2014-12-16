using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using NAudio;
using System.IO;
using System.Threading;
using NAudio.Wave;
using NAudio.Gui;
using System.Diagnostics;
using PlaylistControls;
using NLog;

namespace SampleClient
{
    public partial class MainForm : Form
    {
        AudioPlayer audioPlayer;
        HttpClient httpClient;
        bool repeat = false;

        Logger log;

        public MainForm()
        {
            InitializeComponent();
            ConfigManager.Init();
            GlobalDiagnosticsContext.Set("ApplicationName", "AudioStreamPlayer");
            log = LogManager.GetCurrentClassLogger();
        }

        void Init()
        {
            try
            {
                if (audioPlayer != null)
                    audioPlayer.Dispose();
                audioPlayer = new AudioPlayer();
                audioPlayer.playlistManager.OnCollectionLoadEvent += playlistManager_OnCollectionLoadEvent;
                audioPlayer.playlistManager.OnChangeTrackEvent += playlistManager_OnChangeTrackEvent;
                audioPlayer.OnExceptionEvent += audioPlayer_OnExceptionEvnet;
                audioPlayer.PlaybackStartEvent += audioPlayer_PlaybackStartEvent;
                audioPlayer.PlaybackProgressChangeEvent += audioPlayer_PlaybackProgressChangeEvent;
                audioPlayer.PlaybackStopEvent += audioPlayer_PlaybackStopEvent;
                httpClient = new HttpClient(ConfigManager.Instance.config.audio_server_dns, ConfigManager.Instance.config.http_port);
                httpClient.ExecGETquery("method_name=get_playlists", (response) =>
                {
                    audioPlayer.playlistManager.LoadCollection(response.GetResponseStream());
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Initialization error");
            }
        }

        void audioPlayer_PlaybackStopEvent()
        {
            PlaybackProgress.Invoke(new Action(() =>
            {
                PlaybackProgress.Value = 0;
            }));
            PlaybackTime.Invoke(new Action(() =>
            {
                PlaybackTime.Text = "00:00/00:00";
            }));
            SetCurrentInfo(null, null, null, null);
        }

        void audioPlayer_PlaybackProgressChangeEvent(TimeSpan currentTime, TimeSpan totalTime, long position)
        {
            try
            {
                PlaybackProgress.Invoke(new Action(() =>
                {
                    PlaybackProgress.Value = (int)position;
                }));
            }
            catch { }
            try
            {
                PlaybackTime.Invoke(new Action(() =>
                {
                    PlaybackTime.Text = currentTime.ToString(@"mm\:ss") + "/" + totalTime.ToString(@"mm\:ss");
                }));
            }
            catch { }
        }

        void audioPlayer_PlaybackStartEvent(TimeSpan totalTime, long length, AudioFileInfo file)
        {
            PlaybackProgress.Invoke(new Action(() =>
                {
                    PlaybackProgress.Value = 0;
                    PlaybackProgress.Maximum = (int)length;
                }));
            PlaybackTime.Invoke(new Action(() =>
                {
                    PlaybackTime.Text = "00:00/" + totalTime.ToString(@"mm\:ss");
                }));
            SetCurrentInfo(file.song, file.album, file.singer, TimeSpan.FromSeconds(file.length).ToString(@"mm\:ss"));
            PlaylistPanel lv = null;
            PlaylistCollectionWindow.Invoke(new Action(() =>
                lv = ((PlaylistPanel)PlaylistCollectionWindow.SelectedTab.Controls["PlaylistBox"])));
            lv.Invoke(new Action(() =>
            {
                PlaylistElement item = null;
                if (lv.TryGetItem(file, out item))
                    lv.SetActive(item);
            }));
        }

        void audioPlayer_OnExceptionEvnet(Exception e)
        {
            //MessageBox.Show(e.ToString(), "Error");
            log.Error(e);
        }

        void playlistManager_OnChangeTrackEvent(Playlist pl, AudioFileInfo fileInfo)
        {
            PlaylistPanel lv = null;
            PlaylistCollectionWindow.Invoke(new Action(() =>
                lv = ((PlaylistPanel)PlaylistCollectionWindow.TabPages[pl.Name].Controls["PlaylistBox"])));
            lv.Invoke(new Action(() =>
            {
                PlaylistElement item = null;
                if (lv.TryGetItem(fileInfo, out item))
                {
                    lv.SetActive(item);
                    lv.ScrollControlIntoView(item);
                }
            }));

            SetCurrentInfo(fileInfo.song, fileInfo.album, fileInfo.singer, TimeSpan.FromSeconds(fileInfo.length).ToString(@"mm\:ss"));
        }

        void SetCurrentInfo(string title, string album, string performer, string duration)
        {
            SongTitle.Invoke(new Action(() => SongTitle.Text = title));
            SongAlbum.Invoke(new Action(() => SongAlbum.Text = album));
            SongPerformer.Invoke(new Action(() => SongPerformer.Text = performer));
            SongDuration.Invoke(new Action(() => SongDuration.Text = duration));
        }

        void playlistManager_OnCollectionLoadEvent(Dictionary<string, Playlist> playlistCollection)
        {
            PlaylistCollectionWindow.Invoke(new Action(() =>
            {
                PlaylistCollectionWindow.TabPages.Clear();
                foreach (var pl in playlistCollection.Values)
                {
                    TabPage page = new TabPage();
                    page.Name = pl.Name;
                    page.Text = pl.Name;
                    PlaylistPanel PlaylistBox = new PlaylistPanel();
                    page.Controls.Add(PlaylistBox);
                    PlaylistBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
                    //PlaylistBox.CheckBoxes = false;
                    PlaylistBox.Dock = System.Windows.Forms.DockStyle.Fill;
                    //PlaylistBox.HideSelection = false;
                    //PlaylistBox.LabelWrap = false;
                    PlaylistBox.Location = new System.Drawing.Point(3, 3);
                    //PlaylistBox.MultiSelect = false;
                    PlaylistBox.Name = "PlaylistBox";
                    //PlaylistBox.ShowGroups = false;
                    PlaylistBox.Size = new System.Drawing.Size(232, 305);
                    PlaylistBox.TabIndex = 9;
                    //PlaylistBox.UseCompatibleStateImageBehavior = false;
                    //PlaylistBox.View = System.Windows.Forms.View.SmallIcon;
                    PlaylistBox.Tag = pl;
                    PlaylistBox.PlaylistItemMouseDoubleClick += PlaylistBox_MouseDoubleClick;
                    PlaylistCollectionWindow.TabPages.Add(page);
                    Application.DoEvents();
                    //PlaylistBox.BeginUpdate();
                    foreach (var item in pl.FileList)
                    {
                        PlaylistBox.Items.Add(item);
                        //plItem.Name = item.name;
                        //plItem.Tag = item;
                        //plItem.Checked = true;
                        //while (PlaylistBox.Bounds.Width - plItem.Bounds.Width > 2)
                        //    plItem.Text += " ";
                        Application.DoEvents();
                    }
                    //PlaylistBox.EndUpdate();
                }
                if (PlaylistCollectionWindow.TabPages.Count > 0)
                {
                    PlaylistPanel pp = PlaylistCollectionWindow.SelectedTab.Controls["PlaylistBox"] as PlaylistPanel;
                    if (pp != null && pp.Items.Count > 0)
                        pp.SetActive(pp.Items.First());
                }
            }));
        }

        void PlaylistBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //PlaylistElement item = sender as PlaylistElement;
            PlaylistPanel pl = PlaylistCollectionWindow.SelectedTab.Controls["PlaylistBox"] as PlaylistPanel;
            audioPlayer.PlayFile(pl.SelectedItem.FileInfo, (Playlist)pl.Tag);
        }

        void volume_VolumeChanged(object sender, EventArgs e)
        {
            audioPlayer.SetVolume(volume.Volume);
        }

        private void Play_Click(object sender, EventArgs e)
        {
            Playlist pl = audioPlayer.playlistManager[PlaylistCollectionWindow.SelectedTab.Name];
            if (pl != null)
            {
                PlaylistPanel panel = PlaylistCollectionWindow.SelectedTab.Controls["PlaylistBox"] as PlaylistPanel;
                if (panel.ActiveItem != null)
                {
                    AudioFileInfo file = panel.ActiveItem.FileInfo;
                    audioPlayer.PlayFile(file, pl);
                }
            }
        }



        private void Stop_Click(object sender, EventArgs e)
        {
            audioPlayer.StopPlayer();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigManager.Instance.SaveConfig();
            audioPlayer.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Show();
            Application.DoEvents();
            Init();
        }

        private void NextTrack_Click(object sender, EventArgs e)
        {
            audioPlayer.NextTrack();
        }

        private void PrevTrack_Click(object sender, EventArgs e)
        {
            audioPlayer.PreviousTrack();
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            audioPlayer.PausePlayer();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        int iFormX, iFormY, iMouseX, iMouseY;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            iFormX = this.Location.X;
            iFormY = this.Location.Y;
            iMouseX = MousePosition.X;
            iMouseY = MousePosition.Y;

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            int iMouseX2 = MousePosition.X;
            int iMouseY2 = MousePosition.Y;
            if (e.Button == MouseButtons.Left)
                this.Location = new Point(iFormX + (iMouseX2 - iMouseX), iFormY + (iMouseY2 - iMouseY));

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (new Settings().ShowDialog() == System.Windows.Forms.DialogResult.OK)
                Init();
        }

        private void PlaybackProgress_MouseClick(object sender, MouseEventArgs e)
        {
            // Get mouse position(x) minus the width of the progressbar (so beginning of the progressbar is mousepos = 0 //
            float absoluteMouse = (PointToClient(MousePosition).X - PlaybackProgress.Bounds.X - PlaybackControlsContainer.Bounds.X - panel1.Bounds.X);
            // Calculate the factor for converting the position (progbarWidth/100) //
            float calcFactor = PlaybackProgress.Width / (float)PlaybackProgress.Maximum;
            // In the end convert the absolute mouse value to a relative mouse value by dividing the absolute mouse by the calcfactor //
            float relativeMouse = absoluteMouse / calcFactor;

            // Set the calculated relative value to the progressbar //
            audioPlayer.SetPosition((long)relativeMouse);
        }

        private void SongTitle_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(SongTitle, SongTitle.Text);
        }

        private void SongAlbum_Click(object sender, EventArgs e)
        {

        }

        private void RandomOrderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            audioPlayer.SetRandomOrder(RandomOrderCheckBox.Checked);
        }

        private void Repeat_Click(object sender, EventArgs e)
        {
            repeat = !repeat;
            audioPlayer.SetRepeat(repeat);
            Repeat.BackColor = repeat ? SystemColors.ButtonFace : SystemColors.ActiveCaption;
        }

    }
}
