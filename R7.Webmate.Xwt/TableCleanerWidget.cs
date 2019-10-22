using System;
using NGettext;
using R7.Webmate.Core.Text;
using R7.Webmate.Core.Text.Processings;
using R7.Webmate.Xwt.Icons;
using Xwt;

namespace R7.Webmate.Xwt
{
    // TODO: Extract base class
    public class TableCleanerWidget: Widget
    {
        protected ICatalog T = TextCatalogKeeper.GetDefault ();

        protected Button btnPasteHtml;

        protected TextViewLabel lblSrc = new TextViewLabel (false);

        protected Button btnProcess;

        protected CheckBox chkAutoProcess = new CheckBox ();

        protected VBox vboxResults = new VBox ();

        protected TextCleanerModel Model = new TextCleanerModel ();

        // TODO: Move processings to model?
        protected TableCleanProcessing TableCleanProcessing;

        public TableCleanerWidget ()
        {
            btnPasteHtml = new Button (IconHelper.GetIcon ("paste").WithSize (IconSize.Medium), T.GetString ("Paste HTML"));
            btnPasteHtml.Clicked += BtnPasteHtml_Clicked;

            btnProcess = new Button (IconHelper.GetIcon ("play-circle").WithSize (IconSize.Medium), T.GetString ("Process"));
            btnProcess.Clicked += BtnProcess_Clicked;

            var hboxPaste = new HBox ();
            hboxPaste.PackStart (btnPasteHtml, true, true);

            chkAutoProcess.Label = T.GetString ("Process on paste?");
            chkAutoProcess.Active = true;

            var vbox = new VBox ();
            vbox.PackStart (hboxPaste, true, true);
            vbox.PackStart (lblSrc, false, true);
            vbox.PackStart (btnProcess, false, true);
            vbox.PackStart (chkAutoProcess, false, true);
            vbox.PackStart (vboxResults, false, true);

            vbox.Margin = 5;
            Content = vbox;
            Content.Show ();

            InitProcessings ();
        }

        protected void InitProcessings ()
        {
            var htmlToHtmlProcessing = new HtmlToHtmlProcessing ();
            htmlToHtmlProcessing.TextToTextProcessing = TextProcessingLoader.LoadDefaultFromFile ("text-to-text.yml");

            TableCleanProcessing = new TableCleanProcessing ();
            TableCleanProcessing.HtmlToHtmlProcessing = htmlToHtmlProcessing;
            TableCleanProcessing.TableCleanTextProcessing = TextProcessingLoader.LoadDefaultFromFile ("table-clean.yml");
        }

        // TODO: Allow to insert plain text containing HTML markup
        void BtnPasteHtml_Clicked (object sender, EventArgs e)
        {
            Model.Source = HtmlHelper.GetFirstTable (ClipboardHelper.TryGetHtml ());

            lblSrc.Text = Model.Source;

            if (chkAutoProcess.Active) {
                Process ();
                ShowResults ();
            }
        }

        void BtnProcess_Clicked (object sender, EventArgs e)
        {
            Process ();
            ShowResults ();
        }

        void Process ()
        {
            Model.Results.Clear ();

            if (HtmlHelper.IsHtml (Model.Source)) {
                Model.Results.Add (new TextCleanerResult {
                    Text = TableCleanProcessing.Execute (Model.Source),
                    ResultType = TextCleanerResultType.HTML
                });
            }
        }

        void ShowResults ()
        {
            vboxResults.Clear ();

            var index = 0;
            foreach (var result in Model.Results) {
                AddResult (++index, TextCleanerResultTypeHelper.GetString (result.ResultType), result.Text);
            }
        }

        void AddResult (int index, string format, string result)
        {
            var lblResult = new TextViewLabel (true);
            lblResult.Text = result;
                      
            var vboxResult = new VBox ();
            vboxResult.MarginLeft = 5;
            vboxResult.MarginRight = 5;
            vboxResult.MarginBottom = 3;
            vboxResult.PackStart (lblResult, false, true);

            var frmResult = new Frame ();
            frmResult.Label = string.Format (T.GetString ("Result #{0} - {1}"), index, format);
            frmResult.Content = vboxResult;
            vboxResults.PackStart (frmResult);
        }
    }
}

