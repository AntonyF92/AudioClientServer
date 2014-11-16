using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace MediaServer
{
    public class PlaylistCollection : Dictionary<string, Playlist>
    {
    }
    public class FolderInfo
    {
        public string name;
        public string path;
        public override string ToString()
        {
            return path;
        }
    }
    public class ServerData
    {
        public PlaylistCollection Playlists = new PlaylistCollection();
        public List<FolderInfo> SelectedFolders = new List<FolderInfo>();

        public static ServerData Instance { get; private set; }
        public static void Init()
        {
            Instance = new ServerData();
        }

        private ServerData()
        {
            if (Directory.Exists(Path.Combine(Environment.CurrentDirectory, ServerSettings.Default.PlaylistFolder)))
            {
                foreach (string file in Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, ServerSettings.Default.PlaylistFolder)))
                {
                    XmlSerializer sr = new XmlSerializer(typeof(Playlist));
                    StreamReader stream = new StreamReader(file);
                    Playlist tmp = new Playlist();
                    tmp = (Playlist)sr.Deserialize(stream);
                    Playlists.Add(tmp.Name, tmp);
                    stream.Close();
                }
            }
            else
                Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, ServerSettings.Default.PlaylistFolder));
        }

        public void SavePlaylists()
        {
            string basePath = Path.Combine(Environment.CurrentDirectory, ServerSettings.Default.PlaylistFolder);
            foreach (var pl in Playlists.Values)
            {
                XmlSerializer sr = new XmlSerializer(typeof(Playlist));
                var file = File.Create(Path.Combine(basePath, pl.Name));
                sr.Serialize(file, pl);
                file.Close();
            }
        }
    }
}
