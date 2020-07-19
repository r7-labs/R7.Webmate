using System.Collections.Generic;
using System;

namespace R7.Webmate.Text.Commands
{
    public class IfOptionCommand: TextCommandBase
    {
        public string Option { get; set; }

        public IList<ITextCommand> ThenCommands = new List<ITextCommand> ();

        public IList<ITextCommand> ElseCommands = new List<ITextCommand> ();

        public override string Run (string text)
        {
            throw new NotSupportedException ("Must be executed by external processor.");
        }
    }
}
