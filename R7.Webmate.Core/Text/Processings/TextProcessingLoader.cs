using System.IO;

namespace R7.Webmate.Core.Text.Processings
{
    public static class TextProcessingLoader
    {
        public static ITextProcessing LoadDefaultFromFile (string fname)
        {
            // TODO: Support for user-defined processings
            // TODO: Load only text
            // TODO: Provide custom deserializer
            var serializer = new TextProcessingSerializer (); 
            return serializer.Deserialize (File.ReadAllText (Path.Combine ("./resources/processings/default", fname)));
        }
    }
}
