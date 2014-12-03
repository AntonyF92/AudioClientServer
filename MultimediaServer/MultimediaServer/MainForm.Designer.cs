namespace MediaServer
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
            this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.OpenFilesDialog = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PlaylistCollectionWindow = new System.Windows.Forms.TabControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.playlistsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newPlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.currentPlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removePlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseButton = new System.Windows.Forms.Button();
            this.MinimizeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.OpenFoldersDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TrayIcon
            // 
            this.TrayIcon.Text = "MediaServer";
            this.TrayIcon.Visible = true;
            // 
            // OpenFilesDialog
            // 
            this.OpenFilesDialog.Filter = "Mp3 & WAV files|*.mp3; *.wav";
            this.OpenFilesDialog.Multiselect = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.PlaylistCollectionWindow);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Location = new System.Drawing.Point(12, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(525, 560);
            this.panel1.TabIndex = 0;
            // 
            // PlaylistCollectionWindow
            // 
            this.PlaylistCollectionWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlaylistCollectionWindow.Location = new System.Drawing.Point(0, 24);
            this.PlaylistCollectionWindow.Name = "PlaylistCollectionWindow";
            this.PlaylistCollectionWindow.SelectedIndex = 0;
            this.PlaylistCollectionWindow.Size = new System.Drawing.Size(525, 536);
            this.PlaylistCollectionWindow.TabIndex = 12;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playlistsToolStripMenuItem,
            this.serverToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(525, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // playlistsToolStripMenuItem
            // 
            this.playlistsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newPlaylistToolStripMenuItem,
            this.currentPlaylistToolStripMenuItem});
            this.playlistsToolStripMenuItem.Name = "playlistsToolStripMenuItem";
            this.playlistsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.playlistsToolStripMenuItem.Text = "Playlists";
            // 
            // newPlaylistToolStripMenuItem
            // 
            this.newPlaylistToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFilesToolStripMenuItem,
            this.openFolderToolStripMenuItem});
            this.newPlaylistToolStripMenuItem.Name = "newPlaylistToolStripMenuItem";
            this.newPlaylistToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.newPlaylistToolStripMenuItem.Text = "New playlist";
            // 
            // openFilesToolStripMenuItem
            // 
            this.openFilesToolStripMenuItem.Name = "openFilesToolStripMenuItem";
            this.openFilesToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.openFilesToolStripMenuItem.Text = "Open files";
            this.openFilesToolStripMenuItem.Click += new System.EventHandler(this.openFilesToolStripMenuItem_Click);
            // 
            // openFolderToolStripMenuItem
            // 
            this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.openFolderToolStripMenuItem.Text = "Open folder";
            this.openFolderToolStripMenuItem.Click += new System.EventHandler(this.openFolderToolStripMenuItem_Click);
            // 
            // currentPlaylistToolStripMenuItem
            // 
            this.currentPlaylistToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFilesToolStripMenuItem,
            this.addFolderToolStripMenuItem,
            this.removePlaylistToolStripMenuItem});
            this.currentPlaylistToolStripMenuItem.Name = "currentPlaylistToolStripMenuItem";
            this.currentPlaylistToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.currentPlaylistToolStripMenuItem.Text = "Current playlist";
            // 
            // addFilesToolStripMenuItem
            // 
            this.addFilesToolStripMenuItem.Name = "addFilesToolStripMenuItem";
            this.addFilesToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.addFilesToolStripMenuItem.Text = "Add files";
            // 
            // addFolderToolStripMenuItem
            // 
            this.addFolderToolStripMenuItem.Name = "addFolderToolStripMenuItem";
            this.addFolderToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.addFolderToolStripMenuItem.Text = "Add folder";
            // 
            // removePlaylistToolStripMenuItem
            // 
            this.removePlaylistToolStripMenuItem.Name = "removePlaylistToolStripMenuItem";
            this.removePlaylistToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.removePlaylistToolStripMenuItem.Text = "Remove playlist";
            this.removePlaylistToolStripMenuItem.Click += new System.EventHandler(this.removePlaylistToolStripMenuItem_Click);
            // 
            // serverToolStripMenuItem
            // 
            this.serverToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startServerToolStripMenuItem,
            this.stopServerToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.serverToolStripMenuItem.Name = "serverToolStripMenuItem";
            this.serverToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.serverToolStripMenuItem.Text = "Server";
            // 
            // startServerToolStripMenuItem
            // 
            this.startServerToolStripMenuItem.Name = "startServerToolStripMenuItem";
            this.startServerToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.startServerToolStripMenuItem.Text = "Start server";
            this.startServerToolStripMenuItem.Click += new System.EventHandler(this.startServerToolStripMenuItem_Click);
            // 
            // stopServerToolStripMenuItem
            // 
            this.stopServerToolStripMenuItem.Name = "stopServerToolStripMenuItem";
            this.stopServerToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.stopServerToolStripMenuItem.Text = "Stop server";
            this.stopServerToolStripMenuItem.Click += new System.EventHandler(this.stopServerToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CloseButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CloseButton.FlatAppearance.BorderSize = 0;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Image = global::MediaServer.Properties.Resources._1416407871_ic_close_48px_24;
            this.CloseButton.Location = new System.Drawing.Point(510, 4);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(30, 23);
            this.CloseButton.TabIndex = 18;
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // MinimizeButton
            // 
            this.MinimizeButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.MinimizeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.MinimizeButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.MinimizeButton.FlatAppearance.BorderSize = 0;
            this.MinimizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinimizeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MinimizeButton.Location = new System.Drawing.Point(471, 4);
            this.MinimizeButton.Name = "MinimizeButton";
            this.MinimizeButton.Size = new System.Drawing.Size(33, 23);
            this.MinimizeButton.TabIndex = 17;
            this.MinimizeButton.Text = "__";
            this.MinimizeButton.UseVisualStyleBackColor = false;
            this.MinimizeButton.Click += new System.EventHandler(this.MinimizeButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 20);
            this.label1.TabIndex = 19;
            this.label1.Text = "Audio server";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(543, 599);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.MinimizeButton);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Audio server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon TrayIcon;
        private System.Windows.Forms.OpenFileDialog OpenFilesDialog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem serverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button MinimizeButton;
        private System.Windows.Forms.TabControl PlaylistCollectionWindow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem playlistsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newPlaylistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem currentPlaylistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removePlaylistToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog OpenFoldersDialog;
    }
}

