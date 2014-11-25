using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override string ToString()
        {
            return name;
        }
    }

