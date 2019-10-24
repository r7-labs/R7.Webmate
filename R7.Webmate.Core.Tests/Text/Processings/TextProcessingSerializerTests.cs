using Xunit;
using R7.Webmate.Core.Text.Processings;

namespace R7.Webmate.Core.Tests.Text.Processings
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
