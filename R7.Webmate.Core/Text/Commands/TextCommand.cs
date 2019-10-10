namespace R7.Webmate.Core.Text.Commands
{
	public delegate bool IsEnabledHandler ();

	public interface ITextCommand
	{
		bool IsEnabled { get; set; }

		string Execute (string value);
	}

	public abstract class TextCommandBase: ITextCommand
	{
		protected TextCommandBase ()
		{
			IsEnabled = true;
		}

		public bool IsEnabled { get; set; }

		public abstract string Execute (string value);

		public IsEnabledHandler IsEnabledHandler; 
	}
}