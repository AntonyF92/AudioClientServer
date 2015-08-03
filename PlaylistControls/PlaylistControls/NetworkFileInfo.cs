using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace PlaylistControls
{
    public class AudioFileInfo
    {
        public string name;
        public string path;
        public string singer;
        public string album;
        public string song;
        public string year;
        public double length;
        public int bitrate;
        public int frequency;
        public float size;
        public float size_mb;
        public string exstension;
        [XmlIgnore]
        public ListViewItem playlistViewItem = null;

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(singer) && !string.IsNullOrEmpty(song))
                return singer + " - " + song;
            else
                return name;
        }

        public override bool Equals(object obj)
        {
            AudioFileInfo item = obj as AudioFileInfo;
            return this.album == item.album && this.bitrate == item.bitrate && this.frequency == item.frequency && this.length == item.length &&
                this.name == item.name && this.path == item.path && this.singer == item.singer && this.size == item.size &&
                this.song == item.song && this.year == item.year;
        }
    }
}

