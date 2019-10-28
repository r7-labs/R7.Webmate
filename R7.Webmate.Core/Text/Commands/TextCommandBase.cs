namespace R7.Webmate.Core.Text.Commands
{
	public abstract class TextCommandBase: ITextCommand
	{
		public abstract string Execute (string value);
	}
}