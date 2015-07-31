using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace AudioPlayer
{
    public enum ConfigFields
    {
        http_port,
        audio_port,
        audio_buffer_size,
        tcp_port
    }

    public class ConfigManager
    {
        public Config config { get; private set; }
        private string configFile = Path.Combine(Environment.CurrentDirectory, "config.xml");

        public static ConfigManager Instance { get; private set; }

        public static void Init()
        {
            Instance = new ConfigManager();
        }

        private ConfigManager()
        {
            LoadConfig();
        }

        public void LoadConfig()
        {
            try
            {
                XmlSerializer sr = new XmlSerializer(typeof(Config));
                Stream stream = File.Open(configFile, FileMode.Open);
                config = new Config();
                config = (Config)sr.Deserialize(stream);
                stream.Close();
            }
            catch (Exception ex)
            {
                config = new Config();
            }
        }

        public void SaveConfig()
        {
            try
            {
                XmlSerializer sr = new XmlSerializer(typeof(Config));
                var file = File.Create(configFile);
                sr.Serialize(file, config);
                file.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public string GetStringOption(string name)
        {
            try
            {
                return config.GetType().GetProperty(name).GetValue(config).ToString();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int GetIntOption(string name)
        {
            return int.Parse(GetStringOption(name));
        }

        public void SetHost(string host)
        {
            config.audio_server_dns = host;
            if (!config.possible_hosts.Contains(host))
                config.possible_hosts.Add(host);
        }

        public void SetIntOption(ConfigFields field, string value)
        {
            int v = int.Parse(value);
            switch (field)
            {
                case ConfigFields.audio_buffer_size: config.audio_buffer_size = v; break;
                case ConfigFields.audio_port: config.audio_port = v; break;
                case ConfigFields.http_port: config.http_port = v; break;
                case ConfigFields.tcp_port: config.tcp_port = v; break;
            }
        }
    }
}
