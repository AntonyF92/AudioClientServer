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
using PlaylistControls;

namespace MediaServer
{

    public partial class MainForm : Form
    {
        MediaServer server = new MediaServer(ServerSettings.Default.http_port);
        HttpFileServer fileServer;

        public MainForm()
        {
            GlobalDiagnosticsContext.Set("ApplicationName", "MediaServer");
            ServerData.Init();
            FileServer.Init(ServerSettings.Default.data_port);
            fileServer = new HttpFileServer(ServerSettings.Default.server_dns, ServerSettings.Default.server_port);
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
            PlaylistView PlaylistBox = new PlaylistView();
            PlaylistBox.Init(page, true);
            PlaylistBox.Tag = pl;
            PlaylistCollectionWindow.TabPages.Add(page);
            PlaylistCollectionWindow.SelectedTab = page;
            PlaylistBox.UpdateAfterInit();
            Application.DoEvents();
            //PlaylistBox.BeginUpdate();
            LoadPlaylistIntoListView(PlaylistBox, pl);
            //PlaylistBox.EndUpdate();
        }

        void LoadPlaylistIntoListView(PlaylistView PlaylistBox, Playlist pl)
        {
            PlaylistBox.Invoke(new Action(() =>
            {
                PlaylistBox.BeginUpdate();
                foreach (var item in pl.FileList)
                {
                    PlaylistBox.AddItem(item);
                }
                PlaylistBox.EndUpdate();
            }));
        }

        List<string> GetFilesInFolder(string path, bool include_subfolders = true)
        {
            List<string> files = new List<string>();
            foreach (var file in Directory.GetFiles(path))
                if (Path.GetExtension(file).ToLower() == ".mp3" || Path.GetExtension(file).ToLower() == ".wav")
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
            ThreadPool.QueueUserWorkItem(o => server.Listen());
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            FileServer.Instance.StopListening();
            fileServer.Dispose();
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
                        Task.Run(() =>
                        {
                            try
                            {
                                ServerData.Instance.playlistManager.AddPlaylist(CreatePlaylist(OpenFilesDialog.FileNames, name));
                                ServerData.Instance.SavePlaylists();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString(), "Error");
                            }
                        });
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
            string folder = Path.GetDirectoryName(file);
            info.folder = folder.Substring(folder.LastIndexOf('\\') + 1);
            info.name = Path.GetFileNameWithoutExtension(file);
            //info.path = file.Replace(ServerSettings.Default.server_folder, "").Replace("\\", "/");
            info.path = file;
            info.exstension = Path.GetExtension(file).Replace(".", "");
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
                    info.size = (float)(new FileInfo(file).Length);
                    info.size_mb = (float)Math.Round(info.size / 1024f / 1024f, 2);
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
                            Task.Run(() =>
                                {
                                    try
                                    {
                                        ServerData.Instance.playlistManager.AddPlaylist(CreatePlaylist(files.ToArray(), name));
                                        ServerData.Instance.SavePlaylists();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.ToString(), "Error");
                                    }
                                });
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
                        PlaylistView playlistBox = PlaylistCollectionWindow.SelectedTab.Controls["PlaylistView"] as PlaylistView;
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

        private void addFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (PlaylistCollectionWindow.SelectedTab != null)
                    if (OpenFoldersDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        List<string> files = GetFilesInFolder(OpenFoldersDialog.SelectedPath);
                        if (files.Count > 0)
                        {
                            Task.Run(() =>
                                {
                                    try
                                    {
                                        Playlist pl = ServerData.Instance.playlistManager[PlaylistCollectionWindow.SelectedTab.Name];
                                        foreach (var f in files)
                                            pl.FileList.Add(GetFileInfo(f));
                                        UpdatePlaylistPage(PlaylistCollectionWindow.SelectedTab, pl);
                                        ServerData.Instance.SavePlaylists();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.ToString(), "Error");
                                    }
                                });
                        }
                        else
                            MessageBox.Show("There is no files in that folder", "Add folder");
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error in add folder to playlist");
            }
        }

        void UpdatePlaylistPage(TabPage page, Playlist pl)
        {
            PlaylistView panel = null;
            page.Invoke(new Action(()=>
            {
                panel = page.Controls["PlaylistView"] as PlaylistView;
            }));
            panel.Invoke(new Action(() =>
            {
                panel.Controls.Clear();
                panel.Items.Clear();
                LoadPlaylistIntoListView(panel, pl);
            }));
        }
    }
}
