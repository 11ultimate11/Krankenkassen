using Krankenkassen.PlatformInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Image = System.Drawing.Image;
using Point = System.Drawing.Point;

namespace Krankenkassen
{
    public partial class PrintProcessor
    {
        Image image;
        readonly string[] extensions = new string[] { "jpg", "jpeg", "png" };
        public partial void Print(string path)
        {
            if (!File.Exists(path)) return;
            if (!extensions.Contains(Path.GetExtension(path))) return;
            image = Image.FromFile(path);
            if (image is null) return;
            PrintImage();
        }
        public partial void Print(Stream stream)
        {
            if (stream is null) return;
            image = Image.FromStream(stream);
            if (image is null) return;
            PrintImage();
        }
        #region Print Image

        #endregion
        private void PrintImage()
        {
            try
            {
                PrintDocument pd = new();
                pd.DefaultPageSettings.Landscape = image.Height > image.Width;
                pd.PrintPage+=Pd_PrintPage;
                pd.Print();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            Rectangle m = e.MarginBounds;
            GetBorders(image, m, out int h, out int w);
            m.Width = w;
            m.Height = h;
            e.Graphics.DrawImage(image, m);
        }
        private void GetBorders(Image img, Rectangle m, out int height, out int width)
        {
            float ratio = img.Width / img.Height;
            width = Math.Min(image.Width, m.Width);
            height = (int)(width/ratio);

        }
    }

}
