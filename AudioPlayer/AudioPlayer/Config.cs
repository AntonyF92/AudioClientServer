using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AudioPlayer
{
    public class Config
    {
        public int http_port = 11000;
        public int tcp_port = 10000;
        public string audio_server_dns = "localhost";
        public int audio_buffer_size = 65536;
        public List<string> possible_hosts = new List<string>();
        public int audio_port = 12000;
    }
}
