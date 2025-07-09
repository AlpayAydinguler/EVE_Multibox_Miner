using System;
using System.Drawing;
using System.Windows.Forms;

namespace EVE_Multibox_Miner.UI
{
    public partial class AreaSelectorForm : Form
    {
        private Point startPoint;
        private Rectangle selectionRect;
        public Rectangle SelectedArea { get; private set; }

        public AreaSelectorForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Opacity = 0.5;
            this.DoubleBuffered = true;
            this.Cursor = Cursors.Cross;
            this.BackColor = Color.Black;
            this.KeyPreview = true;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startPoint = e.Location;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = Math.Min(startPoint.X, e.X);
                int y = Math.Min(startPoint.Y, e.Y);
                int width = Math.Abs(startPoint.X - e.X);
                int height = Math.Abs(startPoint.Y - e.Y);

                selectionRect = new Rectangle(x, y, width, height);
                this.Invalidate();
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (selectionRect.Width > 10 && selectionRect.Height > 10)
                {
                    SelectedArea = selectionRect;
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                }
                this.Close();
            }
            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (selectionRect != Rectangle.Empty)
            {
                using (var brush = new SolidBrush(Color.FromArgb(100, 150, 150, 255)))
                using (var pen = new Pen(Color.Red, 2))
                {
                    e.Graphics.FillRectangle(brush, selectionRect);
                    e.Graphics.DrawRectangle(pen, selectionRect);

                    // Draw size info
                    using (var font = new Font("Arial", 12))
                    using (var textBrush = new SolidBrush(Color.White))
                    {
                        string sizeText = $"{selectionRect.Width} x {selectionRect.Height}";
                        e.Graphics.DrawString(sizeText, font, textBrush,
                            selectionRect.X + 5, selectionRect.Y + 5);
                    }
                }
            }
            base.OnPaint(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            base.OnKeyDown(e);
        }
    }
}