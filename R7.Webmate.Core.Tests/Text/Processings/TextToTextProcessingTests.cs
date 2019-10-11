using R7.Webmate.Core.Text.Processings;
using Xunit;

namespace R7.Webmate.Core.Tests.Text.Processings
{
    public class TextToTextProcessingTests
    {
        ITextProcessing TP = TextProcessingLoader.LoadDefaultFromFile ("text-to-text.yml");

        [Fact]
        public void NormalizeEndlinesTest ()
        {
            Assert.Equal ("First line\nSecond line\nThird line",
                TP.Execute ("First line\r\nSecond line\rThird line\n"));
        }

        [Fact]
        public void RemoveExcessEmptyLinesTest ()
        {
            Assert.Equal ("First line\n\nSecond line\n\nThird line\nForth line",
                TP.Execute ("First line\n\n\n\n\nSecond line\n\nThird line\nForth line"));
        }

        [Fact]
        public void RemoveSpaceBeforeClosingPunctuationTest ()
        {
            Assert.Equal ("Some sentence.", TP.Execute ("Some sentence ."));
            Assert.Equal ("Some sentence!", TP.Execute ("Some sentence !"));
            Assert.Equal ("Some sentence?", TP.Execute ("Some sentence ?"));
            Assert.Equal ("Some sentence, and more", TP.Execute ("Some sentence , and more"));
            Assert.Equal ("Some sentence; and more", TP.Execute ("Some sentence ; and more"));
            Assert.Equal ("Some sentence: and more", TP.Execute ("Some sentence : and more"));
            Assert.Equal ("(Some sentence in brackets)", TP.Execute ("(Some sentence in brackets )"));
            Assert.Equal ("[Some sentence in brackets]", TP.Execute ("[Some sentence in brackets ]"));
            Assert.Equal ("{Some sentence in brackets}", TP.Execute ("{Some sentence in brackets }"));
        }

        [Fact]
        public void RemoveExtraPunctuationAfterClosingBracketTest ()
        {
            Assert.Equal ("(Some sentence in brackets.)", TP.Execute ("(Some sentence in brackets.)."));
            Assert.Equal ("(Some sentence in brackets!)", TP.Execute ("(Some sentence in brackets!)."));
            Assert.Equal ("(Some sentence in brackets?)", TP.Execute ("(Some sentence in brackets?)."));
        }
    }
}
