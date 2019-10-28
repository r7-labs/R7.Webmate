using System.Collections.Generic;
using System.IO;

namespace R7.Webmate.Core.Text.Processings
{
    // TODO: Exception handling
    // TODO: Support for loading text w/o deserialization
    // TODO: Support for loading from text, not file
    public static class TextProcessingLoader
    {
        public static string BasePath { get; private set; } = "./resources/processings";

        // TODO: Provide custom deserializer
        static TextProcessingSerializer Deserializer = new TextProcessingSerializer ();

        public static ITextProcessing Load (string fileName)
        {
            return Load (fileName, BasePath);
        }

        public static ITextProcessing LoadDefault (string fileName, string basePath)
        {
            return LoadInternal (Path.Combine (basePath, fileName));
        }

        public static ITextProcessing Load (string fileName, string basePath)
        {
            return LoadInternal (
                FileHelper.GetFirstSuffixedOrDefaultFile (Path.Combine (basePath, fileName), ".user").FullName);
        }

        public static IList<ITextProcessing> LoadAll ()
        {
            return LoadAll (BasePath);
        }

        public static IList<ITextProcessing> LoadAll (string basePath)
        {
            var processings = new List<ITextProcessing> ();
            var processingFiles = Directory.GetFiles (basePath, "*.yml");
            foreach (var processingFile in processingFiles) {
                processings.Add (Load (Path.GetFileName (processingFile)));
            }

            return processings;
        }

        static ITextProcessing LoadInternal (string filePath)
        {
            return Deserializer.Deserialize (File.ReadAllText (filePath));
        }
    }
}
