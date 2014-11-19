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

namespace SampleClient
{
    public partial class Form1 : Form
    {
        AudioPlayer audioPlayer;
        HttpClient httpClient;
        VolumeSlider volume;

        public Form1()
        {
            InitializeComponent();
            ConfigManager.Init();
            audioPlayer = new AudioPlayer();
            httpClient = new HttpClient(ConfigManager.Instance.config.audio_server_dns, ConfigManager.Instance.config.http_port);
            volume = new VolumeSlider();
            volume.Location = new Point(3, 80);
            volume.Size = new Size(132, 15);
            splitContainer1.Panel1.Controls.Add(volume);
            volume.VolumeChanged += volume_VolumeChanged;
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
                NetworkFileInfo file = ((ListView)PlaylistCollectionWindow.SelectedTab.Controls["PlaylistBox"]).SelectedItems[0].Tag as NetworkFileInfo;
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
            httpClient.ExecGETquery("method_name=get_playlists", (response) =>
                {
                    audioPlayer.playlistManager.LoadCollection(response.GetResponseStream());
                });
            audioPlayer.playlistManager.LoadPlaylistCollectionIntoTabControl(PlaylistCollectionWindow);
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
    }
}
