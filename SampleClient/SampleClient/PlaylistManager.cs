using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace SampleClient
{
    public class PlaylistManager
    {
        Dictionary<string, Playlist> playlistCollection = new Dictionary<string, Playlist>();

        public Playlist this[string name]
        {
            get
            {
                if (playlistCollection.ContainsKey(name))
                    return playlistCollection[name];
                return null;
            }
        }

        private Action<Playlist, NetworkFileInfo> PlaylistItemDoubleClickHandler = null;

        public PlaylistManager(Action<Playlist, NetworkFileInfo> PlaylistItemDoubleClickHandler)
        {
            if (PlaylistItemDoubleClickHandler == null)
                throw new ArgumentNullException("PlaylistItemDoubleClickHandler");
            this.PlaylistItemDoubleClickHandler = PlaylistItemDoubleClickHandler;
        }

        public void AddPlaylist(Playlist pl)
        {
            if (!playlistCollection.ContainsKey(pl.Name))
                playlistCollection.Add(pl.Name, pl);
        }

        public void LoadCollection(Stream stream)
        {
            playlistCollection.Clear();
            List<Playlist> list = new List<Playlist>();
            XmlSerializer sr = new XmlSerializer(typeof(List<Playlist>));
            list = (List<Playlist>)sr.Deserialize(stream);
            foreach (var pl in list)
                playlistCollection.Add(pl.Name, pl);
        }

        public void UpdatePlaylist(Playlist pl)
        {
            if (playlistCollection.ContainsKey(pl.Name))
                playlistCollection[pl.Name] = pl;
        }

        public void LoadPlaylistCollectionIntoTabControl(TabControl control)
        {
            control.Invoke(new Action(() =>
            {
                control.TabPages.Clear();
                foreach (var pl in playlistCollection.Values)
                {
                    TabPage page = new TabPage();
                    page.Name = pl.Name;
                    page.Text = pl.Name;
                    ListView PlaylistBox = new ListView();
                    page.Controls.Add(PlaylistBox);
                    PlaylistBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
                    PlaylistBox.CheckBoxes = true;
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
                    PlaylistBox.MouseDoubleClick += PlaylistBox_MouseDoubleClick;
                    PlaylistBox.BeginUpdate();
                    foreach (var item in pl.FileList)
                    {
                        var plItem = PlaylistBox.Items.Add(item.name);
                        plItem.Tag = item;
                        plItem.Checked = true;
                        while (PlaylistBox.Bounds.Width - plItem.Bounds.Width > 2)
                            plItem.Text += " ";
                    }
                    PlaylistBox.EndUpdate();
                    control.TabPages.Add(page);
                }
            }));
        }

        void PlaylistBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var item = ((ListView)sender).SelectedItems[0];
            PlaylistItemDoubleClickHandler(playlistCollection[((ListView)sender).Parent.Name], (NetworkFileInfo)item.Tag);
        }
    }
}
