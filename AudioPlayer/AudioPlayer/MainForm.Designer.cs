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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.applicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MinimizeButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.PrevTrack = new System.Windows.Forms.Button();
            this.NextTrack = new System.Windows.Forms.Button();
            this.Play = new System.Windows.Forms.Button();
            this.Pause = new System.Windows.Forms.Button();
            this.PlaybackTime = new System.Windows.Forms.Label();
            this.smoothProgressBar1 = new PlaylistControls.SmoothProgressBar();
            this.RandomButton = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SongInfoContainer.SuspendLayout();
            this.PlaylistsContainer.SuspendLayout();
            this.PlaybackControlsContainer.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Magneto", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 0);
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
            this.SongDuration.Location = new System.Drawing.Point(6, 103);
            this.SongDuration.Name = "SongDuration";
            this.SongDuration.Size = new System.Drawing.Size(130, 33);
            this.SongDuration.TabIndex = 3;
            // 
            // SongAlbum
            // 
            this.SongAlbum.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SongAlbum.Location = new System.Drawing.Point(6, 80);
            this.SongAlbum.Name = "SongAlbum";
            this.SongAlbum.Size = new System.Drawing.Size(130, 23);
            this.SongAlbum.TabIndex = 2;
            // 
            // SongPerformer
            // 
            this.SongPerformer.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SongPerformer.Location = new System.Drawing.Point(6, 59);
            this.SongPerformer.Name = "SongPerformer";
            this.SongPerformer.Size = new System.Drawing.Size(130, 21);
            this.SongPerformer.TabIndex = 1;
            // 
            // SongTitle
            // 
            this.SongTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SongTitle.Location = new System.Drawing.Point(6, 26);
            this.SongTitle.Name = "SongTitle";
            this.SongTitle.Size = new System.Drawing.Size(130, 33);
            this.SongTitle.TabIndex = 0;
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
            this.PlaybackControlsContainer.Controls.Add(this.RandomButton);
            this.PlaybackControlsContainer.Controls.Add(this.smoothProgressBar1);
            this.PlaybackControlsContainer.Controls.Add(this.PlaybackTime);
            this.PlaybackControlsContainer.Controls.Add(this.Stop);
            this.PlaybackControlsContainer.Controls.Add(this.PrevTrack);
            this.PlaybackControlsContainer.Controls.Add(this.NextTrack);
            this.PlaybackControlsContainer.Controls.Add(this.Play);
            this.PlaybackControlsContainer.Controls.Add(this.Pause);
            this.PlaybackControlsContainer.Location = new System.Drawing.Point(15, 27);
            this.PlaybackControlsContainer.Name = "PlaybackControlsContainer";
            this.PlaybackControlsContainer.Size = new System.Drawing.Size(635, 100);
            this.PlaybackControlsContainer.TabIndex = 15;
            this.PlaybackControlsContainer.TabStop = false;
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
            this.applicationToolStripMenuItem.Name = "applicationToolStripMenuItem";
            this.applicationToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.applicationToolStripMenuItem.Text = "Application";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
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
            this.CloseButton.Text = "X";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            this.CloseButton.MouseEnter += new System.EventHandler(this.CloseButton_MouseHover);
            this.CloseButton.MouseLeave += new System.EventHandler(this.CloseButton_MouseLeave);
            // 
            // Stop
            // 
            this.Stop.BackColor = System.Drawing.SystemColors.Window;
            this.Stop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Stop.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Stop.FlatAppearance.BorderSize = 0;
            this.Stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Stop.Image = ((System.Drawing.Image)(resources.GetObject("Stop.Image")));
            this.Stop.Location = new System.Drawing.Point(56, 58);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(40, 23);
            this.Stop.TabIndex = 19;
            this.Stop.UseVisualStyleBackColor = true;
            // 
            // PrevTrack
            // 
            this.PrevTrack.BackColor = System.Drawing.SystemColors.Window;
            this.PrevTrack.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.PrevTrack.FlatAppearance.BorderSize = 0;
            this.PrevTrack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PrevTrack.Image = ((System.Drawing.Image)(resources.GetObject("PrevTrack.Image")));
            this.PrevTrack.Location = new System.Drawing.Point(148, 58);
            this.PrevTrack.Name = "PrevTrack";
            this.PrevTrack.Size = new System.Drawing.Size(40, 23);
            this.PrevTrack.TabIndex = 17;
            this.PrevTrack.UseVisualStyleBackColor = true;
            // 
            // NextTrack
            // 
            this.NextTrack.BackColor = System.Drawing.SystemColors.Window;
            this.NextTrack.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.NextTrack.FlatAppearance.BorderSize = 0;
            this.NextTrack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NextTrack.Image = ((System.Drawing.Image)(resources.GetObject("NextTrack.Image")));
            this.NextTrack.Location = new System.Drawing.Point(194, 58);
            this.NextTrack.Name = "NextTrack";
            this.NextTrack.Size = new System.Drawing.Size(40, 23);
            this.NextTrack.TabIndex = 16;
            this.NextTrack.UseVisualStyleBackColor = true;
            // 
            // Play
            // 
            this.Play.BackColor = System.Drawing.SystemColors.Window;
            this.Play.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Play.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Play.FlatAppearance.BorderSize = 0;
            this.Play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Play.Image = ((System.Drawing.Image)(resources.GetObject("Play.Image")));
            this.Play.Location = new System.Drawing.Point(10, 58);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(40, 23);
            this.Play.TabIndex = 15;
            this.Play.UseVisualStyleBackColor = true;
            // 
            // Pause
            // 
            this.Pause.BackColor = System.Drawing.SystemColors.Window;
            this.Pause.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Pause.FlatAppearance.BorderSize = 0;
            this.Pause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Pause.Image = ((System.Drawing.Image)(resources.GetObject("Pause.Image")));
            this.Pause.Location = new System.Drawing.Point(102, 58);
            this.Pause.Name = "Pause";
            this.Pause.Size = new System.Drawing.Size(40, 23);
            this.Pause.TabIndex = 18;
            this.Pause.UseVisualStyleBackColor = true;
            // 
            // PlaybackTime
            // 
            this.PlaybackTime.Location = new System.Drawing.Point(529, 58);
            this.PlaybackTime.Name = "PlaybackTime";
            this.PlaybackTime.Size = new System.Drawing.Size(100, 23);
            this.PlaybackTime.TabIndex = 20;
            this.PlaybackTime.Text = "00:00/00:00";
            this.PlaybackTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // smoothProgressBar1
            // 
            this.smoothProgressBar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.smoothProgressBar1.Location = new System.Drawing.Point(10, 19);
            this.smoothProgressBar1.Maximum = 100;
            this.smoothProgressBar1.Minimum = 0;
            this.smoothProgressBar1.Name = "smoothProgressBar1";
            this.smoothProgressBar1.ProgressBarColor = System.Drawing.Color.CornflowerBlue;
            this.smoothProgressBar1.Size = new System.Drawing.Size(619, 19);
            this.smoothProgressBar1.TabIndex = 21;
            this.smoothProgressBar1.Value = 0;
            // 
            // RandomButton
            // 
            this.RandomButton.AutoCheck = false;
            this.RandomButton.AutoSize = true;
            this.RandomButton.Location = new System.Drawing.Point(240, 61);
            this.RandomButton.Name = "RandomButton";
            this.RandomButton.Size = new System.Drawing.Size(103, 17);
            this.RandomButton.TabIndex = 22;
            this.RandomButton.TabStop = true;
            this.RandomButton.Text = "Random (on/off)";
            this.RandomButton.UseVisualStyleBackColor = true;
            this.RandomButton.Click += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(669, 694);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MinimizeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Text = "MainForm";
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
        private PlaylistControls.SmoothProgressBar smoothProgressBar1;
        private System.Windows.Forms.Label PlaybackTime;
        private System.Windows.Forms.RadioButton RandomButton;
    }
}

