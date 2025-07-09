using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace EVE_Multibox_Miner.Utilities
{
    public static class WindowHelper
    {
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(POINT point);

        [DllImport("user32.dll")]
        private static extern bool ScreenToClient(IntPtr hWnd, ref POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern bool ClientToScreen(IntPtr hWnd, ref POINT lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public Rectangle ToRectangle()
            {
                return new Rectangle(Left, Top, Right - Left, Bottom - Top);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                X = x;
                Y = y;
            }

            public Point ToPoint()
            {
                return new Point(X, Y);
            }
        }

        public static void ActivateWindow(Rectangle windowArea)
        {
            // Find window at center point
            var center = new Point(windowArea.Left + windowArea.Width / 2,
                                  windowArea.Top + windowArea.Height / 2);

            POINT pt = new POINT(center.X, center.Y);
            IntPtr hWnd = WindowFromPoint(pt);

            if (hWnd != IntPtr.Zero)
            {
                SetForegroundWindow(hWnd);
                Thread.Sleep(300); // Allow window to activate
            }
        }

        public static Point TranslatePointToWindow(Point screenPoint, IntPtr hWnd)
        {
            POINT pt = new POINT(screenPoint.X, screenPoint.Y);
            ScreenToClient(hWnd, ref pt);
            return pt.ToPoint();
        }

        public static Point TranslatePointToScreen(Point clientPoint, IntPtr hWnd)
        {
            POINT pt = new POINT(clientPoint.X, clientPoint.Y);
            ClientToScreen(hWnd, ref pt);
            return pt.ToPoint();
        }
    }
}