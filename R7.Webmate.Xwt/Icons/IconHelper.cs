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
            if (OSHelper.IsWindows ()) {
                return Image.FromFile ("./resources/app-icons/r7-webmate-128px.png");
            }
            return Image.FromFile ("./resources/app-icons/r7-webmate-plain.svg");
        }

        public static string GetIconExtension ()
        {
            if (OSHelper.IsWindows ()) {
                return ".png";
            }
            return ".svg";
        }
    }
}
