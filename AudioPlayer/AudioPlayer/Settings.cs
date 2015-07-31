using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioPlayer
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            foreach (string host in ConfigManager.Instance.config.possible_hosts)
                ServerList.Items.Add(host);
            if (!ConfigManager.Instance.config.possible_hosts.Contains(ConfigManager.Instance.config.audio_server_dns))
                ServerList.Items.Add(ConfigManager.Instance.config.audio_server_dns);
            ServerList.SelectedItem = ConfigManager.Instance.config.audio_server_dns;
            TcpPort.Text = ConfigManager.Instance.config.tcp_port.ToString();
            HttpPort.Text = ConfigManager.Instance.config.http_port.ToString();
            BufferSize.Text = ConfigManager.Instance.config.audio_buffer_size.ToString();
        }

        private void TcpPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = false;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ServerList.Text))
                    throw new ArgumentNullException("Audio server address");
                if (string.IsNullOrEmpty(HttpPort.Text))
                    throw new ArgumentNullException("Http port");
                if (string.IsNullOrEmpty(TcpPort.Text))
                    throw new ArgumentNullException("TCP port");
                if (string.IsNullOrEmpty(BufferSize.Text))
                    throw new ArgumentNullException("Buffer size");
                ConfigManager.Instance.SetHost(ServerList.Text);
                ConfigManager.Instance.SetIntOption(ConfigFields.http_port, HttpPort.Text);
                ConfigManager.Instance.SetIntOption(ConfigFields.audio_port, TcpPort.Text);
                ConfigManager.Instance.SetIntOption(ConfigFields.audio_buffer_size, BufferSize.Text);
                ConfigManager.Instance.SaveConfig();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        int iFormX, iFormY, iMouseX, iMouseY;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            iFormX = this.Location.X;
            iFormY = this.Location.Y;
            iMouseX = MousePosition.X;
            iMouseY = MousePosition.Y;

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            int iMouseX2 = MousePosition.X;
            int iMouseY2 = MousePosition.Y;
            if (e.Button == MouseButtons.Left)
                this.Location = new Point(iFormX + (iMouseX2 - iMouseX), iFormY + (iMouseY2 - iMouseY));

        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
