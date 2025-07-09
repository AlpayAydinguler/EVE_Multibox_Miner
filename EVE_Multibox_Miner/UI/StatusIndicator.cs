using System.Drawing;
using System.Windows.Forms;
using EVE_Multibox_Miner.Models;

namespace EVE_Multibox_Miner.UI
{
    public partial class StatusIndicator : UserControl
    {
        private MiningState _state;

        public StatusIndicator()
        {
            // InitializeComponent(); // Remove this line
            InitializeControl(); // Use custom initialization
            SetState(MiningState.Mining);
        }

        private void InitializeControl()
        {
            this.DoubleBuffered = true;
            this.Size = new Size(20, 20);
        }

        public void SetState(MiningState state)
        {
            _state = state;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (var brush = new SolidBrush(GetStateColor(_state)))
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.FillEllipse(brush, 0, 0, Width, Height);
            }
        }

        private Color GetStateColor(MiningState state)
        {
            switch (state)
            {
                case MiningState.Mining: return Color.LimeGreen;
                case MiningState.Compressing: return Color.CornflowerBlue;
                case MiningState.Docking: return Color.Orange;
                case MiningState.Docked: return Color.Gray;
                case MiningState.Error: return Color.Red;
                default: return Color.White;
            }
        }
    }
}