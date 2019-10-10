using System;
using System.Collections.Generic;

namespace R7.Webmate.Core.Text.Commands
{
	public class CompositeCommand: TextCommandBase
    {
		public CompositeCommand (params ITextCommand [] commands)
		{
			Commands = new List<ITextCommand> (commands);
		}
        		
        public CompositeCommand When (Func<bool> condition)
        {
            IsEnabled = condition ();

            return this;
        }

        public CompositeCommand When (bool when)
        {
            IsEnabled = when;

            return this;
        }

        protected List<ITextCommand> Commands;

		public override string Execute (string value)
		{
			if (IsEnabled) {
				foreach (var command in Commands)
					value = command.Execute (value);
			}

			return value;
		}
	}
}
	