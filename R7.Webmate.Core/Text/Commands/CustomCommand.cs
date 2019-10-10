namespace R7.Webmate.Core.Text.Commands
{
	public delegate string CustomCommandHandler (string value);

	public class CustomCommand: TextCommandBase
    {
		public CustomCommand (CustomCommandHandler customCommandHandler)
		{
			CustomCommandHandler = customCommandHandler;
		}

		public CustomCommandHandler CustomCommandHandler;

		public override string Execute (string value)
		{
            if (IsEnabled && CustomCommandHandler != null) {
                return CustomCommandHandler (value);
            }

			return value;
		}
	}
}

