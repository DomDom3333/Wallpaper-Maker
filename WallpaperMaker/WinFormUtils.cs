using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using SkiaSharp;

namespace WallpaperMaker.WinForm;

public static class WinFormUtils
{
    internal static int GrabXRes()
    {
        var bounds = Screen.PrimaryScreen?.Bounds ?? new Rectangle(0, 0, 1920, 1080);
        return bounds.Width;
    }

    internal static int GrabYRes()
    {
        var bounds = Screen.PrimaryScreen?.Bounds ?? new Rectangle(0, 0, 1920, 1080);
        return bounds.Height;
    }

    internal static Bitmap SKBitmapToWinFormsBitmap(SKBitmap skBitmap)
    {
        using var image = SKImage.FromBitmap(skBitmap);
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        using var stream = new MemoryStream(data.ToArray());
        return new Bitmap(stream);
    }

    internal static SKColor DrawingColorToSKColor(Color color)
    {
        return new SKColor(color.R, color.G, color.B, color.A);
    }

    internal static Color SKColorToDrawingColor(SKColor color)
    {
        return Color.FromArgb(color.Alpha, color.Red, color.Green, color.Blue);
    }
}
