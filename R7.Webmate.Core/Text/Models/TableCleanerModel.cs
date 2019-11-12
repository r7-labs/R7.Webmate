using R7.Webmate.Core.Text.Processings;

namespace R7.Webmate.Core.Text.Models
{
    public class TableCleanerModel: TextCleanerModelBase
    {
        public bool BootstrapTable { get; set; }

        public bool BootstrapResponsiveTable { get; set; }

        public TableCleanProcessing TP { get; set; }

        public TableCleanerModel ()
        {
            var htmlToHtmlProcessing = new HtmlToHtmlProcessing ();
            htmlToHtmlProcessing.TextToTextProcessing = TextProcessingLoader.Load ("text-to-text.yml");

            TP = new TableCleanProcessing ();
            TP.HtmlToHtmlProcessing = htmlToHtmlProcessing;
            TP.TableCleanTextProcessing = TextProcessingLoader.Load ("table-clean.yml");
        }

        public override void Process ()
        {
            Results.Clear ();

            if (!string.IsNullOrEmpty (Source)) {
                if (HtmlHelper.IsHtml (Source)) {
                    var resultText = TP.Process (Source);

                    Results.Add (new TextCleanerResult {
                        Text = resultText,
                        Label = "HTML table",
                        TextColor = "darkblue",
                        Format = TextCleanerResultFormat.HTML
                    });

                    if (!string.IsNullOrEmpty (resultText)) {
                        if (BootstrapTable) {
                            // TODO: Don't hardcode this?
                            resultText = resultText.Replace ("<table>",
                                "<table class=\"table table-bordered table-striped table-hover\">");
                            if (BootstrapResponsiveTable) {
                                resultText = $"<div class=\"table-responsive\">{resultText}</div>";
                            }
                            Results.Add (new TextCleanerResult {
                                Text = resultText,
                                Label = "Bootstrap table",
                                TextColor = "darkblue",
                                Format = TextCleanerResultFormat.HTML
                            });
                        }
                    }
                }
            }
        }
    }
}
