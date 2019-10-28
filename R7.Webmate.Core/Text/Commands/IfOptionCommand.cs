using System.Collections.Generic;
using System;

namespace R7.Webmate.Core.Text.Commands
{
    public class IfOptionCommand: TextCommandBase
    {
        public string Option { get; set; }

        public IList<ITextCommand> ThenCommands = new List<ITextCommand> ();

        public IList<ITextCommand> ElseCommands = new List<ITextCommand> ();

        public override string Execute (string value)
        {
            throw new NotSupportedException ("Must be executed by external processor.");
        }
    }
}
