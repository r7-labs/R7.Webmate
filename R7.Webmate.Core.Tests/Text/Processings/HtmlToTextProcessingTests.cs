using R7.Webmate.Core.Text.Processings;
using Xunit;

namespace R7.Webmate.Core.Tests.Text.Processings
{
    public class HtmlToTextProcessingTests
    {
        ITextProcessing TP = TextProcessingLoader.Load ("html-to-text.yml");

        [Fact]
        public void ReplaceNamedEntitesWithCharsTest ()
        {
            Assert.Equal ("A\u00A0\u2013 B", TP.Process ("A&nbsp;&ndash; B"));
            Assert.Equal ("\"&<>", TP.Process ("&quot;&amp;&laquo;&raquo;"));
        }

        [Fact]
        public void ReplaceNumericEntitesWithCharsTest ()
        {
            Assert.Equal ("A\u00A0\u2013 B", TP.Process ("A&#160;&#8211;&#32;B"));
        }
    }
}
