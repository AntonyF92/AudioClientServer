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

namespace SampleClient
{
    public partial class MainForm : Form
    {
        AudioPlayer audioPlayer;
        HttpClient httpClient;
        VolumeSlider volume;

        public MainForm()
        {
            InitializeComponent();
            ConfigManager.Init();
            volume = new VolumeSlider();
            volume.Location = new Point(3, 80);
            volume.Size = new Size(132, 15);
            splitContainer1.Panel1.Controls.Add(volume);
            volume.VolumeChanged += volume_VolumeChanged;
        }

        void Init()
        {
            try
            {
                audioPlayer = new AudioPlayer();
                audioPlayer.playlistManager.OnCollectionLoadEvent += playlistManager_OnCollectionLoadEvent;
                audioPlayer.playlistManager.OnChangeTrackEvent += playlistManager_OnChangeTrackEvent;
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

        void playlistManager_OnChangeTrackEvent(Playlist pl, AudioFileInfo fileInfo)
        {
            ListView lv = null;
            PlaylistCollectionWindow.Invoke(new Action(() =>
                lv = ((ListView)PlaylistCollectionWindow.TabPages[pl.Name].Controls["PlaylistBox"])));
            lv.Invoke(new Action(() =>
            {
                lv.SelectedItems.Clear();
                lv.Items[fileInfo.name].Selected = true;
            }));
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
                    ListView PlaylistBox = new ListView();
                    page.Controls.Add(PlaylistBox);
                    PlaylistBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
                    PlaylistBox.CheckBoxes = false;
                    PlaylistBox.Dock = System.Windows.Forms.DockStyle.Fill;
                    PlaylistBox.HideSelection = false;
                    PlaylistBox.LabelWrap = false;
                    PlaylistBox.Location = new System.Drawing.Point(3, 3);
                    PlaylistBox.MultiSelect = false;
                    PlaylistBox.Name = "PlaylistBox";
                    PlaylistBox.ShowGroups = false;
                    PlaylistBox.Size = new System.Drawing.Size(232, 305);
                    PlaylistBox.TabIndex = 9;
                    PlaylistBox.UseCompatibleStateImageBehavior = false;
                    PlaylistBox.View = System.Windows.Forms.View.SmallIcon;
                    PlaylistBox.Tag = pl;
                    PlaylistBox.MouseDoubleClick += PlaylistBox_MouseDoubleClick;
                    PlaylistCollectionWindow.TabPages.Add(page);
                    Application.DoEvents();
                    //PlaylistBox.BeginUpdate();
                    foreach (var item in pl.FileList)
                    {
                        var plItem = PlaylistBox.Items.Add(item.name);
                        plItem.Name = item.name;
                        plItem.Tag = item;
                        plItem.Checked = true;
                        while (PlaylistBox.Bounds.Width - plItem.Bounds.Width > 2)
                            plItem.Text += " ";
                        Application.DoEvents();
                    }
                    //PlaylistBox.EndUpdate();
                }
            }));
        }

        void PlaylistBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var item = ((ListView)sender);
            audioPlayer.Play((AudioFileInfo)item.SelectedItems[0].Tag, (Playlist)item.Tag);
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
                AudioFileInfo file = ((ListView)PlaylistCollectionWindow.SelectedTab.Controls["PlaylistBox"]).SelectedItems[0].Tag as AudioFileInfo;
                audioPlayer.Play(file, pl);
            }
        }



        private void Stop_Click(object sender, EventArgs e)
        {
            audioPlayer.StopPlayer();
        }

        void StopPlayer()
        {
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
    }
}
