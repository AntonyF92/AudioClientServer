namespace AudioPlayer
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
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
            this.player = new AxWMPLib.AxWindowsMediaPlayer();
            this.panel1.SuspendLayout();
            this.SongInfoContainer.SuspendLayout();
            this.PlaylistsContainer.SuspendLayout();
            this.PlaybackControlsContainer.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.player)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Magneto", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 19);
            this.label1.TabIndex = 21;
            this.label1.Text = "StreamPlayer v1.0";
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
            this.PlaylistCollectionWindow.Location = new System.Drawing.Point(6, 10);
            this.PlaylistCollectionWindow.Name = "PlaylistCollectionWindow";
            this.PlaylistCollectionWindow.SelectedIndex = 0;
            this.PlaylistCollectionWindow.Size = new System.Drawing.Size(475, 497);
            this.PlaylistCollectionWindow.TabIndex = 11;
            // 
            // PlaybackControlsContainer
            // 
            this.PlaybackControlsContainer.Controls.Add(this.player);
            this.PlaybackControlsContainer.Location = new System.Drawing.Point(15, 27);
            this.PlaybackControlsContainer.Name = "PlaybackControlsContainer";
            this.PlaybackControlsContainer.Size = new System.Drawing.Size(635, 100);
            this.PlaybackControlsContainer.TabIndex = 15;
            this.PlaybackControlsContainer.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
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
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // MinimizeButton
            // 
            this.MinimizeButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.MinimizeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.MinimizeButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.MinimizeButton.FlatAppearance.BorderSize = 0;
            this.MinimizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinimizeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MinimizeButton.Location = new System.Drawing.Point(597, 0);
            this.MinimizeButton.Name = "MinimizeButton";
            this.MinimizeButton.Size = new System.Drawing.Size(33, 23);
            this.MinimizeButton.TabIndex = 19;
            this.MinimizeButton.Text = "__";
            this.MinimizeButton.UseVisualStyleBackColor = false;
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CloseButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.CloseButton.FlatAppearance.BorderSize = 0;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Image = global::AudioPlayer.Properties.Resources._1416407871_ic_close_48px_24;
            this.CloseButton.Location = new System.Drawing.Point(636, 0);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(30, 23);
            this.CloseButton.TabIndex = 20;
            this.CloseButton.UseVisualStyleBackColor = false;
            // 
            // player
            // 
            this.player.Enabled = true;
            this.player.Location = new System.Drawing.Point(6, 19);
            this.player.Name = "player";
            this.player.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("player.OcxState")));
            this.player.Size = new System.Drawing.Size(623, 45);
            this.player.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(671, 694);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MinimizeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.SongInfoContainer.ResumeLayout(false);
            this.PlaylistsContainer.ResumeLayout(false);
            this.PlaybackControlsContainer.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.player)).EndInit();
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
        private AxWMPLib.AxWindowsMediaPlayer player;
    }
}

