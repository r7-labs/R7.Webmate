using System.Text.RegularExpressions;

namespace R7.Webmate.Core.Text.Commands
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

        public override string Execute (string value)
        {
            return Regex.Replace (value, Pattern, m => m.Value.ToLower (), RegexOptions);
        }
    }
}
