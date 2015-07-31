using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace SampleClient
{
    public class HttpClient
    {
        private int port;
        private string http_server_dns;

        public HttpClient(string host, int port)
        {
            this.http_server_dns = host;
            this.port = port;
        }

        public void ExecGETquery(string url, Action<HttpWebResponse> responseHandler)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(string.Format("http://{0}:{1}/{2}", http_server_dns, port, url));
            request.Method = "GET";
            request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version10;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (responseHandler != null)
                responseHandler(response);
            response.Close();
        }
    }
}
