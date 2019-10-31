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
                TP.Process ("First line\r\nSecond line\rThird line\n"));
        }

        [Fact]
        public void RemoveExcessEmptyLinesTest ()
        {
            Assert.Equal ("First line\n\nSecond line\n\nThird line\nForth line",
                TP.Process ("First line\n\n\n\n\nSecond line\n\nThird line\nForth line"));

            Assert.Equal ("First line\n\nSecond line\n\nThird line\nForth line",
                TP.Process ("First line\n\t  \n\n \t \n\nSecond line\n \nThird line\nForth line"));
        }

        [Fact]
        public void RemoveSpaceBeforeClosingPunctuationTest ()
        {
            Assert.Equal ("Some sentence.", TP.Process ("Some sentence ."));
            Assert.Equal ("Some sentence!", TP.Process ("Some sentence !"));
            Assert.Equal ("Some sentence?", TP.Process ("Some sentence ?"));
            Assert.Equal ("Some sentence, and more", TP.Process ("Some sentence , and more"));
            Assert.Equal ("Some sentence; and more", TP.Process ("Some sentence ; and more"));
            Assert.Equal ("Some sentence: and more", TP.Process ("Some sentence : and more"));
            Assert.Equal ("(Some sentence in brackets)", TP.Process ("(Some sentence in brackets )"));
            Assert.Equal ("[Some sentence in brackets]", TP.Process ("[Some sentence in brackets ]"));
            Assert.Equal ("{Some sentence in brackets}", TP.Process ("{Some sentence in brackets }"));
        }

        [Fact]
        public void RemoveExtraPunctuationAfterClosingBracketTest ()
        {
            Assert.Equal ("(Some sentence in brackets).", TP.Process ("(Some sentence in brackets.)."));
            Assert.Equal ("(Some sentence in brackets)!", TP.Process ("(Some sentence in brackets!)."));
            Assert.Equal ("(Some sentence in brackets)?", TP.Process ("(Some sentence in brackets?)."));
        }

        [Fact]
        public void FixCommonEnglishAbbreviationsTest ()
        {
            Assert.Equal ("i.e.", TP.Process ("i. e."));
            Assert.Equal ("e.g.", TP.Process ("e. g."));
            Assert.Equal ("a.k.a.", TP.Process ("a. k. a."));
            Assert.Equal ("e.t.a.", TP.Process ("e. t. a."));
        }

        [Fact]
        public void RemoveNbspsWithSpacesAroundTest ()
        {
            // include text in //, as Trim() removes also &nbsps;
            Assert.Equal ("/ /", TP.Process (X ("/&nbsp; /")));
            Assert.Equal ("/ /", TP.Process (X ("/ &nbsp;/")));
            Assert.Equal ("/ /", TP.Process (X ("/&nbsp; &nbsp;  &nbsp;&nbsp;   &nbsp;/")));
        }

        [Fact]
        public void RemoveDuplicateNbspTest ()
        {
            // include text in //, as Trim() removes also &nbsps;
            Assert.Equal ("/&nbsp;/", XX (TP.Process (X ("/&nbsp;&nbsp;/"))));
        }

        [Fact]
        public void FixRussianTyposTest ()
        {
            Assert.Equal (X ("Это случилось в 1990&nbsp;г."), TP.Process ("Это случилось в 1990г."));
            Assert.Equal (X ("Это случилось в 1990-1991&nbsp;гг."), TP.Process ("Это случилось в 1990-1991 г.г."));
            Assert.Equal (X ("Это стоит 15&nbsp;р."), TP.Process ("Это стоит 15р."));
            Assert.Equal (X ("Это стоит 15&nbsp;руб."), TP.Process ("Это стоит 15руб."));
            Assert.Equal ("\"Сельское хозяйство\" сокращается как с.-х., с.-х., с.-х.", TP.Process ("\"Сельское хозяйство\" сокращается как с/х, с\\х, с.х."));
        }
    }
}
