using System;
using Xwt;
using Xwt.Drawing;

namespace R7.Webmate.Xwt.Icons
{
    public static class FAIconHelper
    {
        public static Image GetIcon (FAIconStyle style, string name)
        {
            // TODO: Cache loaded images
            return Image.FromFile ($"./resources/icons/{style.ToString ().ToLowerInvariant ()}/{name}.svg");
        }

        public static Image GetIcon (string name)
        {
            return GetIcon (FAIconStyle.Solid, name);
        }
    }
}
