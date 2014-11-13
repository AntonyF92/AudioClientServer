using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace SampleClient
{
    public class ClientPeer
    {
        TcpClient client = new TcpClient();
        System.Timers.Timer serviceTimer = new System.Timers.Timer(5);
        Form1 app = null;

        public ClientPeer(Form1 form)
        {
            this.app = form;
            client.Connect("localhost", 10000);
            serviceTimer.Elapsed += serviceTimer_Elapsed;
            serviceTimer.Start();
        }

        void serviceTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            serviceTimer.Stop();
            serviceTimer.Start();
        }
    }
}
