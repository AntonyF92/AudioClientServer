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
        public Dictionary<string, Playlist> playlistCollection = new Dictionary<string, Playlist>();

        public delegate void CollectionLoadEventHandler(Dictionary<string, Playlist> playlistCollection);
        public event CollectionLoadEventHandler OnCollectionLoadEvent;
        public delegate void ChangeTrackEventHandler(Playlist pl, AudioFileInfo fileInfo);
        public event ChangeTrackEventHandler OnChangeTrackEvent;

        public Playlist this[string name]
        {
            get
            {
                if (playlistCollection.ContainsKey(name))
                    return playlistCollection[name];
                return null;
            }
        }

        public PlaylistManager()
        {
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
            LoadCollection(list);
        }

        public void LoadCollection(List<Playlist> list)
        {
            foreach (var pl in list)
                playlistCollection.Add(pl.Name, pl);
            if (OnCollectionLoadEvent != null)
                OnCollectionLoadEvent(playlistCollection);
        }

        public void UpdatePlaylist(Playlist pl)
        {
            if (playlistCollection.ContainsKey(pl.Name))
                playlistCollection[pl.Name] = pl;
        }


        public void ChangeTrack(Playlist pl, AudioFileInfo fi)
        {
            if (OnChangeTrackEvent != null)
                OnChangeTrackEvent(pl, fi);
        }

    }
}
