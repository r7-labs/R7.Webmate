using System.Text.RegularExpressions;

namespace R7.Webmate.Text.Commands
{
	public class RegexReplaceCommand: TextCommandBase
    {
        // TODO: Rename to "Match"?
        public string Pattern { get; set; }

        // TODO: Rename to "With"
        public string Replacement { get; set; }

        public RegexOptions RegexOptions { get; set; }

        public RegexReplaceCommand ()
        {}

        public RegexReplaceCommand (string pattern, string replacement, RegexOptions regexOptions = RegexOptions.None)
		{
			Pattern = pattern;
			Replacement = replacement;
			RegexOptions = regexOptions;
		}

		public override string Run (string text)
		{
			return Regex.Replace (text, Pattern, Replacement, RegexOptions);
		}
	}
}
