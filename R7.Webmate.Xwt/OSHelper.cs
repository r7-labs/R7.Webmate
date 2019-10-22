using System.Runtime.InteropServices;
using Xwt;

namespace R7.Webmate.Xwt
{
    public static class OSHelper
    {
        public static bool IsWindows () => RuntimeInformation.IsOSPlatform (OSPlatform.Windows);

        public static bool IsLinux () => RuntimeInformation.IsOSPlatform (OSPlatform.Linux);

        public static bool IsOSX () => RuntimeInformation.IsOSPlatform (OSPlatform.OSX);

        public static ToolkitType GetXwtToolkit ()
        {
            if (IsWindows ()) {
                return ToolkitType.Wpf;
            }
            return ToolkitType.Gtk3;
        }
    }
}
