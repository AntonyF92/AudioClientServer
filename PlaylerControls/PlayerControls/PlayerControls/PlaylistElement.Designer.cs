namespace PlayerControls
{
    partial class PlaylistElement
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Performer = new System.Windows.Forms.Label();
            this.Duration = new System.Windows.Forms.Label();
            this.Info = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Performer
            // 
            this.Performer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Performer.Location = new System.Drawing.Point(3, 0);
            this.Performer.Name = "Performer";
            this.Performer.Size = new System.Drawing.Size(401, 23);
            this.Performer.TabIndex = 0;
            this.Performer.Text = "label1";
            this.Performer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Performer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PlaylistElement_MouseClick);
            // 
            // Duration
            // 
            this.Duration.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Duration.Location = new System.Drawing.Point(410, 0);
            this.Duration.Name = "Duration";
            this.Duration.Size = new System.Drawing.Size(85, 23);
            this.Duration.TabIndex = 2;
            this.Duration.Text = "label1";
            this.Duration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Duration.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PlaylistElement_MouseClick);
            // 
            // Info
            // 
            this.Info.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Info.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Info.Location = new System.Drawing.Point(3, 23);
            this.Info.Name = "Info";
            this.Info.Size = new System.Drawing.Size(280, 23);
            this.Info.TabIndex = 3;
            this.Info.Text = "label1";
            this.Info.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Info.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PlaylistElement_MouseClick);
            // 
            // PlaylistElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Controls.Add(this.Info);
            this.Controls.Add(this.Duration);
            this.Controls.Add(this.Performer);
            this.DoubleBuffered = true;
            this.Name = "PlaylistElement";
            this.Size = new System.Drawing.Size(498, 48);
            this.Load += new System.EventHandler(this.PlaylistElement_Load);
            this.Leave += new System.EventHandler(this.PlaylistElement_Leave);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PlaylistElement_MouseClick);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Performer;
        private System.Windows.Forms.Label Duration;
        private System.Windows.Forms.Label Info;
    }
}
