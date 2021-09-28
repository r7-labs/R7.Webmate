using R7.Webmate;
using Xwt.Drawing;

namespace R7.Webmate.Xwt.Icons
{
    public static class IconHelper
    {
        public static Image GetIcon (IconStyle style, string name)
        {
            // TODO: Handle possible exceptions
            // TODO: Cache loaded icons
            return Image.FromFile ($"./resources/icons/{style.ToString ().ToLowerInvariant ()}/{name}{GetIconExtension ()}");
        }

        public static Image GetIcon (string name)
        {
            return GetIcon (IconStyle.Solid, name);
        }

        public static Image GetAppIcon ()
        {
            // TODO: Handle possible exceptions
            if (PlatformHelper.IsWindows ()) {
                return Image.FromFile ("./resources/app-icons/r7-webmate-32px.png");
            }
            return Image.FromFile ("./resources/app-icons/r7-webmate.svg");
        }

        public static string GetIconExtension ()
        {
            if (PlatformHelper.IsWindows ()) {
                return ".png";
            }
            return ".svg";
        }
    }
}
