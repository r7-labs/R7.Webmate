using Xwt;
using Xwt.Drawing;

namespace R7.Webmate.Xwt.Icons
{
    public static class ImageExtensions
    {
        private static double GetIconSizePx (IconSize iconSize)
        {
            switch (iconSize) {
                case IconSize.Medium: return 24;
                case IconSize.Large: return 32;
                case IconSize.Small:
                default: return 16;
            }
        }

        public static Image WithBoxSize (this Image image, IconSize iconSize)
        {
            return image.WithBoxSize (GetIconSizePx (iconSize));
        }
    }
}
