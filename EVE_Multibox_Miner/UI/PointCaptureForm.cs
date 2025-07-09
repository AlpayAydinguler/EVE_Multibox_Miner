using System;
using System.Drawing;
using System.Windows.Forms;

namespace EVE_Multibox_Miner.UI
{
    public partial class PointCaptureForm : Form
    {
        public Point CapturedPoint { get; private set; }
        private readonly string instruction;

        public PointCaptureForm(string instruction)
        {
            this.instruction = instruction;
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Opacity = 0.01;  // Nearly transparent
            this.TopMost = true;
            this.Cursor = Cursors.Cross;
            this.Text = instruction;
            this.Click += PointCaptureForm_Click;
        }

        private void PointCaptureForm_Click(object sender, EventArgs e)
        {
            // Capture mouse position in screen coordinates
            CapturedPoint = Cursor.Position;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}