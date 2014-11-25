﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MediaServer
{
    public partial class PlaylistElement : UserControl
    {
        public static PlaylistElement selectedItem = null;

        AudioFileInfo fileInfo = null;
        public AudioFileInfo FileInfo { get { return fileInfo; } }
        public bool IsActive { get; private set; }
        public string PerformerString { get { return fileInfo.singer; } }
        public string TitleString { get { return string.Format("{0} - {1}", fileInfo.singer, fileInfo.song); } }
        public string DurationString { get { return TimeSpan.FromSeconds(fileInfo.length).ToString(); } }
        public string Properties { get { return string.Format("{0} :: {1} kHz|{2} kbps|{3} MB", Path.GetExtension(fileInfo.path), fileInfo.frequency, fileInfo.bitrate, fileInfo.size); } }

        Color defaultColor = SystemColors.GradientInactiveCaption;
        Color focusColor = SystemColors.ActiveCaption;

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
            this.BackColor = focusColor;
            selectedItem = this;
        }

        public void SetSelected(bool value)
        {
            IsActive = value;
        }

        public void SetDefaultColor(Color newColor)
        {
            defaultColor = newColor;
            if (this.BackColor != focusColor)
                this.BackColor = defaultColor;
        }

        private void PlaylistElement_Leave(object sender, EventArgs e)
        {
            this.BackColor = defaultColor;
        }
    }
}
