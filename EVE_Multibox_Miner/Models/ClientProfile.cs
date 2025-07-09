using System.Drawing;

namespace EVE_Multibox_Miner.Models
{
    public class ClientProfile
    {
        public string Name { get; set; }
        public Rectangle WindowArea { get; set; }
        public double LoopInterval { get; set; }
        public Point MiningPixel { get; set; }
        public Rectangle CompressionBox { get; set; }
        public Rectangle DockButton { get; set; }
        public Rectangle CompressButton { get; set; }
        public Point DockVerificationPixel { get; set; }
        public Color PreDockColor { get; set; }
        public bool IsActive { get; set; }
    }
}