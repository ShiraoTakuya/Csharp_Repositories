using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
class Program
{
    static void Main(string[] args)
    {
        int count = 0;
        Bitmap previousImage = null;
        string folderPath = @"C:\Screenshots\";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        while (true)
        {
            Bitmap currentImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            Graphics.FromImage(currentImage).CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            if (previousImage != null)
            {
                int differentPixels = GetDifferentPixels(previousImage, currentImage);
                if (differentPixels > 100)
                {
                    string filename = count.ToString("D10") + ".png";
                    string filePath = Path.Combine(folderPath, filename);
                    currentImage.Save(filePath, ImageFormat.Png);
                    count++;
                }
            }
            previousImage = currentImage;
            System.Threading.Thread.Sleep(1000);
        }
    }
    static int GetDifferentPixels(Bitmap image1, Bitmap image2)
    {
        int count = 0;
        for (int x = 0; x < image1.Width; x++)
        {
            for (int y = 0; y < image1.Height; y++)
            {
                if (image1.GetPixel(x, y) != image2.GetPixel(x, y))
                {
                    count++;
                }
            }
        }
        return count;
    }
}