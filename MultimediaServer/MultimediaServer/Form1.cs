using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using NLog;
using Id3.Id3v2;
using Id3.Info;
using Id3;

namespace MediaServer
{

    public partial class Form1 : Form
    {
        List<AudioFileInfo> playlist = new List<AudioFileInfo>();
        MediaServer server = new MediaServer(ServerSettings.Default.http_port);

        public Form1()
        {
            GlobalDiagnosticsContext.Set("ApplicationName", "MediaServer");
            ServerData.Init();
            FileServer.Init(ServerSettings.Default.data_port);
            InitializeComponent();
        }

        private void SelectFiles_Click(object sender, EventArgs e)
        {
            if (OpenFilesDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                playlist.Clear();
                Playlist.Items.Clear();
                ServerData.Instance.Playlists.Clear();
                foreach (var file in OpenFilesDialog.FileNames)
                {
                    AudioFileInfo mp3 = new AudioFileInfo()
                    {
                        name = Path.GetFileName(file),
                        path = Path.GetFullPath(file)
                    };
                    playlist.Add(mp3);
                    Playlist.Items.Add(mp3);
                }
                ServerData.Instance.Playlists.Add("new", new Playlist() { Name = "new", FileList = new List<AudioFileInfo>(playlist) });
                ServerData.Instance.SavePlaylists();
            }
        }

        private void StartServer_Click(object sender, EventArgs e)
        {
            if (server.IsActive)
                server.StopListening();
            ThreadPool.QueueUserWorkItem(o => server.Listen());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (ServerData.Instance.Playlists.ContainsKey("new"))
            {
                foreach (var v in ServerData.Instance.Playlists["new"].FileList)
                {
                    Playlist.Items.Add(v.name);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            FileServer.Instance.StopListening();
        }
    }
}
