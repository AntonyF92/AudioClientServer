using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlaylistControls;

namespace AudioPlayer
{
    public static class Extensions
    {
        public static string GetURL(this AudioFileInfo info)
        {
            return $"http://{ConfigManager.Instance.config.audio_server_dns}:{ConfigManager.Instance.config.audio_port}/?{info.path}";
        }

    }
}
