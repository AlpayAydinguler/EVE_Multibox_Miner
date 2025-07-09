using EVE_Multibox_Miner.Models;
using EVE_Multibox_Miner.Utilities;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Tesseract;

namespace EVE_Multibox_Miner.Services
{
    public static class OCRService
    {
        private static TesseractEngine _engine;
        private static readonly object _lock = new object();

        static OCRService()
        {
            try
            {
                if (_engine == null)
                {
                    _engine = new TesseractEngine("./tessdata", "eng", EngineMode.Default);
                    _engine.SetVariable("tessedit_char_whitelist", "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890() ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"OCR initialization failed: {ex.Message}");
            }
        }

        public static bool IsCompressOptionVisible(Rectangle menuRect)
        {
            try
            {
                if (_engine == null) return true; // Fallback if OCR failed

                // Capture the compress button area
                using (var bitmap = CaptureScreenArea(menuRect))
                {
                    // Preprocess image for better OCR
                    using (var processed = PreprocessImage(bitmap))
                    {
                        // Perform OCR
                        using (var page = _engine.Process(processed))
                        {
                            string text = page.GetText().ToLower();
                            return text.Contains("compress");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"OCR Error: {ex.Message}");
                return true; // Default to visible on error
            }
        }

        private static Bitmap CaptureScreenArea(Rectangle area)
        {
            var bitmap = new Bitmap(area.Width, area.Height, PixelFormat.Format24bppRgb);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(area.Location, Point.Empty, area.Size);
            }

            return bitmap;
        }

        private static Pix PreprocessImage(Bitmap original)
        {
            // Create high-contrast version
            var highContrast = new Bitmap(original.Width, original.Height);

            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    Color pixel = original.GetPixel(x, y);
                    int brightness = (int)(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);
                    int newValue = brightness < 150 ? 0 : 255;
                    highContrast.SetPixel(x, y, Color.FromArgb(newValue, newValue, newValue));
                }
            }

            // Convert to Pix
            return BitmapToPix(highContrast);
        }

        private static Pix BitmapToPix(Bitmap bitmap)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Use fully qualified name for ImageFormat
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                return Pix.LoadFromMemory(memoryStream.ToArray());
            }
        }
    }
}