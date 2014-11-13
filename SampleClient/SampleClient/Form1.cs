using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaServer;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace SampleClient
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://localhost:11000/method_name=get_playlist&name=new");
                request.Method = "GET";
                request.KeepAlive = false;
                request.ProtocolVersion = HttpVersion.Version10;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                XmlSerializer sr = new XmlSerializer(typeof(Playlist));
                Playlist pl = new Playlist();
                pl = (Playlist)sr.Deserialize(response.GetResponseStream());
                LoadList(pl);
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadList(Playlist pl)
        {

            PlaylistBox.Invoke(new Action(() =>
            PlaylistBox.Items.Clear()));
            PlaylistBox.Invoke(new Action(() =>
            PlaylistBox.Items.AddRange(pl.FileList.ToArray())));
        }
    }
}
