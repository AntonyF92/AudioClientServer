using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaServer;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using NAudio;
using System.IO;
using System.Threading;
using NAudio.Wave;

namespace SampleClient
{
    public partial class Form1 : Form
    {
        AudioPlayer audioPlayer;
        HttpClient httpClient;

        public Form1()
        {
            InitializeComponent();
            ConfigManager.Init();
            audioPlayer = new AudioPlayer();
            httpClient = new HttpClient(ConfigManager.Instance.config.audio_server_dns, ConfigManager.Instance.config.http_port);
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
            
        }

        void StopPlayer()
        {
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigManager.Instance.SaveConfig();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            httpClient.ExecGETquery("method_name=get_playlists", (response) =>
                {
                    audioPlayer.playlistManager.LoadCollection(response.GetResponseStream());
                });
            audioPlayer.playlistManager.LoadPlaylistCollectionIntoTabControl(PlaylistCollectionWindow);
        }
    }
}
