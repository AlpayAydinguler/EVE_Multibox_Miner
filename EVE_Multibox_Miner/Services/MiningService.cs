using EVE_Multibox_Miner.Models;
using EVE_Multibox_Miner.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace EVE_Multibox_Miner.Services
{
    public static class MiningService
    {
        private static Dictionary<string, List<Color>> colorSamples = new Dictionary<string, List<Color>>();
        private static Random random = new Random();

        public static void PerformMiningCycle(ClientProfile client)
        {
            // Check mining status
            if (IsMiningStopped(client))
            {
                ClientManagerService.Instance.SetClientState(client.Name, MiningState.Docking);
                return;
            }

            // Perform compression cycle
            PerformCompression(client);
        }

        private static bool IsMiningStopped(ClientProfile client)
        {
            if (!colorSamples.ContainsKey(client.Name))
            {
                colorSamples[client.Name] = new List<Color>();
            }

            // Capture screen color
            using (var bmp = new Bitmap(1, 1))
            using (var g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(client.MiningPixel, Point.Empty, new Size(1, 1));
                var color = bmp.GetPixel(0, 0);
                colorSamples[client.Name].Add(color);

                // Keep only last 5 samples
                if (colorSamples[client.Name].Count > 5)
                    colorSamples[client.Name].RemoveAt(0);
            }

            // Check if mining stopped
            return colorSamples[client.Name].Count == 5 &&
                   colorSamples[client.Name].TrueForAll(c => c == colorSamples[client.Name][0]);
        }

        public static void PerformCompression(ClientProfile client)
        {
            // Move to compression box
            int boxX = client.CompressionBox.X + random.Next(client.CompressionBox.Width);
            int boxY = client.CompressionBox.Y + random.Next(client.CompressionBox.Height);
            var target = new Point(boxX, boxY);

            MouseSimulator.HumanLikeMove(target);
            Thread.Sleep(500);
            MouseSimulator.LeftClick();
            Thread.Sleep(500);

            // Ctrl+A
            SendKeys.SendWait("^a");
            Thread.Sleep(500);

            // Right click
            MouseSimulator.RightClick();
            Thread.Sleep(1000);

            // OCR check for "Compress"
            if (!OCRService.IsCompressOptionVisible(client))
            {
                ClientManagerService.Instance.SetClientState(client.Name, MiningState.Docking);
            }
            else
            {
                // Move to compress button and click
                int compressX = client.CompressButton.X + random.Next(client.CompressButton.Width);
                int compressY = client.CompressButton.Y + random.Next(client.CompressButton.Height);
                var compressPoint = new Point(compressX, compressY);

                MouseSimulator.HumanLikeMove(compressPoint);
                Thread.Sleep(300);
                MouseSimulator.LeftClick();
                Thread.Sleep(500);
            }
        }
    }
}