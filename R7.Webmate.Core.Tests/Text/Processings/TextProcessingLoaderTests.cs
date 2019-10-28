using Xunit;
using R7.Webmate.Core.Text.Processings;

namespace R7.Webmate.Core.Tests.Text.Processings
{
    public class TextProcessingLoaderTests
    {
        [Fact]
        public void LoadAllTest ()
        {
            var exception = Record.Exception (() => {
                TextProcessingLoader.LoadAll ();
            });
            Assert.Null (exception);
        }

        [Fact]
        public void LoadDefaultTest ()
        {
            var defaultProcessing = TextProcessingLoader.LoadDefault ("hello-world.yml", "../../../resources/processings");
            Assert.Equal ("Hello, world!", defaultProcessing.Process (""));
        }

        [Fact]
        public void LoadTest ()
        {
            var userProcessing = TextProcessingLoader.Load ("hello-world.yml", "../../../resources/processings");
            Assert.Equal ("Hello, another world!", userProcessing.Process (""));
        }
    }
}