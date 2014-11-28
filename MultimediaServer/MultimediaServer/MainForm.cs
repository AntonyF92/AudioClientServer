﻿using System;
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
using PlaylistControls;

namespace MediaServer
{

    public partial class MainForm : Form
    {
        MediaServer server = new MediaServer(ServerSettings.Default.http_port);

        public MainForm()
        {
            GlobalDiagnosticsContext.Set("ApplicationName", "MediaServer");
            ServerData.Init();
            FileServer.Init(ServerSettings.Default.data_port);
            ServerData.Instance.playlistManager.OnCollectionLoadEvent += playlistManager_OnCollectionLoadEvent;
            ServerData.Instance.playlistManager.OnPlaylistAddEvent += playlistManager_OnPlaylistAddEvent;
            InitializeComponent();
        }

        void playlistManager_OnPlaylistAddEvent(Playlist playlist)
        {
            PlaylistCollectionWindow.Invoke(new Action(() => AddTabPaige(playlist)));
        }

        void AddTabPaige(Playlist pl)
        {
            TabPage page = new TabPage();
            page.Name = pl.Name;
            page.Text = pl.Name;
            //ListView PlaylistBox = new ListView();
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
            PlaylistCollectionWindow.TabPages.Add(page);
            PlaylistCollectionWindow.SelectedTab = page;
            Application.DoEvents();
            //PlaylistBox.BeginUpdate();
            LoadPlaylistIntoListView(PlaylistBox, pl);
            //PlaylistBox.EndUpdate();
        }

        void LoadPlaylistIntoListView(PlaylistPanel PlaylistBox, Playlist pl)
        {
            PlaylistBox.Invoke(new Action(() =>
            {
                foreach (var item in pl.FileList)
                {
                    PlaylistBox.Items.Add(item);
                    /*plItem.Name = item.name;
                    plItem.Tag = item;
                    plItem.Checked = true;
                    while (PlaylistBox.Bounds.Width - plItem.Bounds.Width > 25)
                        plItem.Text += " ";*/
                    Application.DoEvents();
                }
            }));
        }

        List<string> GetFilesInFolder(string path, bool include_subfolders = true)
        {
            List<string> files = new List<string>();
            foreach (var file in Directory.GetFiles(path))
                if (Path.GetExtension(file) == ".mp3")
                    files.Add(file);
            if (include_subfolders)
                foreach (var subfolder in Directory.GetDirectories(path))
                    files.AddRange(GetFilesInFolder(subfolder, include_subfolders));
            return files;
        }



        void playlistManager_OnCollectionLoadEvent(Dictionary<string, Playlist> playlistCollection)
        {
            PlaylistCollectionWindow.Invoke(new Action(() =>
            {
                PlaylistCollectionWindow.TabPages.Clear();
                foreach (var pl in playlistCollection.Values)
                {
                    AddTabPaige(pl);
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
                    string name;
                    if (InputBox.ShowBox("Enter playlist name:", out name) == System.Windows.Forms.DialogResult.OK)
                    {
                        ServerData.Instance.playlistManager.AddPlaylist(CreatePlaylist(OpenFilesDialog.FileNames, name));
                        ServerData.Instance.SavePlaylists();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Open files error");
            }
        }

        Playlist CreatePlaylist(string[] files, string name)
        {
            Playlist pl = new Playlist();
            pl.Name = name;
            foreach (var file in files)
            {
                pl.FileList.Add(GetFileInfo(file));
            }
            return pl;
        }

        AudioFileInfo GetFileInfo(string file)
        {
            AudioFileInfo info = new AudioFileInfo();
            TagLib.File f = null;
            try
            {
                f = TagLib.File.Create(file);
            }
            catch { }
            info.name = Path.GetFileNameWithoutExtension(file);
            info.path = file;
            if (f != null)
            {
                if (f.Tag != null)
                {
                    info.album = f.Tag.Album;
                    info.singer = string.Join("|", f.Tag.Performers);
                    info.song = f.Tag.Title;
                    info.year = f.Tag.Year.ToString();
                }
                if (f.Properties != null)
                {
                    info.bitrate = f.Properties.AudioBitrate;
                    info.length = Math.Round(f.Properties.Duration.TotalSeconds, 2);
                    info.frequency = f.Properties.AudioSampleRate;
                    info.size = (float)Math.Round((new FileInfo(file).Length / 1024f / 1024f), 2);
                }
            }
            return info;
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (OpenFoldersDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    List<string> files = GetFilesInFolder(OpenFoldersDialog.SelectedPath);
                    if (files.Count > 0)
                    {
                        string name = "";
                        if (InputBox.ShowBox("Enter playlist name:", out name) == System.Windows.Forms.DialogResult.OK)
                        {
                            ServerData.Instance.playlistManager.AddPlaylist(CreatePlaylist(files.ToArray(), name));
                            ServerData.Instance.SavePlaylists();
                        }
                    }
                    else
                        MessageBox.Show("There is no files in that folder", "Create playlist");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error in create playlist by open folder");
            }
        }

        private void removePlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PlaylistCollectionWindow.Invoke(new Action(() =>
                    {
                        Playlist plForRemove = null;
                        PlaylistPanel playlistBox = PlaylistCollectionWindow.SelectedTab.Controls["PlaylistBox"] as PlaylistPanel;
                        plForRemove = playlistBox.Tag as Playlist;
                        ServerData.Instance.playlistManager.RemovePlaylist(plForRemove);
                        PlaylistCollectionWindow.TabPages.RemoveByKey(plForRemove.Name);
                    }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Remove playlist error");
            }
        }
    }
}
