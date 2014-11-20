using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class AudioFileInfo
    {
        public string name;
        public string path;
        public string author;
        public string album;
        public string song;
        public string year;
        public TimeSpan length;
        public int bitrate;

        public override string ToString()
        {
            return name;
        }
    }

