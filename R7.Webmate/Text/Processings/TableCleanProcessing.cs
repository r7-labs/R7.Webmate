namespace R7.Webmate.Text.Processings
{
    public class TableCleanProcessing: TextProcessingBase
    {
        public ITextProcessing TableCleanTextProcessing { get; set; }

        public HtmlToHtmlProcessing HtmlToHtmlProcessing { get; set; }

        public override string Process (string text)
        {
            return TableCleanTextProcessing.Process (HtmlToHtmlProcessing.Process (text));

            /*
            if (tableCleanerParams.SetCssClass)
                text = text.Replace ("<table", string.Format (
                    "<table class=\"{0}\"", tableCleanerParams.TableCssClass));

            if (tableCleanerParams.SetWidth)
                text = text.Replace ("<table", string.Format (
                    "<table width=\"{0}{1}\"", tableCleanerParams.TableWidth, tableCleanerParams.TableWidthUnits));
            */
        }
    }
}
