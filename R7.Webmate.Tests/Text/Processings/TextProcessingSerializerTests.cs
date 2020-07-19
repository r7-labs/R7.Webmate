using Xunit;
using R7.Webmate.Text.Processings;

namespace R7.Webmate.Tests.Text.Processings
{
    public class TextProcessingSerializerTests
    {
        [Fact]
        public void RoundtripTest ()
        {
            var exception = Record.Exception (() => {
                var tp = TextProcessingLoader.Load ("text-to-text.yml");
                var serializer = new TextProcessingSerializer ();
                serializer.Serialize (tp);
            });
            Assert.Null (exception);
        }
    }
}
