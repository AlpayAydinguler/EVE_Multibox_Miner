using EVE_Multibox_Miner.Models;
using EVE_Multibox_Miner.Utilities;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EVE_Multibox_Miner.Services
{
    public static class DockingService
    {
        public static void PerformDocking(ClientProfile client)
        {
            // Press Shift+R
            SendKeys.SendWait("+R");
            Thread.Sleep(1000);

            // Calculate random point in dock button area
            int dockX = client.DockButton.X + new Random().Next(client.DockButton.Width);
            int dockY = client.DockButton.Y + new Random().Next(client.DockButton.Height);
            var dockPoint = new Point(dockX, dockY);

            // Move and click
            MouseSimulator.HumanLikeMove(dockPoint);
            Thread.Sleep(500);
            MouseSimulator.LeftClick();
            Thread.Sleep(2000);

            // Start verification
            VerifyDocking(client);
        }

        private static async void VerifyDocking(ClientProfile client)
        {
            await Task.Delay(60000); // Wait 60 seconds

            // Check docking pixel
            if (IsDocked(client))
            {
                ClientManagerService.Instance.SetClientState(client.Name, MiningState.Docked);
            }
            else
            {
                // Retry docking or handle error
                PerformDocking(client);
            }
        }

        private static bool IsDocked(ClientProfile client)
        {
            // Capture pixel color at verification point
            using (var bmp = new Bitmap(1, 1))
            using (var g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(client.DockVerificationPixel, Point.Empty, new Size(1, 1));
                Color color = bmp.GetPixel(0, 0);

                // Compare with expected docked color
                return color != client.PreDockColor;
            }
        }
    }
}