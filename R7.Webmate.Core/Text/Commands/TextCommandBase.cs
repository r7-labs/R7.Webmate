namespace R7.Webmate.Core.Text.Commands
{
	public abstract class TextCommandBase: ITextCommand
	{
		public bool IsDisabled { get; set; }

		public abstract string Execute (string value);
	}
}