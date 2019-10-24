using R7.Webmate.Core.Text.Processings;
using Xunit;

namespace R7.Webmate.Core.Tests.Text.Processings
{
    public class TextToTextProcessingTests: TextProcessingTestBase
    {
        ITextProcessing TP = TextProcessingLoader.Load ("text-to-text.yml");

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
            Assert.Equal ("(Some sentence in brackets).", TP.Execute ("(Some sentence in brackets.)."));
            Assert.Equal ("(Some sentence in brackets)!", TP.Execute ("(Some sentence in brackets!)."));
            Assert.Equal ("(Some sentence in brackets)?", TP.Execute ("(Some sentence in brackets?)."));
        }

        [Fact]
        public void FixCommonEnglishAbbreviationsTest ()
        {
            Assert.Equal ("i.e.", TP.Execute ("i. e."));
            Assert.Equal ("e.g.", TP.Execute ("e. g."));
            Assert.Equal ("a.k.a.", TP.Execute ("a. k. a."));
            Assert.Equal ("e.t.a.", TP.Execute ("e. t. a."));
        }

        [Fact]
        public void RemoveNbspsWithSpacesAroundTest ()
        {
            // include text in //, as Trim() removes also &nbsps;
            Assert.Equal ("/ /", TP.Execute (X ("/&nbsp; /")));
            Assert.Equal ("/ /", TP.Execute (X ("/ &nbsp;/")));
            Assert.Equal ("/ /", TP.Execute (X ("/&nbsp; &nbsp;  &nbsp;&nbsp;   &nbsp;/")));
        }

        [Fact]
        public void RemoveDuplicateNbspTest ()
        {
            // include text in //, as Trim() removes also &nbsps;
            Assert.Equal ("/&nbsp;/", XX (TP.Execute (X ("/&nbsp;&nbsp;/"))));
        }

        [Fact]
        public void FixCommonTyposInRussianTest ()
        {
            Assert.Equal (X ("Это случилось в 1990&nbsp;г."), TP.Execute ("Это случилось в 1990г."));
            Assert.Equal (X ("Это случилось в 1990-1991&nbsp;гг."), TP.Execute ("Это случилось в 1990-1991 г.г."));
            Assert.Equal (X ("Это стоит 15&nbsp;р."), TP.Execute ("Это стоит 15р."));
            Assert.Equal (X ("Это стоит 15&nbsp;руб."), TP.Execute ("Это стоит 15руб."));
            Assert.Equal ("\"Сельское хозяйство\" сокращается как с.-х., с.-х., с.-х.", TP.Execute ("\"Сельское хозяйство\" сокращается как с/х, с\\х, с.х."));
        }
    }
}
