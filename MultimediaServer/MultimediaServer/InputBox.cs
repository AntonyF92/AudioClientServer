using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaServer
{
    public partial class InputBox : Form
    {
        string value = "";

        private InputBox()
        {
            InitializeComponent();
        }

        int iFormX, iFormY, iMouseX, iMouseY;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            iFormX = this.Location.X;
            iFormY = this.Location.Y;
            iMouseX = MousePosition.X;
            iMouseY = MousePosition.Y;

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            int iMouseX2 = MousePosition.X;
            int iMouseY2 = MousePosition.Y;
            if (e.Button == MouseButtons.Left)
                this.Location = new Point(iFormX + (iMouseX2 - iMouseX), iFormY + (iMouseY2 - iMouseY));

        }

        private void OK_Click(object sender, EventArgs e)
        {
            value = ValueBox.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        public static DialogResult ShowBox(string title, out string value)
        {
            InputBox box = new InputBox();
            box.Title.Text = title;
            var result = box.ShowDialog();
            value = box.ValueBox.Text;
            box.Dispose();
            return result;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
