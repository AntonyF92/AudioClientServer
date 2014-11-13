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

namespace MediaServer
{

    public partial class Form1 : Form
    {
        List<NetworkFileInfo> playlist = new List<NetworkFileInfo>();
        MediaServer server = new MediaServer(11000);

        public Form1()
        {
            GlobalDiagnosticsContext.Set("ApplicationName", "MediaServer");
            ServerData.Init();
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
                    NetworkFileInfo mp3 = new NetworkFileInfo()
                    {
                        name = Path.GetFileName(file),
                        path = file
                    };
                    playlist.Add(mp3);
                    Playlist.Items.Add(mp3);
                }
                ServerData.Instance.Playlists.Add("new", new Playlist() { Name = "new", FileList = new List<NetworkFileInfo>(playlist) });
                ServerData.Instance.SavePlaylists();
            }
        }

        private void StartServer_Click(object sender, EventArgs e)
        {
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
    }
}
