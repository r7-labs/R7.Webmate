using System.Collections.Generic;
using System.IO;

namespace R7.Webmate.Core.Text.Processings
{
    public static class TextProcessingLoader
    {
        public static ITextProcessing LoadDefaultFromFile (string fname)
        {
            // TODO: Exception handling
            // TODO: Support for user-defined processings
            // TODO: Load only text
            // TODO: Provide custom deserializer
            var serializer = new TextProcessingSerializer ();
            return serializer.Deserialize (File.ReadAllText (Path.Combine ("./resources/processings/default", fname)));
        }

        public static IList<ITextProcessing> LoadAllDefault ()
        {
            // TODO: Exception handling
            var processings = new List<ITextProcessing> ();
            var processingFiles = Directory.GetFiles ("./resources/processings/default", "*.yml");
            foreach (var processingFile in processingFiles) {
                processings.Add (LoadDefaultFromFile (Path.GetFileName (processingFile)));
            }

            return processings;
        }
    }
}
