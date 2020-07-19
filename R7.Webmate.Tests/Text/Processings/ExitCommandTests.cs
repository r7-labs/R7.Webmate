using R7.Webmate.Text.Processings;
using Xunit;

namespace R7.Webmate.Tests.Text.Processings
{
    public class ExitCommandTests: TextProcessingTestBase
    {
        [Fact]
        public void ExitCommandTest ()
        {
            var TP = TextProcessingLoader.Load ("exit-command.yml", TestProcessingsPath);
            Assert.Equal ("This should happen.", TP.Process (""));
        }
    }
}
