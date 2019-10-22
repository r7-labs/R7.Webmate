using Xunit;
using R7.Webmate.Core.Text.Processings;

namespace R7.Webmate.Core.Tests.Text.Processings
{
    public class TextProcessingLoaderTests
    {
        [Fact]
        public void LoadAddDefaultTest ()
        {
            var exception = Record.Exception (() => {
                TextProcessingLoader.LoadAllDefault ();
            });
            Assert.Null (exception);
        }
    }
}