using R7.Webmate.Core.Text.Processings;
using Xunit;

namespace R7.Webmate.Core.Tests.Text.Processings
{
    public class TextToHtmlProcessingTests: TextProcessingTestBase
    {
        ITextProcessing TP = TextProcessingLoader.Load ("text-to-html.yml");

        [Fact]
        public void ReplaceEmailsWithLinksTest ()
        {
            Assert.Equal ("<p><a href=\"mailto:foo@bar.com\">foo@bar.com</a></p>", TP.Process ("foo@bar.com"));
            Assert.Equal ("<p><a href=\"mailto:foo.me@bar.com\">foo.me@bar.com</a></p>", TP.Process ("foo.me@bar.com"));
            Assert.Equal ("<p><a href=\"mailto:foo-me@bar.co.uk\">foo-me@bar.co.uk</a></p>", TP.Process ("foo-me@bar.co.uk"));

            Assert.Equal ("<p>foo@bar</p>", TP.Process ("foo@bar"));
            Assert.Equal ("<p>foo@bar.common</p>", TP.Process ("foo@bar.common"));
        }

        [Fact]
        public void DetectOrderedList ()
        {
            Assert.Equal ("<ol>\n<li>Hello,</li>\n<li>world!</li>\n</ol>", TP.Process ("1. Hello,\n2.world!"));
        }

        [Fact]
        public void DetectUnorderedList ()
        {
            Assert.Equal ("<ul>\n<li>Hello,</li>\n<li>world!</li>\n</ul>", TP.Process (X ("&bull; Hello,\n&bull;world!")));
        }
    }
}
