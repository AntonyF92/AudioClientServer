namespace SampleClient
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
            this.PlaylistCollectionWindow = new System.Windows.Forms.TabControl();
            this.Stop = new System.Windows.Forms.Button();
            this.Pause = new System.Windows.Forms.Button();
            this.PrevTrack = new System.Windows.Forms.Button();
            this.NextTrack = new System.Windows.Forms.Button();
            this.Play = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.applicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MinimizeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PlaylistCollectionWindow
            // 
            this.PlaylistCollectionWindow.Location = new System.Drawing.Point(187, 27);
            this.PlaylistCollectionWindow.Name = "PlaylistCollectionWindow";
            this.PlaylistCollectionWindow.SelectedIndex = 0;
            this.PlaylistCollectionWindow.Size = new System.Drawing.Size(525, 497);
            this.PlaylistCollectionWindow.TabIndex = 11;
            // 
            // Stop
            // 
            this.Stop.BackColor = System.Drawing.SystemColors.Window;
            this.Stop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Stop.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Stop.FlatAppearance.BorderSize = 0;
            this.Stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Stop.Image = ((System.Drawing.Image)(resources.GetObject("Stop.Image")));
            this.Stop.Location = new System.Drawing.Point(55, 27);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(40, 23);
            this.Stop.TabIndex = 14;
            this.Stop.UseVisualStyleBackColor = false;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // Pause
            // 
            this.Pause.BackColor = System.Drawing.SystemColors.Window;
            this.Pause.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Pause.FlatAppearance.BorderSize = 0;
            this.Pause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Pause.Image = ((System.Drawing.Image)(resources.GetObject("Pause.Image")));
            this.Pause.Location = new System.Drawing.Point(101, 27);
            this.Pause.Name = "Pause";
            this.Pause.Size = new System.Drawing.Size(40, 23);
            this.Pause.TabIndex = 13;
            this.Pause.UseVisualStyleBackColor = false;
            this.Pause.Click += new System.EventHandler(this.Pause_Click);
            // 
            // PrevTrack
            // 
            this.PrevTrack.BackColor = System.Drawing.SystemColors.Window;
            this.PrevTrack.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.PrevTrack.FlatAppearance.BorderSize = 0;
            this.PrevTrack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PrevTrack.Image = ((System.Drawing.Image)(resources.GetObject("PrevTrack.Image")));
            this.PrevTrack.Location = new System.Drawing.Point(55, 56);
            this.PrevTrack.Name = "PrevTrack";
            this.PrevTrack.Size = new System.Drawing.Size(40, 23);
            this.PrevTrack.TabIndex = 12;
            this.PrevTrack.UseVisualStyleBackColor = false;
            this.PrevTrack.Click += new System.EventHandler(this.PrevTrack_Click);
            // 
            // NextTrack
            // 
            this.NextTrack.BackColor = System.Drawing.SystemColors.Window;
            this.NextTrack.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.NextTrack.FlatAppearance.BorderSize = 0;
            this.NextTrack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NextTrack.Image = ((System.Drawing.Image)(resources.GetObject("NextTrack.Image")));
            this.NextTrack.Location = new System.Drawing.Point(101, 56);
            this.NextTrack.Name = "NextTrack";
            this.NextTrack.Size = new System.Drawing.Size(40, 23);
            this.NextTrack.TabIndex = 11;
            this.NextTrack.UseVisualStyleBackColor = false;
            this.NextTrack.Click += new System.EventHandler(this.NextTrack_Click);
            // 
            // Play
            // 
            this.Play.BackColor = System.Drawing.SystemColors.Window;
            this.Play.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Play.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Play.FlatAppearance.BorderSize = 0;
            this.Play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Play.Image = ((System.Drawing.Image)(resources.GetObject("Play.Image")));
            this.Play.Location = new System.Drawing.Point(9, 27);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(40, 23);
            this.Play.TabIndex = 2;
            this.Play.UseVisualStyleBackColor = false;
            this.Play.Click += new System.EventHandler(this.Play_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.PlaylistCollectionWindow);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Controls.Add(this.Stop);
            this.panel1.Controls.Add(this.Pause);
            this.panel1.Controls.Add(this.Play);
            this.panel1.Controls.Add(this.NextTrack);
            this.panel1.Controls.Add(this.PrevTrack);
            this.panel1.Location = new System.Drawing.Point(12, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(714, 527);
            this.panel1.TabIndex = 13;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(714, 24);
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
            this.MinimizeButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.MinimizeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.MinimizeButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.MinimizeButton.FlatAppearance.BorderSize = 0;
            this.MinimizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinimizeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MinimizeButton.Location = new System.Drawing.Point(657, 1);
            this.MinimizeButton.Name = "MinimizeButton";
            this.MinimizeButton.Size = new System.Drawing.Size(33, 23);
            this.MinimizeButton.TabIndex = 15;
            this.MinimizeButton.Text = "__";
            this.MinimizeButton.UseVisualStyleBackColor = false;
            this.MinimizeButton.Click += new System.EventHandler(this.MinimizeButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe Script", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(23, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 23);
            this.label1.TabIndex = 17;
            this.label1.Text = "StreamPlayer v1.0";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CloseButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CloseButton.FlatAppearance.BorderSize = 0;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Image = global::SampleClient.Properties.Resources._1416407871_ic_close_48px_24;
            this.CloseButton.Location = new System.Drawing.Point(696, 1);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(30, 23);
            this.CloseButton.TabIndex = 16;
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(728, 567);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MinimizeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "ClientPlayer";
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

        private System.Windows.Forms.Button Play;
        private System.Windows.Forms.TabControl PlaylistCollectionWindow;
        private System.Windows.Forms.Button Pause;
        private System.Windows.Forms.Button PrevTrack;
        private System.Windows.Forms.Button NextTrack;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button MinimizeButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem applicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

