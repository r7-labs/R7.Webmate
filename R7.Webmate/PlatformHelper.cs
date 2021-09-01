using System.Runtime.InteropServices;

namespace R7.Webmate
{
    public static class PlatformHelper
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

        public static string GetBrowserCommand()
        {
            if (IsWindows ()) {
                return "explorer";
            }
            if (IsUnix ()) {
                return "x-www-browser";
            }
            return "unknown";
        }
    }
}
