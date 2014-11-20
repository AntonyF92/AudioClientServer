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
            this.OpenFilesDialog = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.serverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseButton = new System.Windows.Forms.Button();
            this.MinimizeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.PlaylistCollectionWindow = new System.Windows.Forms.TabControl();
            this.AddPlaylist = new System.Windows.Forms.Button();
            this.RemovePlaylist = new System.Windows.Forms.Button();
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
            this.OpenFilesDialog.Filter = "Mp3 files|*.mp3";
            this.OpenFilesDialog.Multiselect = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.PlaylistCollectionWindow);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Location = new System.Drawing.Point(12, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(392, 467);
            this.panel1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(392, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
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
            this.startServerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.startServerToolStripMenuItem.Text = "Start server";
            // 
            // stopServerToolStripMenuItem
            // 
            this.stopServerToolStripMenuItem.Name = "stopServerToolStripMenuItem";
            this.stopServerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.stopServerToolStripMenuItem.Text = "Stop server";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CloseButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CloseButton.FlatAppearance.BorderSize = 0;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Image = global::MediaServer.Properties.Resources._1416407871_ic_close_48px_24;
            this.CloseButton.Location = new System.Drawing.Point(374, 4);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(30, 23);
            this.CloseButton.TabIndex = 18;
            this.CloseButton.UseVisualStyleBackColor = false;
            // 
            // MinimizeButton
            // 
            this.MinimizeButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.MinimizeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.MinimizeButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.MinimizeButton.FlatAppearance.BorderSize = 0;
            this.MinimizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinimizeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MinimizeButton.Location = new System.Drawing.Point(335, 4);
            this.MinimizeButton.Name = "MinimizeButton";
            this.MinimizeButton.Size = new System.Drawing.Size(33, 23);
            this.MinimizeButton.TabIndex = 17;
            this.MinimizeButton.Text = "__";
            this.MinimizeButton.UseVisualStyleBackColor = false;
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
            // 
            // PlaylistCollectionWindow
            // 
            this.PlaylistCollectionWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlaylistCollectionWindow.Location = new System.Drawing.Point(0, 24);
            this.PlaylistCollectionWindow.Name = "PlaylistCollectionWindow";
            this.PlaylistCollectionWindow.SelectedIndex = 0;
            this.PlaylistCollectionWindow.Size = new System.Drawing.Size(392, 443);
            this.PlaylistCollectionWindow.TabIndex = 12;
            // 
            // AddPlaylist
            // 
            this.AddPlaylist.Location = new System.Drawing.Point(12, 503);
            this.AddPlaylist.Name = "AddPlaylist";
            this.AddPlaylist.Size = new System.Drawing.Size(75, 23);
            this.AddPlaylist.TabIndex = 20;
            this.AddPlaylist.Text = "Add playlist";
            this.AddPlaylist.UseVisualStyleBackColor = true;
            // 
            // RemovePlaylist
            // 
            this.RemovePlaylist.Location = new System.Drawing.Point(93, 503);
            this.RemovePlaylist.Name = "RemovePlaylist";
            this.RemovePlaylist.Size = new System.Drawing.Size(89, 23);
            this.RemovePlaylist.TabIndex = 21;
            this.RemovePlaylist.Text = "Remove playlist";
            this.RemovePlaylist.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(410, 538);
            this.Controls.Add(this.RemovePlaylist);
            this.Controls.Add(this.AddPlaylist);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.MinimizeButton);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Audio server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
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
        private System.Windows.Forms.Button AddPlaylist;
        private System.Windows.Forms.Button RemovePlaylist;
    }
}

