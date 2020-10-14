using R7.Webmate.Text.Processings;

namespace R7.Webmate.Text.Models
{
    public class TextCleanerModel: TextCleanerModelBase
    {
        public ITextProcessing TextSimplifyProcessing = TextProcessingLoader.Load ("text-simplify.yml");

        public ITextProcessing TextToTextProcessing = TextProcessingLoader.Load ("text-to-text.yml");

        public ITextProcessing TextToHtmlProcessing = TextProcessingLoader.Load ("text-to-html.yml");

        public ITextProcessing HtmlToTextProcessing = TextProcessingLoader.Load ("html-to-text.yml");

        public HtmlToHtmlProcessing HtmlToHtmlProcessing;

        public TextCleanerModel ()
        {
            HtmlToHtmlProcessing = new HtmlToHtmlProcessing ();
            HtmlToHtmlProcessing.TextToTextProcessing = TextToTextProcessing;
        }

        public override void Process ()
        {
            Results.Clear ();

            if (!string.IsNullOrEmpty (Source)) {
                if (HtmlHelper.IsHtml (Source)) {
                    Results.Add (new TextResult {
                        Text = HtmlToHtmlProcessing.Process (Source),
                        Label = "HTML",
                        TextColor = "darkblue",
                        Format = TextResultFormat.HTML
                    });

                    Results.Add (new TextResult {
                        Text = TextToTextProcessing.Process (HtmlToTextProcessing.Process (Source)),
                        Label = "Text",
                        Format = TextResultFormat.Text
                    });
                }
                else {
                    var textResult = new TextResult {
                        Text = TextToTextProcessing.Process (Source),
                        Label = "Text",
                        Format = TextResultFormat.Text
                    };

                    Results.Add (textResult);

                    Results.Add (new TextResult {
                        Text = TextSimplifyProcessing.Process (textResult.Text),
                        Label = "Simplified text",
                        TextColor = "darkred",
                        Format = TextResultFormat.Text
                    });

                    Results.Add (new TextResult {
                        Text = TextToHtmlProcessing.Process (textResult.Text),
                        Label = "HTML",
                        TextColor = "darkblue",
                        Format = TextResultFormat.HTML
                    });
                }
            }
        }
    }
}
