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
        public int CompressXOffset { get; set; }  // X difference from right-click to Compress button
        public int CompressYOffset { get; set; }  // Y difference from right-click to Compress button
        public int CompressWidth { get; set; }    // Width of the Compress button
        public int CompressHeight { get; set; }   // Height of the Compress button
        public Point DockVerificationPixel { get; set; }
        public Color PreDockColor { get; set; }
        public bool IsActive { get; set; }
    }
}