using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PlaylistControls
{
    public partial class PlaylistElement : UserControl
    {
        public static PlaylistElement selectedItem = null;
        public static PlaylistElement activeElement = null;

        AudioFileInfo fileInfo = null;
        public AudioFileInfo FileInfo { get { return fileInfo; } }
        public bool IsActive { get; private set; }
        public string PerformerString { get { return fileInfo.singer; } }
        public string TitleString
        {
            get
            {
                if (!string.IsNullOrEmpty(fileInfo.singer) && !string.IsNullOrEmpty(fileInfo.song))
                    return string.Format("{0} - {1}", fileInfo.singer, fileInfo.song);
                else
                    return fileInfo.name;
            }
        }
        public string DurationString
        {
            get
            {
                TimeSpan time = TimeSpan.FromSeconds(fileInfo.length);
                if (time.Hours == 0)
                    return time.ToString(@"mm\:ss");
                else
                    return time.ToString(@"hh\:mm\:ss");
            }
        }
        public string Properties { get { return string.Format("{0} :: {1} kHz|{2} kbps|{3} MB", Path.GetExtension(fileInfo.path).Replace(".", ""), fileInfo.frequency / 1000, fileInfo.bitrate, fileInfo.size); } }

        public event MouseEventHandler ElementDoubleClick
        {
            add
            {
                this.MouseDoubleClick += value;
                this.Performer.MouseDoubleClick += value;
                this.Duration.MouseDoubleClick += value;
                this.Info.MouseDoubleClick += value;
            }

            remove
            {
                this.MouseDoubleClick -= value;
                this.Performer.MouseDoubleClick -= value;
                this.Duration.MouseDoubleClick -= value;
                this.Info.MouseDoubleClick -= value;
            }
        }

        Color defaultColor = SystemColors.GradientInactiveCaption;
        Color focusColor = SystemColors.ActiveCaption;
        Color activeColor = SystemColors.GradientActiveCaption;

        public PlaylistElement(AudioFileInfo fileInfo)
        {
            InitializeComponent();
            this.fileInfo = fileInfo;
        }

        private void PlaylistElement_Load(object sender, EventArgs e)
        {
            this.Performer.Text = TitleString;
            this.Duration.Text = DurationString;
            this.Info.Text = Properties;
        }

        private void PlaylistElement_MouseClick(object sender, MouseEventArgs e)
        {
            if (selectedItem != null)
                selectedItem.OnLeave(null);
            this.BackColor = focusColor;
            selectedItem = this;
            this.Focus();
        }

        public void SetSelected(bool value)
        {
            IsActive = value;
        }

        public void SetColor(Color newColor)
        {
            defaultColor = newColor;
            if (this.BackColor != focusColor)
                this.BackColor = defaultColor;
        }

        private void PlaylistElement_Leave(object sender, EventArgs e)
        {
            this.BackColor = defaultColor;
        }

        public void SetActive()
        {
            if (activeElement != null)
                activeElement.BackColor = defaultColor;
            this.BackColor = activeColor;
            activeElement = this;
        }

    }
}
