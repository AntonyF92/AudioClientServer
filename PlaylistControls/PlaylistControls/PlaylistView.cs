using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PlaylistControls
{

    public class PlaylistView : ListView
    {


        public List<AudioFileInfo> PlaylistItems { get; private set; }
        public AudioFileInfo currentFile { get; private set; }

        ColumnHeader state;
        ColumnHeader performer;
        ColumnHeader song;
        ColumnHeader duration;

        public PlaylistView()
        {
            Name = "PlaylistView";
            PlaylistItems = new List<AudioFileInfo>();
        }

        public void Init(Control parent, bool fill)
        {
            this.Parent = parent;
            this.AllowColumnReorder = false;
            this.MultiSelect = false;
            this.ColumnWidthChanging += PlaylistView_ColumnWidthChanging;
            this.ColumnWidthChanged += PlaylistView_ColumnWidthChanged;
            this.DoubleBuffered = true;
            if (fill)
                this.Dock = DockStyle.Fill;
            this.state = new System.Windows.Forms.ColumnHeader();
            this.performer = new System.Windows.Forms.ColumnHeader();
            this.song = new System.Windows.Forms.ColumnHeader();
            duration = new ColumnHeader();
            // 
            // listView1
            // 
            this.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.state,
            this.performer,
            this.song,
            this.duration});
            this.Location = new System.Drawing.Point(0, 0);
            this.Size = new System.Drawing.Size(475, 560);
            this.UseCompatibleStateImageBehavior = false;
            this.View = System.Windows.Forms.View.Details;
            this.FullRowSelect = true;
            this.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            // 
            // state
            // 
            this.state.Text = "";
            this.state.Width = 20;
            // 
            // duration
            // 
            this.duration.Text = "Length";
            this.duration.Width = 50;
            this.duration.TextAlign = HorizontalAlignment.Right;
            int width = this.Size.Width - (state.Width + duration.Width);
            // 
            // performer
            // 
            this.performer.Text = "Artist";
            this.performer.Width = width / 5 * 2;
            // 
            // song
            // 
            this.song.Text = "Song";
            song.Width = width - performer.Width;

            this.SmallImageList = new ImageList();
            this.SmallImageList.Images.Add(Properties.Resources.Play);
        }

        private void PlaylistView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            
        }

        private void PlaylistView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
        }

        void AddGroup(string key, string name)
        {
            
            ListViewGroup group = new ListViewGroup(key, name);
            this.Groups.Add(group);
        }

        public void AddItem(AudioFileInfo file)
        {
            PlaylistItems.Add(file);
            ListViewItem item = new ListViewItem();
            item.Tag = file;
            item.SubItems.Add(file.singer);
            item.SubItems.Add(string.IsNullOrEmpty(file.song)? file.name : file.song);
            item.SubItems.Add(TimeSpan.FromSeconds(file.length).ToString(@"mm\:ss"));
            file.playlistViewItem = item;
            if (!this.Groups.ContainsGroup(el => el == file.folder_key))
                AddGroup(file.folder_key, file.folder);
            item.Group = this.Groups[file.folder_key];
            this.Items.Add(item);
        }

        public void SetCurrentFile(string file)
        {
            this.Invoke(new Action(() =>
            {
                if (currentFile != null)
                {
                    currentFile.playlistViewItem.ImageIndex = -1;
                    currentFile.playlistViewItem.BackColor = System.Drawing.Color.Transparent;
                    currentFile = null;
                }
                foreach (var v in PlaylistItems)
                {
                    if (v.path == file)
                    {
                        currentFile = v;
                        currentFile.playlistViewItem.ImageIndex = 0;
                        currentFile.playlistViewItem.BackColor = System.Drawing.SystemColors.ActiveCaption;
                        currentFile.playlistViewItem.EnsureVisible();
                        break;
                    }
                }
            }));
        }

        public void UpdateAfterInit()
        {
            int width = this.Size.Width - (state.Width + duration.Width + 21);
            this.performer.Width = width / 5 * 2;
            song.Width = width - performer.Width;
        }

    }
}
