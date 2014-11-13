using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace MediaServer
{
    public class PeerBase
    {
        private static int id = 0;

        public int ID { get; private set; }
        protected int bufferSize = 1024;
        protected TcpClient tcpClient;

        System.Timers.Timer serviceTimer;
        int interval = 5;

        public PeerBase(TcpClient tcpClient)
        {
            ID = id++;
            this.tcpClient = tcpClient;
            serviceTimer = new System.Timers.Timer(interval);
            serviceTimer.Elapsed += serviceTimer_Elapsed;
            serviceTimer.Start();
        }

        void serviceTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            serviceTimer.Stop();
            try
            {
                var stream = tcpClient.GetStream();
                byte[] data = new byte[bufferSize];
                stream.Read(data, 0, bufferSize);
                byte code = data[0];
                byte[] buffer = new byte[bufferSize - 1];
                data.CopyTo(buffer, 1);
                OnReceiveData(code, buffer);
            }
            catch (Exception ex)
            {
            }
            serviceTimer.Start();
        }

        public virtual void OnReceiveData(byte code, byte[] data)
        {
            
        }

        public void SendData(byte code, byte[] data)
        {
            List<byte> d = new List<byte>();
            d.Add(code);
            d.AddRange(data);
            tcpClient.GetStream().Write(d.ToArray(), 0, d.Count);
            tcpClient.GetStream().Flush();
        }

        public void SendData(byte[] data)
        {
            tcpClient.GetStream().Write(data, 0, data.Length);
            tcpClient.GetStream().Flush();
        }
    }
}
