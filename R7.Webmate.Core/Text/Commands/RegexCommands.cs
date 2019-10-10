using System.Text.RegularExpressions;

namespace R7.Webmate.Core.Text.Commands
{
	public class RegexReplaceCommand: TextCommandBase
    {
		public RegexReplaceCommand (string pattern, string replacement, RegexOptions regexOptions = RegexOptions.None)
		{
			Pattern = pattern;
			Replacement = replacement;
			RegexOptions = regexOptions;
		}

		public string Pattern { get; set; }

		public string Replacement { get; set; }

		public RegexOptions RegexOptions { get; set; } 

		public override string Execute (string value)
		{
			if (IsEnabled) {
				return Regex.Replace (value, Pattern, Replacement, RegexOptions);
			}

			return value; 
		}
	}
}

