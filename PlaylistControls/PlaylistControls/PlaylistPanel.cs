﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlaylistControls
{
    public partial class PlaylistPanel : UserControl
    {
        public class PlaylistElementCollection : List<PlaylistElement>
        {
            public PlaylistPanel panel { get; private set; }
            Point startLocation = new Point(0, 0);
            int rangeBetweenElementsVertical = 0;
            Point currentLocation = new Point(0, 0);
            public PlaylistElementCollection(PlaylistPanel panel)
                : base()
            {
                this.panel = panel;
            }

            public void Add(AudioFileInfo fileInfo)
            {
                PlaylistElement item = new PlaylistElement(fileInfo);
                item.Location = currentLocation;
                base.Add(item);
                panel.Controls.Add(item);
                if (panel.PlaylistItemMouseDoubleClick != null)
                {
                    item.MouseDoubleClick += panel.PlaylistItemMouseDoubleClick;
                }
                currentLocation = new Point(0, currentLocation.Y + item.Size.Height + rangeBetweenElementsVertical);
            }

            public void AddRange(IEnumerable<AudioFileInfo> list)
            {
                foreach (var v in list)
                    Add(v);
            }

            public new void Clear()
            {
                PlaylistElement.selectedItem = null;
                base.Clear();
                panel.Controls.Clear();
            }
        }


        private PlaylistElementCollection items;
        public PlaylistElementCollection Items { get { return items; } }

        [Browsable(true)]
        //[Category("Appearance")]
        //[Description("Gets and sets the foreground color of the textbox")]
        public event MouseEventHandler PlaylistItemMouseDoubleClick;


        public PlaylistPanel()
        {
            InitializeComponent();
            items = new PlaylistElementCollection(this);
        }

        public PlaylistElement SelectedItem
        {
            get
            {
                return PlaylistElement.selectedItem;
            }
        }
    }
}
