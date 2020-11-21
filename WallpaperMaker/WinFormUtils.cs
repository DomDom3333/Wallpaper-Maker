using System.Drawing;
using System.Windows.Forms;

namespace WallpaperMaker.WinForm
{
    public class WinFormUtils
    {
        internal static int grabXRes()
        {
            Rectangle Monitor = Screen.PrimaryScreen.Bounds;
            return Monitor.Width;
        }
        internal static int grabYRes()
        {
            Rectangle Monitor = Screen.PrimaryScreen.Bounds;
            return Monitor.Height;
        }
    }
}
