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
                var tp = TextProcessingFactory.CreateTextToTextProcessing ();
                var tpSerializer = new TextProcessingSerializer ();
                var yaml = tpSerializer.Serialize (tp);
                var tp2 = tpSerializer.Deserialize (yaml);
            });
            Assert.Null (exception);
        }
    }
}
