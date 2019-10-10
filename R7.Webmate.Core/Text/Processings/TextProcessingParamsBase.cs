namespace R7.Webmate.Core.Text.Processings
{
    public interface ITextProcessingParams
    {
    }

    public abstract class TextProcessingParamsBase: ITextProcessingParams
    {
        public TextProcessingParamsBase Copy ()
        {
            return (TextProcessingParamsBase) MemberwiseClone ();
        }
    }
}
