namespace MediaServer
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
            this.components = new System.ComponentModel.Container();
            this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.Playlist = new System.Windows.Forms.ListBox();
            this.SelectFiles = new System.Windows.Forms.Button();
            this.OpenFilesDialog = new System.Windows.Forms.OpenFileDialog();
            this.StartServer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TrayIcon
            // 
            this.TrayIcon.Text = "MediaServer";
            this.TrayIcon.Visible = true;
            // 
            // Playlist
            // 
            this.Playlist.FormattingEnabled = true;
            this.Playlist.Location = new System.Drawing.Point(12, 25);
            this.Playlist.Name = "Playlist";
            this.Playlist.Size = new System.Drawing.Size(164, 199);
            this.Playlist.TabIndex = 0;
            // 
            // SelectFiles
            // 
            this.SelectFiles.Location = new System.Drawing.Point(12, 238);
            this.SelectFiles.Name = "SelectFiles";
            this.SelectFiles.Size = new System.Drawing.Size(75, 23);
            this.SelectFiles.TabIndex = 1;
            this.SelectFiles.Text = "Select files";
            this.SelectFiles.UseVisualStyleBackColor = true;
            this.SelectFiles.Click += new System.EventHandler(this.SelectFiles_Click);
            // 
            // OpenFilesDialog
            // 
            this.OpenFilesDialog.Filter = "Mp3 files|*.mp3";
            this.OpenFilesDialog.Multiselect = true;
            // 
            // StartServer
            // 
            this.StartServer.Location = new System.Drawing.Point(292, 25);
            this.StartServer.Name = "StartServer";
            this.StartServer.Size = new System.Drawing.Size(75, 23);
            this.StartServer.TabIndex = 2;
            this.StartServer.Text = "Start server";
            this.StartServer.UseVisualStyleBackColor = true;
            this.StartServer.Click += new System.EventHandler(this.StartServer_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 273);
            this.Controls.Add(this.StartServer);
            this.Controls.Add(this.SelectFiles);
            this.Controls.Add(this.Playlist);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon TrayIcon;
        private System.Windows.Forms.ListBox Playlist;
        private System.Windows.Forms.Button SelectFiles;
        private System.Windows.Forms.OpenFileDialog OpenFilesDialog;
        private System.Windows.Forms.Button StartServer;
    }
}

