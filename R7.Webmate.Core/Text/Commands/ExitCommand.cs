using System;

namespace R7.Webmate.Core.Text.Commands
{
    public class ExitCommand: TextCommandBase
    {
        public override string Run (string text)
        {
            throw new NotSupportedException ("Must be executed by external processor.");
        }
    }
}
