namespace AudioPlayer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SongInfoContainer = new System.Windows.Forms.GroupBox();
            this.SongDuration = new System.Windows.Forms.Label();
            this.SongAlbum = new System.Windows.Forms.Label();
            this.SongPerformer = new System.Windows.Forms.Label();
            this.SongTitle = new System.Windows.Forms.Label();
            this.PlaylistsContainer = new System.Windows.Forms.GroupBox();
            this.PlaylistCollectionWindow = new System.Windows.Forms.TabControl();
            this.PlaybackControlsContainer = new System.Windows.Forms.GroupBox();
            this.RepeatButton = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.VolumeBar = new PlaylistControls.SmoothProgressBar();
            this.TotalTime = new System.Windows.Forms.Label();
            this.RandomButton = new System.Windows.Forms.RadioButton();
            this.PlaybackProgress = new PlaylistControls.SmoothProgressBar();
            this.PlaybackTime = new System.Windows.Forms.Label();
            this.Stop = new System.Windows.Forms.Button();
            this.PrevTrack = new System.Windows.Forms.Button();
            this.NextTrack = new System.Windows.Forms.Button();
            this.Play = new System.Windows.Forms.Button();
            this.Pause = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.applicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MinimizeButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SongYear = new System.Windows.Forms.Label();
            this.SongParameters = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SongInfoContainer.SuspendLayout();
            this.PlaylistsContainer.SuspendLayout();
            this.PlaybackControlsContainer.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Magneto", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 19);
            this.label1.TabIndex = 21;
            this.label1.Text = "NetPlayer v1.0";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.SongInfoContainer);
            this.panel1.Controls.Add(this.PlaylistsContainer);
            this.panel1.Controls.Add(this.PlaybackControlsContainer);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Location = new System.Drawing.Point(3, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(663, 661);
            this.panel1.TabIndex = 18;
            // 
            // SongInfoContainer
            // 
            this.SongInfoContainer.Controls.Add(this.SongParameters);
            this.SongInfoContainer.Controls.Add(this.SongYear);
            this.SongInfoContainer.Controls.Add(this.SongDuration);
            this.SongInfoContainer.Controls.Add(this.SongAlbum);
            this.SongInfoContainer.Controls.Add(this.SongPerformer);
            this.SongInfoContainer.Controls.Add(this.SongTitle);
            this.SongInfoContainer.Location = new System.Drawing.Point(15, 133);
            this.SongInfoContainer.Name = "SongInfoContainer";
            this.SongInfoContainer.Size = new System.Drawing.Size(142, 515);
            this.SongInfoContainer.TabIndex = 17;
            this.SongInfoContainer.TabStop = false;
            // 
            // SongDuration
            // 
            this.SongDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SongDuration.ForeColor = System.Drawing.SystemColors.Window;
            this.SongDuration.Location = new System.Drawing.Point(6, 126);
            this.SongDuration.Name = "SongDuration";
            this.SongDuration.Size = new System.Drawing.Size(130, 23);
            this.SongDuration.TabIndex = 3;
            // 
            // SongAlbum
            // 
            this.SongAlbum.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SongAlbum.ForeColor = System.Drawing.SystemColors.Window;
            this.SongAlbum.Location = new System.Drawing.Point(6, 80);
            this.SongAlbum.Name = "SongAlbum";
            this.SongAlbum.Size = new System.Drawing.Size(130, 23);
            this.SongAlbum.TabIndex = 2;
            this.SongAlbum.MouseHover += new System.EventHandler(this.SongTitle_MouseHover_1);
            // 
            // SongPerformer
            // 
            this.SongPerformer.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SongPerformer.ForeColor = System.Drawing.SystemColors.Window;
            this.SongPerformer.Location = new System.Drawing.Point(6, 59);
            this.SongPerformer.Name = "SongPerformer";
            this.SongPerformer.Size = new System.Drawing.Size(130, 21);
            this.SongPerformer.TabIndex = 1;
            this.SongPerformer.MouseHover += new System.EventHandler(this.SongTitle_MouseHover_1);
            // 
            // SongTitle
            // 
            this.SongTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SongTitle.ForeColor = System.Drawing.SystemColors.Window;
            this.SongTitle.Location = new System.Drawing.Point(6, 26);
            this.SongTitle.Name = "SongTitle";
            this.SongTitle.Size = new System.Drawing.Size(130, 33);
            this.SongTitle.TabIndex = 0;
            this.SongTitle.MouseHover += new System.EventHandler(this.SongTitle_MouseHover_1);
            // 
            // PlaylistsContainer
            // 
            this.PlaylistsContainer.Controls.Add(this.PlaylistCollectionWindow);
            this.PlaylistsContainer.Location = new System.Drawing.Point(163, 133);
            this.PlaylistsContainer.Name = "PlaylistsContainer";
            this.PlaylistsContainer.Size = new System.Drawing.Size(487, 515);
            this.PlaylistsContainer.TabIndex = 16;
            this.PlaylistsContainer.TabStop = false;
            // 
            // PlaylistCollectionWindow
            // 
            this.PlaylistCollectionWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlaylistCollectionWindow.Location = new System.Drawing.Point(3, 16);
            this.PlaylistCollectionWindow.Name = "PlaylistCollectionWindow";
            this.PlaylistCollectionWindow.SelectedIndex = 0;
            this.PlaylistCollectionWindow.Size = new System.Drawing.Size(481, 496);
            this.PlaylistCollectionWindow.TabIndex = 11;
            // 
            // PlaybackControlsContainer
            // 
            this.PlaybackControlsContainer.Controls.Add(this.RepeatButton);
            this.PlaybackControlsContainer.Controls.Add(this.label3);
            this.PlaybackControlsContainer.Controls.Add(this.VolumeBar);
            this.PlaybackControlsContainer.Controls.Add(this.TotalTime);
            this.PlaybackControlsContainer.Controls.Add(this.RandomButton);
            this.PlaybackControlsContainer.Controls.Add(this.PlaybackProgress);
            this.PlaybackControlsContainer.Controls.Add(this.PlaybackTime);
            this.PlaybackControlsContainer.Controls.Add(this.Stop);
            this.PlaybackControlsContainer.Controls.Add(this.PrevTrack);
            this.PlaybackControlsContainer.Controls.Add(this.NextTrack);
            this.PlaybackControlsContainer.Controls.Add(this.Play);
            this.PlaybackControlsContainer.Controls.Add(this.Pause);
            this.PlaybackControlsContainer.Controls.Add(this.label2);
            this.PlaybackControlsContainer.Location = new System.Drawing.Point(15, 27);
            this.PlaybackControlsContainer.Name = "PlaybackControlsContainer";
            this.PlaybackControlsContainer.Size = new System.Drawing.Size(635, 100);
            this.PlaybackControlsContainer.TabIndex = 15;
            this.PlaybackControlsContainer.TabStop = false;
            // 
            // RepeatButton
            // 
            this.RepeatButton.AutoCheck = false;
            this.RepeatButton.AutoSize = true;
            this.RepeatButton.ForeColor = System.Drawing.SystemColors.Window;
            this.RepeatButton.Location = new System.Drawing.Point(349, 47);
            this.RepeatButton.Name = "RepeatButton";
            this.RepeatButton.Size = new System.Drawing.Size(81, 17);
            this.RepeatButton.TabIndex = 27;
            this.RepeatButton.TabStop = true;
            this.RepeatButton.Text = "Repeat one";
            this.RepeatButton.UseVisualStyleBackColor = true;
            this.RepeatButton.CheckedChanged += new System.EventHandler(this.RepeatButton_CheckedChanged);
            this.RepeatButton.Click += new System.EventHandler(this.RepeatButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Window;
            this.label3.Location = new System.Drawing.Point(15, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Volume";
            // 
            // VolumeBar
            // 
            this.VolumeBar.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.VolumeBar.Location = new System.Drawing.Point(63, 73);
            this.VolumeBar.Maximum = 100;
            this.VolumeBar.Minimum = 0;
            this.VolumeBar.Name = "VolumeBar";
            this.VolumeBar.ProgressBarColor = System.Drawing.Color.CornflowerBlue;
            this.VolumeBar.Size = new System.Drawing.Size(97, 13);
            this.VolumeBar.TabIndex = 25;
            this.VolumeBar.Value = 100;
            this.VolumeBar.Load += new System.EventHandler(this.VolumeBar_Load);
            this.VolumeBar.Click += new System.EventHandler(this.VolumeBar_Click);
            // 
            // TotalTime
            // 
            this.TotalTime.ForeColor = System.Drawing.SystemColors.Window;
            this.TotalTime.Location = new System.Drawing.Point(595, 44);
            this.TotalTime.Name = "TotalTime";
            this.TotalTime.Size = new System.Drawing.Size(34, 23);
            this.TotalTime.TabIndex = 23;
            this.TotalTime.Text = "00:00";
            this.TotalTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RandomButton
            // 
            this.RandomButton.AutoCheck = false;
            this.RandomButton.AutoSize = true;
            this.RandomButton.ForeColor = System.Drawing.SystemColors.Window;
            this.RandomButton.Location = new System.Drawing.Point(240, 47);
            this.RandomButton.Name = "RandomButton";
            this.RandomButton.Size = new System.Drawing.Size(103, 17);
            this.RandomButton.TabIndex = 22;
            this.RandomButton.TabStop = true;
            this.RandomButton.Text = "Random (on/off)";
            this.RandomButton.UseVisualStyleBackColor = true;
            this.RandomButton.CheckedChanged += new System.EventHandler(this.RandomButton_CheckedChanged);
            this.RandomButton.Click += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // PlaybackProgress
            // 
            this.PlaybackProgress.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.PlaybackProgress.Location = new System.Drawing.Point(10, 19);
            this.PlaybackProgress.Maximum = 100;
            this.PlaybackProgress.Minimum = 0;
            this.PlaybackProgress.Name = "PlaybackProgress";
            this.PlaybackProgress.ProgressBarColor = System.Drawing.Color.CornflowerBlue;
            this.PlaybackProgress.Size = new System.Drawing.Size(619, 10);
            this.PlaybackProgress.TabIndex = 21;
            this.PlaybackProgress.Value = 0;
            this.PlaybackProgress.Click += new System.EventHandler(this.PlaybackProgress_Click);
            // 
            // PlaybackTime
            // 
            this.PlaybackTime.BackColor = System.Drawing.Color.Transparent;
            this.PlaybackTime.ForeColor = System.Drawing.SystemColors.Window;
            this.PlaybackTime.Location = new System.Drawing.Point(550, 44);
            this.PlaybackTime.Name = "PlaybackTime";
            this.PlaybackTime.Size = new System.Drawing.Size(35, 23);
            this.PlaybackTime.TabIndex = 20;
            this.PlaybackTime.Text = "00:00";
            this.PlaybackTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Stop
            // 
            this.Stop.BackColor = System.Drawing.Color.Transparent;
            this.Stop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Stop.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.Stop.FlatAppearance.BorderSize = 0;
            this.Stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Stop.Image = ((System.Drawing.Image)(resources.GetObject("Stop.Image")));
            this.Stop.Location = new System.Drawing.Point(56, 44);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(40, 23);
            this.Stop.TabIndex = 19;
            this.Stop.TabStop = false;
            this.Stop.UseVisualStyleBackColor = false;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            this.Stop.MouseEnter += new System.EventHandler(this.Play_MouseEnter);
            this.Stop.MouseLeave += new System.EventHandler(this.Play_MouseLeave);
            // 
            // PrevTrack
            // 
            this.PrevTrack.BackColor = System.Drawing.Color.Transparent;
            this.PrevTrack.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.PrevTrack.FlatAppearance.BorderSize = 0;
            this.PrevTrack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PrevTrack.Image = ((System.Drawing.Image)(resources.GetObject("PrevTrack.Image")));
            this.PrevTrack.Location = new System.Drawing.Point(148, 44);
            this.PrevTrack.Name = "PrevTrack";
            this.PrevTrack.Size = new System.Drawing.Size(40, 23);
            this.PrevTrack.TabIndex = 17;
            this.PrevTrack.TabStop = false;
            this.PrevTrack.UseVisualStyleBackColor = false;
            this.PrevTrack.Click += new System.EventHandler(this.PrevTrack_Click);
            this.PrevTrack.MouseEnter += new System.EventHandler(this.Play_MouseEnter);
            this.PrevTrack.MouseLeave += new System.EventHandler(this.Play_MouseLeave);
            // 
            // NextTrack
            // 
            this.NextTrack.BackColor = System.Drawing.Color.Transparent;
            this.NextTrack.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.NextTrack.FlatAppearance.BorderSize = 0;
            this.NextTrack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NextTrack.Image = ((System.Drawing.Image)(resources.GetObject("NextTrack.Image")));
            this.NextTrack.Location = new System.Drawing.Point(194, 44);
            this.NextTrack.Name = "NextTrack";
            this.NextTrack.Size = new System.Drawing.Size(40, 23);
            this.NextTrack.TabIndex = 16;
            this.NextTrack.TabStop = false;
            this.NextTrack.UseVisualStyleBackColor = false;
            this.NextTrack.Click += new System.EventHandler(this.NextTrack_Click);
            this.NextTrack.MouseEnter += new System.EventHandler(this.Play_MouseEnter);
            this.NextTrack.MouseLeave += new System.EventHandler(this.Play_MouseLeave);
            // 
            // Play
            // 
            this.Play.BackColor = System.Drawing.Color.Transparent;
            this.Play.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Play.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.Play.FlatAppearance.BorderSize = 0;
            this.Play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Play.Image = ((System.Drawing.Image)(resources.GetObject("Play.Image")));
            this.Play.Location = new System.Drawing.Point(10, 44);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(40, 23);
            this.Play.TabIndex = 15;
            this.Play.TabStop = false;
            this.Play.UseVisualStyleBackColor = false;
            this.Play.Click += new System.EventHandler(this.Play_Click);
            this.Play.MouseEnter += new System.EventHandler(this.Play_MouseEnter);
            this.Play.MouseLeave += new System.EventHandler(this.Play_MouseLeave);
            // 
            // Pause
            // 
            this.Pause.BackColor = System.Drawing.Color.Transparent;
            this.Pause.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.Pause.FlatAppearance.BorderSize = 0;
            this.Pause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Pause.Image = ((System.Drawing.Image)(resources.GetObject("Pause.Image")));
            this.Pause.Location = new System.Drawing.Point(102, 44);
            this.Pause.Name = "Pause";
            this.Pause.Size = new System.Drawing.Size(40, 23);
            this.Pause.TabIndex = 18;
            this.Pause.TabStop = false;
            this.Pause.UseVisualStyleBackColor = false;
            this.Pause.Click += new System.EventHandler(this.Pause_Click);
            this.Pause.MouseEnter += new System.EventHandler(this.Play_MouseEnter);
            this.Pause.MouseLeave += new System.EventHandler(this.Play_MouseLeave);
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(573, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 23);
            this.label2.TabIndex = 24;
            this.label2.Text = "/";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Window;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(663, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // applicationToolStripMenuItem
            // 
            this.applicationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.applicationToolStripMenuItem.ForeColor = System.Drawing.SystemColors.WindowText;
            this.applicationToolStripMenuItem.Name = "applicationToolStripMenuItem";
            this.applicationToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.applicationToolStripMenuItem.Text = "Application";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // MinimizeButton
            // 
            this.MinimizeButton.BackColor = System.Drawing.SystemColors.Window;
            this.MinimizeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.MinimizeButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.MinimizeButton.FlatAppearance.BorderSize = 0;
            this.MinimizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinimizeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MinimizeButton.Location = new System.Drawing.Point(597, 0);
            this.MinimizeButton.Name = "MinimizeButton";
            this.MinimizeButton.Size = new System.Drawing.Size(33, 23);
            this.MinimizeButton.TabIndex = 19;
            this.MinimizeButton.TabStop = false;
            this.MinimizeButton.Text = "__";
            this.MinimizeButton.UseVisualStyleBackColor = false;
            this.MinimizeButton.Click += new System.EventHandler(this.MinimizeButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.SystemColors.Window;
            this.CloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CloseButton.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.CloseButton.FlatAppearance.BorderSize = 0;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CloseButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CloseButton.Location = new System.Drawing.Point(627, 0);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(45, 23);
            this.CloseButton.TabIndex = 20;
            this.CloseButton.TabStop = false;
            this.CloseButton.Text = "X";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            this.CloseButton.MouseEnter += new System.EventHandler(this.CloseButton_MouseHover);
            this.CloseButton.MouseLeave += new System.EventHandler(this.CloseButton_MouseLeave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AudioPlayer.Properties.Resources.play;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 26);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 200;
            this.toolTip1.ReshowDelay = 100;
            // 
            // SongYear
            // 
            this.SongYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SongYear.ForeColor = System.Drawing.SystemColors.Window;
            this.SongYear.Location = new System.Drawing.Point(6, 103);
            this.SongYear.Name = "SongYear";
            this.SongYear.Size = new System.Drawing.Size(130, 23);
            this.SongYear.TabIndex = 4;
            // 
            // SongParameters
            // 
            this.SongParameters.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SongParameters.ForeColor = System.Drawing.SystemColors.Window;
            this.SongParameters.Location = new System.Drawing.Point(6, 149);
            this.SongParameters.Name = "SongParameters";
            this.SongParameters.Size = new System.Drawing.Size(130, 34);
            this.SongParameters.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(669, 694);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MinimizeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "NetPlayer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.SongInfoContainer.ResumeLayout(false);
            this.PlaylistsContainer.ResumeLayout(false);
            this.PlaybackControlsContainer.ResumeLayout(false);
            this.PlaybackControlsContainer.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox SongInfoContainer;
        private System.Windows.Forms.Label SongDuration;
        private System.Windows.Forms.Label SongAlbum;
        private System.Windows.Forms.Label SongPerformer;
        private System.Windows.Forms.Label SongTitle;
        private System.Windows.Forms.GroupBox PlaylistsContainer;
        private System.Windows.Forms.TabControl PlaylistCollectionWindow;
        private System.Windows.Forms.GroupBox PlaybackControlsContainer;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem applicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button MinimizeButton;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Button PrevTrack;
        private System.Windows.Forms.Button NextTrack;
        private System.Windows.Forms.Button Play;
        private System.Windows.Forms.Button Pause;
        private PlaylistControls.SmoothProgressBar PlaybackProgress;
        private System.Windows.Forms.Label PlaybackTime;
        private System.Windows.Forms.RadioButton RandomButton;
        private System.Windows.Forms.Label TotalTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private PlaylistControls.SmoothProgressBar VolumeBar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton RepeatButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label SongParameters;
        private System.Windows.Forms.Label SongYear;
    }
}

