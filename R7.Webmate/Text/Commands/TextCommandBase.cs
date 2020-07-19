namespace R7.Webmate.Text.Commands
{
	public abstract class TextCommandBase: ITextCommand
	{
		public abstract string Run (string text);
	}
}
