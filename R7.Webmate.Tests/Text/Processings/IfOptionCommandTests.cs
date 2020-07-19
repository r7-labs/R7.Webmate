using R7.Webmate.Text.Processings;
using Xunit;

namespace R7.Webmate.Tests.Text.Processings
{
    public class IfOptionCommandTests: TextProcessingTestBase
    {
        [Fact]
        public void IfOptionCommandTest ()
        {
            var TP = TextProcessingLoader.Load ("if-option-command.yml", TestProcessingsPath);
            Assert.Equal ("option1 is true;option2 is false;option3 is false;", TP.Process (""));
        }
    }
}
