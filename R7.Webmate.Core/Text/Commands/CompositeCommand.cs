using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace R7.Webmate.Core.Text.Commands
{
    public class CompositeCommand: TextCommandBase
    {
        [YamlIgnore]
        public string Comment { get; set; }

        public IList<ITextCommand> Commands { get; set; }

        public CompositeCommand ()
        {}

        public CompositeCommand (string comment, params ITextCommand [] commands)
        {
            Comment = comment;
            Commands = new List<ITextCommand> (commands);
        }

        public CompositeCommand (params ITextCommand [] commands)
		{
			Commands = new List<ITextCommand> (commands);
		}

        public CompositeCommand When (Func<bool> condition)
        {
            IsDisabled = !condition ();

            return this;
        }

        public CompositeCommand When (bool condition)
        {
            IsDisabled = !condition;

            return this;
        }

		public override string Execute (string value)
		{
			if (!IsDisabled) {
                foreach (var command in Commands) {
                    value = command.Execute (value);
                }
			}

			return value;
		}
	}
}
	