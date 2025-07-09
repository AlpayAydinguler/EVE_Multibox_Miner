using EVE_Multibox_Miner.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EVE_Multibox_Miner.UI
{
    public partial class ClientProfileControl : UserControl
    {
        public ClientProfile Profile { get; }

        public ClientProfileControl(ClientProfile profile)
        {
            InitializeComponent();
            Profile = profile;
            lblClientName.Text = profile.Name;
            UpdatePositionLabel();
        }

        public void SetState(MiningState state)
        {
            Invoke((Action)(() =>
            {
                lblStatus.Text = state.ToString();
                lblStatus.BackColor = GetStateColor(state);
            }));
        }

        private Color GetStateColor(MiningState state)
        {
            switch (state)
            {
                case MiningState.Mining: return Color.LightGreen;
                case MiningState.Compressing: return Color.LightBlue;
                case MiningState.Docking: return Color.Orange;
                case MiningState.Docked: return Color.Gray;
                case MiningState.Error: return Color.Red;
                default: return Color.White;
            }
        }

        private void btnSetWindow_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Drag a selection rectangle over the client window");
            using (var selector = new AreaSelectorForm())
            {
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    Profile.WindowArea = selector.SelectedArea;
                    UpdatePositionLabel();
                }
            }
        }

        private void UpdatePositionLabel()
        {
            if (Profile.WindowArea.IsEmpty)
            {
                lblPosition.Text = "Not set";
            }
            else
            {
                lblPosition.Text = $"{Profile.WindowArea.X},{Profile.WindowArea.Y} - {Profile.WindowArea.Width}x{Profile.WindowArea.Height}";
            }
        }

        private void btnSetCompression_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Drag a rectangle over the ore inventory");
            using (var selector = new AreaSelectorForm())
            {
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    Profile.CompressionBox = selector.SelectedArea;
                }
            }
        }

        private void btnSetCompressButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. Right-click on your ore\n2. Click the 'Set Right-Click Position' button\n" +
                            "3. Click the 'Set Compress Button' button on the context menu");

            // Step 1: Set right-click position
            using (var pointForm = new PointCaptureForm("Right-Click Position"))
            {
                if (pointForm.ShowDialog() == DialogResult.OK)
                {
                    Point rightClickPoint = pointForm.CapturedPoint;

                    // Step 2: Set compress button position
                    using (var pointForm2 = new PointCaptureForm("Compress Button"))
                    {
                        if (pointForm2.ShowDialog() == DialogResult.OK)
                        {
                            Point compressPoint = pointForm2.CapturedPoint;

                            // Calculate offsets
                            Profile.CompressXOffset = compressPoint.X - rightClickPoint.X;
                            Profile.CompressYOffset = compressPoint.Y - rightClickPoint.Y;

                            // Set fixed dimensions
                            Profile.CompressWidth = 150;  // Typical width
                            Profile.CompressHeight = 20;  // Typical height

                            MessageBox.Show($"Compress button offsets set: " +
                                           $"X: {Profile.CompressXOffset}, Y: {Profile.CompressYOffset}");
                        }
                    }
                }
            }
        }
    }
}