using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int length;
        public int bitrate;
        public int frequency;
        public float size;
        public long size_b;

        public override string ToString()
        {
            return name;
        }

        public override bool Equals(object obj)
        {
            AudioFileInfo item = obj as AudioFileInfo;
            return this.album == item.album && this.bitrate == item.bitrate && this.frequency == item.frequency && this.length == item.length &&
                this.name == item.name && this.path == item.path && this.singer == item.singer && this.size_b == item.size_b &&
                this.song == item.song && this.year == item.year;
        }
    }
}

