using System;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Collections;
using NLog;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaServer
{
    public class HttpProcessor
    {
        public TcpClient socket;
        public HttpServer srv;

        private Stream inputStream;
        public StreamWriter outputStream;

        public String http_method;
        public String http_url;
        public String http_protocol_versionstring;
        public Hashtable httpHeaders = new Hashtable();

        readonly Logger log = LogManager.GetCurrentClassLogger();

        private static int MAX_POST_SIZE = 10 * 1024 * 1024; // 10MB

        public HttpProcessor(TcpClient s, HttpServer srv)
        {
            this.socket = s;
            this.srv = srv;
        }


        private string streamReadLine(Stream inputStream)
        {
            int next_char;
            string data = "";
            while (true)
            {
                next_char = inputStream.ReadByte();
                if (next_char == '\n') { break; }
                if (next_char == '\r') { continue; }
                if (next_char == -1) { Thread.Sleep(1); continue; };
                data += Convert.ToChar(next_char);
            }
            return data;
        }
        public void process()
        {
            // we can't use a StreamReader for input, because it buffers up extra data on us inside it's
            // "processed" view of the world, and we want the data raw after the headers
            inputStream = new BufferedStream(socket.GetStream());

            // we probably shouldn't be using a streamwriter for all output from handlers either
            outputStream = new StreamWriter(new BufferedStream(socket.GetStream()));
            try
            {
                parseRequest();
                readHeaders();
                if (http_method.Equals("GET"))
                {
                    handleGETRequest();
                }
                else if (http_method.Equals("POST"))
                {
                    handlePOSTRequest();
                }
                outputStream.Flush();
            }
            catch (Exception e)
            {
                log.Info("Exception: " + e.ToString());
                writeFailure();
            }
            // bs.Flush(); // flush any remaining output
            inputStream = null; outputStream = null; // bs = null;            
            socket.Close();
        }

        public void parseRequest()
        {
            String request = streamReadLine(inputStream);
            string[] tokens = request.Split(' ');
            if (tokens.Length != 3)
            {
                throw new Exception("invalid http request line");
            }
            http_method = tokens[0].ToUpper();
            http_url = tokens[1];
            http_protocol_versionstring = tokens[2];

            log.Info("starting: " + request);
        }

        public void readHeaders()
        {
            log.Info("readHeaders()");
            String line;
            while ((line = streamReadLine(inputStream)) != null)
            {
                if (line.Equals(""))
                {
                    log.Info("got headers");
                    return;
                }

                int separator = line.IndexOf(':');
                if (separator == -1)
                {
                    throw new Exception("invalid http header line: " + line);
                }
                String name = line.Substring(0, separator);
                int pos = separator + 1;
                while ((pos < line.Length) && (line[pos] == ' '))
                {
                    pos++; // strip any spaces
                }

                string value = line.Substring(pos, line.Length - pos);
                log.Info("header: {0}:{1}", name, value);
                httpHeaders[name] = value;
            }
        }

        public void handleGETRequest()
        {
            srv.handleGETRequest(this);
        }

        private const int BUF_SIZE = 4096;
        public void handlePOSTRequest()
        {
            // this post data processing just reads everything into a memory stream.
            // this is fine for smallish things, but for large stuff we should really
            // hand an input stream to the request processor. However, the input stream 
            // we hand him needs to let him see the "end of the stream" at this content 
            // length, because otherwise he won't know when he's seen it all! 

            log.Info("get post data start");
            int content_len = 0;
            MemoryStream ms = new MemoryStream();
            if (this.httpHeaders.ContainsKey("Content-Length"))
            {
                content_len = Convert.ToInt32(this.httpHeaders["Content-Length"]);
                if (content_len > MAX_POST_SIZE)
                {
                    throw new Exception(
                        String.Format("POST Content-Length({0}) too big for this simple server",
                          content_len));
                }
                byte[] buf = new byte[BUF_SIZE];
                int to_read = content_len;
                while (to_read > 0)
                {
                    log.Info("starting Read, to_read={0}", to_read);

                    int numread = this.inputStream.Read(buf, 0, Math.Min(BUF_SIZE, to_read));
                    log.Info("read finished, numread={0}", numread);
                    if (numread == 0)
                    {
                        if (to_read == 0)
                        {
                            break;
                        }
                        else
                        {
                            throw new Exception("client disconnected during post");
                        }
                    }
                    to_read -= numread;
                    ms.Write(buf, 0, numread);
                }
                ms.Seek(0, SeekOrigin.Begin);
            }
            log.Info("get post data end");
            srv.handlePOSTRequest(this, new StreamReader(ms));

        }

        public void writeSuccess(string content_type = "text/html")
        {
            outputStream.WriteLine("HTTP/1.0 200 OK");
            outputStream.WriteLine("Content-Type: " + content_type);
            outputStream.WriteLine("Connection: close");
            outputStream.WriteLine("");
            outputStream.Flush();
        }

        public void writeFailure()
        {
            outputStream.WriteLine("HTTP/1.0 404 File not found");
            outputStream.WriteLine("Connection: close");
            outputStream.WriteLine("");
            outputStream.Flush();
        }
    }

    public abstract class HttpServer
    {

        protected int port;
        TcpListener listener;
        bool is_active = false;
        public bool IsActive { get { return is_active; } }

        protected readonly Logger log = LogManager.GetCurrentClassLogger();

        public HttpServer(int port)
        {
            this.port = port;
        }

        public void Listen()
        {
            listener = new TcpListener(port);
            is_active = true;
            listener.Start();
            while (is_active)
            {
                try
                {
                    TcpClient s = listener.AcceptTcpClient();
                    HttpProcessor processor = new HttpProcessor(s, this);
                    ThreadPool.QueueUserWorkItem(o => processor.process());
                    Thread.Sleep(1);
                }
                catch { }
            }
        }

        public void StopListening()
        {
            is_active = false;
            listener.Stop();
        }

        public abstract void handleGETRequest(HttpProcessor p);
        public abstract void handlePOSTRequest(HttpProcessor p, StreamReader inputData);
    }

    public class MediaServer : HttpServer
    {
        public MediaServer(int port) : base(port) { }

        public override void handleGETRequest(HttpProcessor p)
        {
            try
            {
                string request = p.http_url.Remove(0, 1);
                string[] parameters = request.Split('&');
                if (parameters.Length > 0)
                {

                    if (parameters[0].IndexOf("method_name") != -1)
                    {
                        string[] method = parameters[0].Split('=');
                        switch (method[1])
                        {
                            case "get_playlists": ProcessGetPlaylists(p); break;
                            case "get_playlist":
                                {
                                    ProcessGetPlaylist(parameters[1].Split('='), p);
                                }
                                break;
                            default: break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error on handle GET request! Info:\n", ex);
            }
        }

        void ProcessGetPlaylists(HttpProcessor p)
        {
            try
            {
                p.writeSuccess("text/xml");
                XmlSerializer sr = new XmlSerializer(typeof(List<Playlist>));
                sr.Serialize(p.outputStream.BaseStream, ServerData.Instance.playlistManager.playlistCollection.Values.ToList());
                XmlSerializer ser = new XmlSerializer(typeof(List<Playlist>));
            }
            catch (Exception ex)
            {
                log.Error("ProcessGetPlaylists() error! Info:\n", ex);
                p.writeFailure();
            }
        }

        void ProcessGetPlaylist(string[] parameters, HttpProcessor p)
        {
            try
            {
                if (ServerData.Instance.playlistManager.playlistCollection.ContainsKey(parameters[1]))
                {
                    p.writeSuccess("text/xml");
                    XmlSerializer sr = new XmlSerializer(typeof(Playlist));
                    sr.Serialize(p.outputStream.BaseStream, ServerData.Instance.playlistManager.playlistCollection[parameters[1]]);
                }
                else
                    p.writeFailure();
            }
            catch (Exception ex)
            {
                log.Error("Error on process GetPlaylist! Info:\n", ex);
            }
        }

        public override void handlePOSTRequest(HttpProcessor p, StreamReader inputData)
        {

        }
    }

    public class FileServer
    {
        TcpListener listener;
        int bufferSize = 65536;
        public static FileServer Instance { get; private set; }
        Thread workThread;
        private bool acceptConnections = false;
        public static void Init(int port)
        {
            Instance = new FileServer(port);
        }

        private FileServer(int port)
        {
            listener = new TcpListener(port);
            listener.Start();
            StartListening();
        }

        public void StartListening()
        {
            acceptConnections = true;
            workThread = new Thread(HandleConnection);
            workThread.Start();
        }

        ~FileServer()
        {
            StopListening();
        }

        public void StopListening()
        {
            acceptConnections = false;
            listener.Stop();
            workThread.Abort();
        }

        void HandleConnection()
        {
            while (acceptConnections)
            {
                try
                {
                    var client = listener.AcceptTcpClient();
                    ThreadPool.QueueUserWorkItem(o => ProcessClient(client));
                }
                catch { }
            }
        }

        void ProcessClient(TcpClient client)
        {
            var clientStream = client.GetStream();
            FileStream fileStream = null;
            try
            {
                //byte[] buffer = new byte[bufferSize];
                //int readCount = 0;
                //while ((readCount = clientStream.Read(buffer, 0, bufferSize)) > 0)
                {
                    string request = "";
                    byte[] buffer = new byte[bufferSize];
                    int readCount = 0;
                    readCount = clientStream.Read(buffer, 0, bufferSize);
                    request = Encoding.UTF8.GetString(buffer, 0, readCount);
                    //if (request != "<EndOfSession>")
                    {
                        if (File.Exists(request))
                        {
                            fileStream = File.OpenRead(request);
                            clientStream.Write(new byte[] { Convert.ToByte(true) }, 0, 1);
                            while ((readCount = fileStream.Read(buffer, 0, bufferSize)) > 0)
                                clientStream.Write(buffer, 0, readCount);
                            clientStream.Flush();
                            fileStream.Close();
                        }
                    }
                }
                //client.Close();
            }
            catch (Exception ex)
            {
                if (fileStream != null)
                    fileStream.Close();
                try
                {
                    clientStream.Write(new byte[] { Convert.ToByte(false) }, 0, 1);
                }
                catch { }
            }
        }
    }
}
