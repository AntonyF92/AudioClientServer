using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class Playlist
    {
        public string Name;
        public List<AudioFileInfo> FileList;

        public Playlist()
        {
        }

        public override string ToString()
        {
            return Name;
        }
    }
