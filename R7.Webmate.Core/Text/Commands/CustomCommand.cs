namespace R7.Webmate.Core.Text.Commands
{
	public delegate string CustomCommandHandler (string text);

	public class CustomCommand: TextCommandBase
    {
		public CustomCommand (CustomCommandHandler customCommandHandler)
		{
			CustomCommandHandler = customCommandHandler;
		}

		public CustomCommandHandler CustomCommandHandler;

		public override string Run (string text)
		{
            if (CustomCommandHandler != null) {
                return CustomCommandHandler (text);
            }

			return text;
		}
	}
}

