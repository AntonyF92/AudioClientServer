using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;
using PlaylistControls;

namespace AudioPlayer
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
                //audioPlayer.playlistManager.OnChangeTrackEvent += playlistManager_OnChangeTrackEvent;
                //audioPlayer.OnExceptionEvent += audioPlayer_OnExceptionEvnet;
                //audioPlayer.PlaybackStartEvent += audioPlayer_PlaybackStartEvent;
                //audioPlayer.PlaybackProgressChangeEvent += audioPlayer_PlaybackProgressChangeEvent;
                //audioPlayer.PlaybackStopEvent += audioPlayer_PlaybackStopEvent;
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
            
        }

        void audioPlayer_PlaybackProgressChangeEvent(TimeSpan currentTime, TimeSpan totalTime, long position)
        {
           
        }

        void audioPlayer_PlaybackStartEvent(TimeSpan totalTime, long length, AudioFileInfo file)
        {
        }

        void audioPlayer_OnExceptionEvnet(Exception e)
        {
            //MessageBox.Show(e.ToString(), "Error");
            log.Error(e);
        }

        void playlistManager_OnChangeTrackEvent(Playlist pl, AudioFileInfo fileInfo)
        {
           
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
                    PlaylistView PlaylistBox = new PlaylistView();
                    PlaylistBox.Init(page, true);
                    PlaylistBox.Tag = pl;
                    PlaylistCollectionWindow.TabPages.Add(page);
                    PlaylistBox.UpdateAfterInit();
                    Application.DoEvents();
                    PlaylistBox.BeginUpdate();
                    foreach (var item in pl.FileList)
                    {
                        PlaylistBox.AddItem(item);
                    }
                    PlaylistBox.EndUpdate();
                }
            }));
        }

        void PlaylistBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //PlaylistElement item = sender as PlaylistElement;
            PlaylistView pl = PlaylistCollectionWindow.SelectedTab.Controls["PlaylistBox"] as PlaylistView;
            //audioPlayer.PlayFile(pl.SelectedItem.FileInfo, (Playlist)pl.Tag);
        }

        void volume_VolumeChanged(object sender, EventArgs e)
        {
        }

        private void Play_Click(object sender, EventArgs e)
        {

        }



        private void Stop_Click(object sender, EventArgs e)
        {
            audioPlayer.Stop();
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
            //// Get mouse position(x) minus the width of the progressbar (so beginning of the progressbar is mousepos = 0 //
            //float absoluteMouse = (PointToClient(MousePosition).X - PlaybackProgress.Bounds.X - PlaybackControlsContainer.Bounds.X - panel1.Bounds.X);
            //// Calculate the factor for converting the position (progbarWidth/100) //
            //float calcFactor = PlaybackProgress.Width / (float)PlaybackProgress.Maximum;
            //// In the end convert the absolute mouse value to a relative mouse value by dividing the absolute mouse by the calcfactor //
            //float relativeMouse = absoluteMouse / calcFactor;

            //// Set the calculated relative value to the progressbar //
            //audioPlayer.SetPosition((long)relativeMouse);
        }

        private void SongTitle_MouseHover(object sender, EventArgs e)
        {
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CloseButton_MouseHover(object sender, EventArgs e)
        {
            CloseButton.BackColor = Color.Red;
            CloseButton.ForeColor = Color.White;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.BackColor = SystemColors.Window;
            CloseButton.ForeColor = Color.Black;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RandomButton.Checked = !RandomButton.Checked;
            audioPlayer.SetRandomOrder(RandomButton.Checked);
        }

        private void SongAlbum_Click(object sender, EventArgs e)
        {

        }

        private void RandomOrderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //.SetRandomOrder(RandomOrderCheckBox.Checked);
        }

        private void Repeat_Click(object sender, EventArgs e)
        {
        }
    }
}
