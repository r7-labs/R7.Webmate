using R7.Webmate.Core.Text.Commands;

namespace R7.Webmate.Core.Text.Processings
{
	public class TextToTextProcessing : TextProcessingBase
    {
        protected override void Build ()
        {
            Command = new CompositeCommand (

                // normalize endlines
                new CompositeCommand (
                    new ReplaceCommand ("\r\n", "\n"),
                    new ReplaceCommand ("\r", "\n")),

                // replace tabs with spaces
                new CompositeCommand (
                    new ReplaceCommand ("\t", " ")),

                /*
                // add spaces after punctuation
                new CompositeCommand (
                    new ReplaceCommand (",", ", "),
                    new ReplaceCommand ("!", "! ")),
                */

                // remove spaces before closing punctuation
                new CompositeCommand (
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

                // remove extra punctuation before closing parenthesis
                new CompositeCommand (
                    new ReplaceCommand (".).", ".)"),
                    new ReplaceCommand ("!)!", "!)"),
                    new ReplaceCommand ("?)?", "?)")),

                // replace figure quotes in text output
                new CompositeCommand (
                    new ReplaceCommand ("«", "\""),
                    new ReplaceCommand ("»", "\"")),

                // remove hyphens
                new CompositeCommand (
                    new ReplaceCommand ("¬", string.Empty)),

				// fix some common typos
				new CompositeCommand (
					new ReplaceCommand ("г.г.", "гг."),
					new ReplaceCommand ("с\\х", "с.-х."),
					new ReplaceCommand ("с/х", "с.-х."),
					new ReplaceCommand ("с.х.", "с.-х.")),

                // remove duplicate whitespace
                new CompositeCommand (
                    new RegexReplaceCommand (@"\s+", " ")),

                // trim text
                new CompositeCommand (
					new TrimCommand ())
			);
		}
	}
}
