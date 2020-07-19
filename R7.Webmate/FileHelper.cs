using System.IO;

namespace R7.Webmate
{
    public static class FileHelper
    {
        public static string GetSuffixedFilePath (string path, string suffix)
        {
            return Path.Combine (
                Path.GetDirectoryName (path),
                Path.GetFileNameWithoutExtension (path) + suffix + Path.GetExtension (path)
            );
        }

        public static string GetUserFilePath (string path)
        {
            return GetSuffixedFilePath (path, ".user");
        }

        public static FileInfo GetFirstSuffixedOrDefaultFile (string path, params string [] suffixes)
        {
            foreach (var suffix in suffixes) {
                var suffixedPath = GetSuffixedFilePath (path, suffix);
                if (File.Exists (suffixedPath)) {
                    return new FileInfo (suffixedPath);
                }
            }
            if (File.Exists (path)) {
                return new FileInfo (path);
            }
            return null;
        }

        public static FileInfo GetUserOrDefaultFile (string path)
        {
            return GetFirstSuffixedOrDefaultFile (path, ".user");
        }
    }
}
