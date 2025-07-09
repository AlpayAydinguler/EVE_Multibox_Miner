using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace EVE_Multibox_Miner.Utilities
{
    public static class MouseSimulator
    {
        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
        private const uint MOUSEEVENTF_LEFTUP = 0x04;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const uint MOUSEEVENTF_RIGHTUP = 0x10;

        private static readonly Random random = new Random();

        public static void HumanLikeMove(Point target)
        {
            var start = Cursor.Position;
            var steps = random.Next(30, 60);
            var baseOffset = random.Next(15, 30);

            // Create control points for Bézier curve
            var control1 = new Point(
                start.X + random.Next(-baseOffset, baseOffset),
                start.Y + random.Next(-baseOffset, baseOffset)
            );

            var control2 = new Point(
                target.X + random.Next(-baseOffset, baseOffset),
                target.Y + random.Next(-baseOffset, baseOffset)
            );

            for (int i = 0; i <= steps; i++)
            {
                double t = (double)i / steps;
                double u = 1 - t;
                double tt = t * t;
                double uu = u * u;
                double uuu = uu * u;
                double ttt = tt * t;

                var finalTarget = new Point(
                    target.X + random.Next(-3, 3),
                    target.Y + random.Next(-3, 3)
                );

                Point point = new Point(
                    (int)(uuu * start.X + 3 * uu * t * control1.X + 3 * u * tt * control2.X + ttt * finalTarget.X),
                    (int)(uuu * start.Y + 3 * uu * t * control1.Y + 3 * u * tt * control2.Y + ttt * finalTarget.Y)
                );

                Cursor.Position = point;
                Thread.Sleep(random.Next(8, 20));

                if (random.Next(100) < 5)
                    Thread.Sleep(random.Next(30, 80));
            }

            Cursor.Position = target;
        }

        public static void LeftClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public static void RightClick()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }
    }
}