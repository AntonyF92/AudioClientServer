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
                audioPlayer.OnException += audioPlayer_OnExceptionEvnet;
                audioPlayer.OnPlaybackStart += AudioPlayer_OnPlaybackStart;
                audioPlayer.PlaybackProgressChanged += AudioPlayer_PlaybackProgressChanged;
                audioPlayer.OnPlaybackStop += AudioPlayer_OnPlaybackStop;
                audioPlayer.SetVolume(VolumeBar.Value);
                httpClient = new HttpClient(ConfigManager.Instance.config.audio_server_dns, ConfigManager.Instance.config.http_port);
                httpClient.ExecGETquery("method_name=get_playlists", (response) =>
                {
                    audioPlayer.playlistManager.LoadCollection(response.GetResponseStream());
                });
                LoadFromConfig();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Initialization error");
            }
        }

        void LoadFromConfig()
        {
            RandomButton.Checked = PlayerConfig.Default.Random;
            RepeatButton.Checked = PlayerConfig.Default.Repeat_one;
            VolumeBar.Value = PlayerConfig.Default.Volume;
        }

        void SaveConfig()
        {
            PlayerConfig.Default.Random = RandomButton.Checked;
            PlayerConfig.Default.Repeat_one = RepeatButton.Checked;
            PlayerConfig.Default.Volume = VolumeBar.Value;
            PlayerConfig.Default.Save();
        }

        private void AudioPlayer_OnPlaybackStop(AudioFileInfo file)
        {
            PlaybackProgress.Value = 0;
            PlaybackTime.Invoke(new Action(() =>
            PlaybackTime.Text = "00:00"));
        }

        private void AudioPlayer_PlaybackProgressChanged(double position)
        {
            PlaybackProgress.Value = (int)position;
            PlaybackTime.Invoke(new Action(() =>
            PlaybackTime.Text = TimeSpan.FromSeconds(position).ToString(@"mm\:ss")));
        }

        private void AudioPlayer_OnPlaybackStart(AudioFileInfo file)
        {
            SongAlbum.Invoke(new Action(() =>
            SongAlbum.Text = file.album));
            SongPerformer.Invoke(new Action(() =>
            SongPerformer.Text = file.singer));
            SongTitle.Invoke(new Action(() =>
            SongTitle.Text = file.song));
            SongDuration.Invoke(new Action(() =>
            SongDuration.Text = TimeSpan.FromSeconds(file.length).ToString(@"mm\:ss")));
            PlaybackProgress.Invoke(new Action(() =>
            {
                PlaybackProgress.Maximum = (int)file.length;
                PlaybackProgress.Value = 0;
            }));
            TotalTime.Invoke(new Action(()=>
            TotalTime.Text = TimeSpan.FromSeconds(file.length).ToString(@"mm\:ss")));
            CurrentPlaylist?.Invoke(new Action(() =>
            CurrentPlaylist.SetCurrentFile(file.path)));
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
                    PlaylistBox.MouseDoubleClick += PlaylistBox_MouseDoubleClick1;
                }
                if (CurrentPlaylist?.Items.Count > 0)
                    CurrentPlaylist.Items[0].Selected = true;
            }));
        }

        private void PlaylistBox_MouseDoubleClick1(object sender, MouseEventArgs e)
        {
            try
            {
                PlaylistView pl = sender as PlaylistView;
                ListViewHitTestInfo hit = pl.HitTest(e.Location);
                if (hit.Item != null)
                {
                    AudioFileInfo info = hit.Item.Tag as AudioFileInfo;
                    audioPlayer.Play(info, pl.Tag as Playlist, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        PlaylistView CurrentPlaylist
        {
            get
            {
                PlaylistView v = null;
                PlaylistCollectionWindow.Invoke(new Action(() =>
                v = PlaylistCollectionWindow.SelectedTab?.Controls["PlaylistView"] as PlaylistView));
                return v;
            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigManager.Instance.SaveConfig();
            audioPlayer.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Show();
            VolumeBar.ValueChanged += VolumeBar_ValueChanged;
            Application.DoEvents();
            Init();
        }

        private void VolumeBar_ValueChanged()
        {
            audioPlayer?.SetVolume(VolumeBar.Value);
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
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            audioPlayer.Stop();
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            audioPlayer.Pause();
        }

        private void Play_Click(object sender, EventArgs e)
        {
            audioPlayer.Play(CurrentPlaylist?.SelectedItems[0]?.Tag as AudioFileInfo, CurrentPlaylist?.Tag as Playlist);
        }

        private void Play_MouseEnter(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            bt.BackColor = Color.LightSkyBlue;
        }

        private void Play_MouseLeave(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            bt.BackColor = Color.Transparent;
        }

        private void PlaybackProgress_Click(object sender, EventArgs e)
        {
            // Get mouse position(x) minus the width of the progressbar (so beginning of the progressbar is mousepos = 0 //
            float absoluteMouse = (PointToClient(MousePosition).X - PlaybackProgress.Bounds.X - PlaybackControlsContainer.Bounds.X - panel1.Bounds.X);
            // Calculate the factor for converting the position (progbarWidth/100) //
            float calcFactor = PlaybackProgress.Width / (float)PlaybackProgress.Maximum;
            // In the end convert the absolute mouse value to a relative mouse value by dividing the absolute mouse by the calcfactor //
            float relativeMouse = absoluteMouse / calcFactor;
            // Set the calculated relative value to the progressbar //
            audioPlayer.SetProgress(relativeMouse);
        }

        private void VolumeBar_Load(object sender, EventArgs e)
        {

        }

        private void RepeatButton_CheckedChanged(object sender, EventArgs e)
        {
            audioPlayer.SetRepeat(RepeatButton.Checked);

        }

        private void RandomButton_CheckedChanged(object sender, EventArgs e)
        {
            audioPlayer.SetRandomOrder(RandomButton.Checked);

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                SaveConfig();
            }
            catch
            { }
        }

        private void VolumeBar_Click(object sender, EventArgs e)
        {
            // Get mouse position(x) minus the width of the progressbar (so beginning of the progressbar is mousepos = 0 //
            float absoluteMouse = (PointToClient(MousePosition).X - VolumeBar.Bounds.X - PlaybackControlsContainer.Bounds.X - panel1.Bounds.X);
            // Calculate the factor for converting the position (progbarWidth/100) //
            float calcFactor = VolumeBar.Width / (float)VolumeBar.Maximum;
            // In the end convert the absolute mouse value to a relative mouse value by dividing the absolute mouse by the calcfactor //
            float relativeMouse = absoluteMouse / calcFactor;

            // Set the calculated relative value to the progressbar //
            int res = (int)Math.Round(relativeMouse);
            VolumeBar.Value = res;
        }

        private void RepeatButton_Click(object sender, EventArgs e)
        {
            RepeatButton.Checked = !RepeatButton.Checked;
        }

        private void SongAlbum_Click(object sender, EventArgs e)
        {

        }


    }
}
