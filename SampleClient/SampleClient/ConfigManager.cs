using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace SampleClient
{
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

    }
}
