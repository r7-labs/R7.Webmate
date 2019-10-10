namespace R7.Webmate.Core.Text.Commands
{
	public class ReplaceCommand: TextCommandBase
	{
		public ReplaceCommand (string pattern, string replacement)
		{
			Pattern = pattern;
			Replacement = replacement;
		}

		public string Pattern { get; set; }

		public string Replacement { get; set; }

		public override string Execute (string value)
		{
			if (IsEnabled) {
				return value.Replace (Pattern, Replacement);
			}

			return value; 
		}
	}

	public class TrimCommand: TextCommandBase
	{
		public TrimCommand (params char [] trimChars)
		{
            if (trimChars != null && trimChars.Length > 0) {
                TrimChars = trimChars;
            }
		}

		public char [] TrimChars { get; set; }

		public override string Execute (string value)
		{
			if (IsEnabled) {
                if (TrimChars != null && TrimChars.Length > 0) {
                    return value.Trim (TrimChars);
                }

				return value.Trim ();
			}

			return value;
		}
	}

	public class AppendCommand: TextCommandBase
    {
		public AppendCommand (string after)
		{
			After = after;
		}

		public string After { get; set; }

		public override string Execute (string value)
		{
			if (IsEnabled) {
				return value + After;
			}

			return value;
		}
	}

	public class PrependCommand: TextCommandBase
    {
		public PrependCommand (string before)
		{
			Before = before;
		}

		public string Before { get; set; }

		public override string Execute (string value)
		{
			if (IsEnabled) {
				return Before + value;
			}

			return value;
		}
	}
}

