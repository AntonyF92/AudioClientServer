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
using NAudio;
using System.IO;
using System.Threading;
using NAudio.Wave;

namespace SampleClient
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private WaveOut player = null;

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
            {
                PlaylistBox.BeginUpdate();
                PlaylistBox.Items.Clear();
                foreach (var item in pl.FileList)
                {
                    var plItem = PlaylistBox.Items.Add(item.name);
                    plItem.Tag = item;
                    plItem.Checked = true;
                    while (PlaylistBox.Bounds.Width - plItem.Bounds.Width > 2)
                        plItem.Text += " ";
                }
                PlaylistBox.EndUpdate();
            }));
        }

        public void PlayMp3FromStream(Stream stream)
        {
            Stream ms = new MemoryStream();
            new Thread(delegate(object o)
            {
                    byte[] buffer = new byte[65536]; // 64KB chunks
                    int read;
                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        var pos = ms.Position;
                        ms.Position = ms.Length;
                        ms.Write(buffer, 0, read);
                        ms.Position = pos;
                    }
            }).Start();

            // Pre-buffering some data to allow NAudio to start playing
            while (ms.Length < 65536 * 10)
                Thread.Sleep(1000);

            ms.Position = 0;
            using (WaveStream blockAlignedStream = new BlockAlignReductionStream(WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(ms))))
            {
                StopPlayer();
                player = new WaveOut(WaveCallbackInfo.FunctionCallback());
                {
                    player.Init(blockAlignedStream);
                    player.Play();
                    while (player.PlaybackState == PlaybackState.Playing)
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                    ms.Flush();
                }
            }
        }

        private void Play_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                NetworkFileInfo fi = null;
                PlaylistBox.Invoke(new Action(() =>
                    fi = (NetworkFileInfo)PlaylistBox.SelectedItems[0].Tag));
                TcpClient client = new TcpClient();
                client.Connect("localhost", 10000);
                var stream = client.GetStream();
                byte[] data = Encoding.UTF8.GetBytes(fi.path);
                stream.Write(data, 0, data.Length);
                stream.Flush();
                while (!client.GetStream().DataAvailable)
                {
                    Thread.Sleep(10);
                }
                PlayMp3FromStream(client.GetStream());
                //client.Close();
            });
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            
        }

        void StopPlayer()
        {
            if (player != null && player.PlaybackState == PlaybackState.Playing)
                player.Stop();
        }
    }
}
