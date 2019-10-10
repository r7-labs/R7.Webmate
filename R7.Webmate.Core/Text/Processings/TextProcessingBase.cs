using R7.Webmate.Core.Text.Commands;

namespace R7.Webmate.Core.Text.Processings
{
	public abstract class TextProcessingBase
	{
		public TextProcessingBase ()
		{
			Build ();
		}

		protected ITextProcessingParams Params { get; set; }

		protected ITextCommand Command { get; set; }

		protected abstract void Build ();

		public virtual string Execute (string text, ITextProcessingParams textProcessingParams = null)
		{
			Params = textProcessingParams;

			return Command.Execute (text);
		}
	}
}

