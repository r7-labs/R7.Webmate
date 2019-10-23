using System.Runtime.InteropServices;
using Xwt;

namespace R7.Webmate.Xwt
{
    public static class OSHelper
    {
        public static bool IsWindows () => RuntimeInformation.IsOSPlatform (OSPlatform.Windows);

        public static bool IsUnix () => RuntimeInformation.IsOSPlatform (OSPlatform.Linux);

        public static bool IsOSX () => RuntimeInformation.IsOSPlatform (OSPlatform.OSX);

        public static string GetPlatformString ()
        {
            if (IsWindows ()) {
                return "windows";
            }
            if (IsUnix ()) {
                return "unix";
            }
            if (IsOSX ()) {
                return "osx";
            }
            return "unknown";
        }

        public static ToolkitType GetDefaultXwtToolkitType ()
        {
            if (IsWindows ()) {
                return ToolkitType.Wpf;
            }
            if (IsUnix ()) {
                return ToolkitType.Gtk3;
            }
            return ToolkitType.Gtk;
        }
    }
}
