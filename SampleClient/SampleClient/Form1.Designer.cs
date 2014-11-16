namespace SampleClient
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
            this.Connect = new System.Windows.Forms.Button();
            this.Play = new System.Windows.Forms.Button();
            this.PlaylistBox = new System.Windows.Forms.ListView();
            this.Stop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(332, 28);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(75, 23);
            this.Connect.TabIndex = 1;
            this.Connect.Text = "Connect";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // Play
            // 
            this.Play.Location = new System.Drawing.Point(332, 97);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(75, 23);
            this.Play.TabIndex = 2;
            this.Play.Text = "Play";
            this.Play.UseVisualStyleBackColor = true;
            this.Play.Click += new System.EventHandler(this.Play_Click);
            // 
            // PlaylistBox
            // 
            this.PlaylistBox.CheckBoxes = true;
            this.PlaylistBox.LabelWrap = false;
            this.PlaylistBox.Location = new System.Drawing.Point(12, 12);
            this.PlaylistBox.MultiSelect = false;
            this.PlaylistBox.Name = "PlaylistBox";
            this.PlaylistBox.ShowGroups = false;
            this.PlaylistBox.Size = new System.Drawing.Size(187, 237);
            this.PlaylistBox.TabIndex = 9;
            this.PlaylistBox.UseCompatibleStateImageBehavior = false;
            this.PlaylistBox.View = System.Windows.Forms.View.SmallIcon;
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(332, 126);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(75, 23);
            this.Stop.TabIndex = 10;
            this.Stop.Text = "Stop";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 261);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.PlaylistBox);
            this.Controls.Add(this.Play);
            this.Controls.Add(this.Connect);
            this.Name = "Form1";
            this.Text = "ClientPlayer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.Button Play;
        private System.Windows.Forms.ListView PlaylistBox;
        private System.Windows.Forms.Button Stop;
    }
}

