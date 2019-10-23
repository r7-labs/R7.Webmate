using System.Runtime.InteropServices;

namespace R7.Webmate.Core
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
    }
}
