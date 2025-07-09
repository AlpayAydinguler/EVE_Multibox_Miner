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
            lblPosition.Text = Profile.WindowArea.IsEmpty ?
                "Not set" :
                $"{Profile.WindowArea.X},{Profile.WindowArea.Y} - {Profile.WindowArea.Width}x{Profile.WindowArea.Height}";
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
            MessageBox.Show("Drag a rectangle over the Compress button");
            using (var selector = new AreaSelectorForm())
            {
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    Profile.CompressButton = selector.SelectedArea;
                }
            }
        }
    }
}