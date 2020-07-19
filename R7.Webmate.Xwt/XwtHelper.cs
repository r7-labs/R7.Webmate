using R7.Webmate;
using Xwt;

namespace R7.Webmate.Xwt
{
    public static class XwtHelper
    {
        public static ToolkitType GetDefaultXwtToolkitType ()
        {
            if (PlatformHelper.IsWindows ()) {
                return ToolkitType.Wpf;
            }
            if (PlatformHelper.IsUnix ()) {
                return ToolkitType.Gtk3;
            }
            return ToolkitType.Gtk;
        }
    }
}
