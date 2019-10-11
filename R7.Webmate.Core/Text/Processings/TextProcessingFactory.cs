using System;
using R7.Webmate.Core.Text.Commands;

namespace R7.Webmate.Core.Text.Processings
{
    [Obsolete]
    public static class TextProcessingFactory
    {
        public static ITextProcessing CreateTextToTextProcessing ()
        {
            var textProcessing = new TextProcessing ();
            textProcessing.AddCommands (

                new CompositeCommand ("normalize endlines",
                    new ReplaceCommand ("\r\n", "\n"),
                    new ReplaceCommand ("\r", "\n")),

                    new CompositeCommand ("replace tabs with spaces",
                        new ReplaceCommand ("\t", " ")),

                    /*
                    new CompositeCommand ("add spaces after punctuation",
                        new ReplaceCommand (",", ", "),
                        new ReplaceCommand ("!", "! ")),
                    */

                    new CompositeCommand ("remove spaces before closing punctuation",
                        new ReplaceCommand (" .", "."),
                        new ReplaceCommand (" ,", ","),
                        new ReplaceCommand (" ;", ";"),
                        new ReplaceCommand (" :", ":"),
                        new ReplaceCommand (" )", ")"),
                        new ReplaceCommand (" }", "}"),
                        new ReplaceCommand (" ]", "]"),
                        new ReplaceCommand (" ?", "?"),
                        new ReplaceCommand (" !", "!"),
                        new RegexReplaceCommand (@"\s+", " ")),

                    new CompositeCommand ("remove extra punctuation before closing parenthesis",
                        new ReplaceCommand (".).", ".)"),
                        new ReplaceCommand ("!)!", "!)"),
                        new ReplaceCommand ("?)?", "?)")),

                    new CompositeCommand ("replace figure quotes in text output",
                        new ReplaceCommand ("«", "\""),
                        new ReplaceCommand ("»", "\"")),

                    new CompositeCommand ("remove hyphens",
                        new ReplaceCommand ("¬", string.Empty)),

                    new CompositeCommand ("fix some common typos",
                        new ReplaceCommand ("г.г.", "гг."),
                        new ReplaceCommand ("с\\х", "с.-х."),
                        new ReplaceCommand ("с/х", "с.-х."),
                        new ReplaceCommand ("с.х.", "с.-х.")),

                    new CompositeCommand ("remove duplicate whitespace",
                        new RegexReplaceCommand (@"\s+", " ")),

                    // trim text
                    new CompositeCommand (
                        new TrimCommand ())
                );

            return textProcessing;
        }
    }
}
