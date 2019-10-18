using System.Collections.Generic;
using R7.Webmate.Core.Text.Commands;

namespace R7.Webmate.Core.Text.Processings
{
    public abstract class TextProcessingBase: ITextProcessing
    {
        public ITextProcessingParams Params { get; set; }

        public IList<ITextCommand> Commands { get; set; } = new List<ITextCommand> ();

        public void AddCommands (params ITextCommand [] commands)
        {
            foreach (var command in commands) {
                Commands.Add (command);
            }
        }

        public virtual string Execute (string text)
		{
			foreach (var command in Commands) {
                if (command is ExitCommand) {
                    break;
                }
                text = command.Execute (text);
            }

            return text;
		}
	}
}

