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

    public partial class MainForm : Form
    {
        List<AudioFileInfo> playlist = new List<AudioFileInfo>();
        MediaServer server = new MediaServer(ServerSettings.Default.http_port);

        public MainForm()
        {
            GlobalDiagnosticsContext.Set("ApplicationName", "MediaServer");
            ServerData.Init();
            FileServer.Init(ServerSettings.Default.data_port);
            ServerData.Instance.playlistManager.OnCollectionLoadEvent += playlistManager_OnCollectionLoadEvent;
            InitializeComponent();
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
                    PlaylistCollectionWindow.TabPages.Add(page);
                    PlaylistBox.BeginUpdate();
                    foreach (var item in pl.FileList)
                    {
                        var plItem = PlaylistBox.Items.Add(item.name);
                        plItem.Name = item.name;
                        plItem.Tag = item;
                        plItem.Checked = true;
                        while (PlaylistBox.Bounds.Width - plItem.Bounds.Width > 2)
                            plItem.Text += " ";
                    }
                    PlaylistBox.EndUpdate();
                }
            }));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ServerData.Instance.LoadData();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            FileServer.Instance.StopListening();
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

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void startServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (server.IsActive)
                server.StopListening();
            ThreadPool.QueueUserWorkItem(o => server.Listen());
        }

        private void stopServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (server.IsActive)
                server.StopListening();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (OpenFilesDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (var file in OpenFilesDialog.FileNames)
                    {
                        Mp3File mp3 = new Mp3File(file);
                        Id3Tag tag = mp3.GetTag(Id3TagFamily.Version2x);
                        AudioFileInfo afi = new AudioFileInfo()
                        {
                            album = tag.Album.Value,
                            author = tag.Band.Value,
                            bitrate = mp3.Audio.Bitrate,
                            length = mp3.Audio.Duration,
                            name = Path.GetFileNameWithoutExtension(file),
                            path = file,
                            song = tag.Title.Value,
                            year = tag.Year.Value + ""
                        };

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Open files error");
            }
        }
    }
}
