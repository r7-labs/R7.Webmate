using System.Text.RegularExpressions;

namespace R7.Webmate.Text.Commands
{
    public class RegexToLowerCommand: TextCommandBase
    {
        public string Pattern { get; set; }

        public RegexOptions RegexOptions { get; set; }

        public RegexToLowerCommand ()
        {}

        public RegexToLowerCommand (string pattern, RegexOptions regexOptions = RegexOptions.None)
        {
            Pattern = pattern;
            RegexOptions = regexOptions;
        }

        public override string Run (string text)
        {
            return Regex.Replace (text, Pattern, m => m.Value.ToLower (), RegexOptions);
        }
    }
}
