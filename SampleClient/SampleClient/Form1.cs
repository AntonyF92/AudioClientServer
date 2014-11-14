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

        private Stream ms = new MemoryStream();
        public void PlayMp3FromUrl(string url)
        {
            new Thread(delegate(object o)
            {
                var response = WebRequest.Create(url).GetResponse();
                using (var stream = response.GetResponseStream())
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
                }
            }).Start();

            // Pre-buffering some data to allow NAudio to start playing
            while (ms.Length < 65536 * 10)
                Thread.Sleep(1000);

            ms.Position = 0;
            using (WaveStream blockAlignedStream = new BlockAlignReductionStream(WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(ms))))
            {
                using (WaveOut waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
                {
                    waveOut.Init(blockAlignedStream);
                    waveOut.Play();
                    while (waveOut.PlaybackState == PlaybackState.Playing)
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }
        }

        private void Play_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(o =>
            PlayMp3FromUrl("http://10.15.10.55:8080/test/Daughtry - Drown In You.mp3"));
        }
    }
}
