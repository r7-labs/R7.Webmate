using System.Collections.Generic;
using R7.Webmate.Core.Text.Commands;
using YamlDotNet.Serialization;

namespace R7.Webmate.Core.Text.Processings
{
    public abstract class TextProcessingBase: ITextProcessing
    {
        [YamlMember (Order = 1)]
        public IDictionary<string, bool> Options { get; set; } = new Dictionary<string, bool> ();

        [YamlMember (Order = 2)]
        public IList<ITextCommand> Commands { get; set; } = new List<ITextCommand> ();

        public void AddCommands (params ITextCommand [] commands)
        {
            foreach (var command in commands) {
                Commands.Add (command);
            }
        }

        public virtual string Process (string text)
        {
            var exit = false;
            return Process (text, Commands, ref exit);
        }

        protected virtual string Process (string text, IList<ITextCommand> commands, ref bool exit)
        {
            if (commands == null || commands.Count == 0) {
                return text;
            }

            foreach (var command in commands) {
                if (exit) {
                    break;
                }
                if (command is ExitCommand) {
                    exit = true;
                    break;
                }
                if (command is IfOptionCommand) {
                    var ifCommand = (IfOptionCommand) command;
                    if (Options [ifCommand.Option]) {
                        text = Process (text, ifCommand.ThenCommands, ref exit);
                    }
                    else {
                        text = Process (text, ifCommand.ElseCommands, ref exit);
                    }
                }
                else {
                    text = command.Run (text);
                }
            }

            return text;
        }
    }
}

