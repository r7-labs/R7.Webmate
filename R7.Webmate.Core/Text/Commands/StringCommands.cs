namespace R7.Webmate.Core.Text.Commands
{
	public class ReplaceCommand: TextCommandBase
	{
        public string Pattern { get; set; }

        public string Replacement { get; set; }

        public ReplaceCommand ()
        {}

        public ReplaceCommand (string pattern, string replacement)
		{
			Pattern = pattern;
			Replacement = replacement;
		}

		public override string Execute (string value)
		{
			if (!IsDisabled) {
				return value.Replace (Pattern, Replacement);
			}

			return value; 
		}
	}

	public class TrimCommand: TextCommandBase
	{
        public TrimCommand ()
        {}

        public string TrimChars { get; set; }

        public TrimCommand (string trimChars = null)
		{
            if (!string.IsNullOrEmpty (trimChars)) {
                TrimChars = trimChars;
            }
		}

		public override string Execute (string value)
		{
			if (!IsDisabled) {
                if (!string.IsNullOrEmpty (TrimChars)) {
                    return value.Trim (TrimChars.ToCharArray ());
                }

				return value.Trim ();
			}

			return value;
		}
	}

	public class AppendCommand: TextCommandBase
    {
        public string After { get; set; }

        public AppendCommand ()
        {}

		public AppendCommand (string after)
		{
			After = after;
		}

		public override string Execute (string value)
		{
			if (!IsDisabled) {
				return value + After;
			}

			return value;
		}
	}

	public class PrependCommand: TextCommandBase
    {
        public string Before { get; set; }

        public PrependCommand ()
        {}

		public PrependCommand (string before)
		{
			Before = before;
		}

		public override string Execute (string value)
		{
			if (!IsDisabled) {
				return Before + value;
			}

			return value;
		}
	}
}

