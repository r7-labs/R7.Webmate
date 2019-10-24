using System;
using NGettext;
using R7.Webmate.Core.Text;
using R7.Webmate.Core.Text.Processings;
using R7.Webmate.Xwt.Icons;
using Xwt;

namespace R7.Webmate.Xwt
{
    // TODO: Extract base class
    public class TextCleanerWidget: Widget
    {
        protected ICatalog T = TextCatalogKeeper.GetDefault ();

        protected Button btnPaste;

        protected Button btnPasteHtml;

        protected TextViewLabel lblSrc = new TextViewLabel ();

        protected Button btnProcess;

        protected CheckBox chkAutoProcess = new CheckBox ();

        protected VBox vboxResults = new VBox ();

        protected TextCleanerModel Model = new TextCleanerModel ();

        // TODO: Move processings to model?

        protected ITextProcessing TextToAsciiProcessing = TextProcessingLoader.Load ("text-to-ascii.yml");

        protected ITextProcessing TextToTextProcessing = TextProcessingLoader.Load ("text-to-text.yml");

        protected ITextProcessing TextToHtmlProcessing = TextProcessingLoader.Load ("text-to-html.yml");

        protected HtmlToHtmlProcessing HtmlToHtmlProcessing;

        public TextCleanerWidget ()
        {
            lblSrc.AllowQuickCopy = false;

            btnPaste = new Button (IconHelper.GetIcon ("paste").WithSize (IconSize.Medium), T.GetString ("Paste"));
            btnPaste.Clicked += BtnPaste_Clicked;

            btnPasteHtml = new Button (IconHelper.GetIcon ("paste").WithSize (IconSize.Medium), T.GetString ("Paste HTML"));
            btnPasteHtml.Clicked += BtnPasteHtml_Clicked;

            var btnPasteMenu = new MenuButton ();

            btnProcess = new Button (IconHelper.GetIcon ("play-circle").WithSize (IconSize.Medium), T.GetString ("Process"));
            btnProcess.Clicked += BtnProcess_Clicked;

            var hboxPaste = new HBox ();
            hboxPaste.PackStart (btnPaste, true, true);
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
            HtmlToHtmlProcessing = new HtmlToHtmlProcessing ();
            HtmlToHtmlProcessing.TextToTextProcessing = TextToTextProcessing;
        }

        void BtnPaste_Clicked (object sender, EventArgs e)
        {
            Model.Source = Clipboard.GetText () ?? string.Empty;
            lblSrc.Text = Model.Source;

            if (chkAutoProcess.Active) {
                BtnProcess_Clicked (sender, e);
            }
        }

        void BtnPasteHtml_Clicked (object sender, EventArgs e)
        {
            Model.Source = HtmlHelper.GetBodyContents (ClipboardHelper.TryGetHtml ());

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

            if (!string.IsNullOrEmpty (Model.Source)) {
                if (HtmlHelper.IsHtml (Model.Source)) {
                    Model.Results.Add (new TextCleanerResult {
                        Text = HtmlToHtmlProcessing.Execute (Model.Source),
                        Label = T.GetString ("HTML"),
                        Format = TextCleanerResultFormat.HTML
                    });
                }
                else {
                    var unicodeText = new TextCleanerResult {
                        Text = TextToTextProcessing.Execute (Model.Source),
                        Label = T.GetString ("Text"),
                        Format = TextCleanerResultFormat.Text
                    };

                    Model.Results.Add (unicodeText);

                    Model.Results.Add (new TextCleanerResult {
                        Text = TextToAsciiProcessing.Execute (unicodeText.Text),
                        Label = T.GetString ("ASCII text"),
                        Format = TextCleanerResultFormat.Text
                    });

                    Model.Results.Add (new TextCleanerResult {
                        Text = TextToHtmlProcessing.Execute (unicodeText.Text),
                        Label = T.GetString ("HTML"),
                        Format = TextCleanerResultFormat.HTML
                    });
                }
            }
        }

        void ShowResults ()
        {
            vboxResults.Clear ();

            var index = 0;
            foreach (var result in Model.Results) {
                AddResult (result.Text, ++index, result.Label, result.Format);
            }
        }

        void AddResult (string result, int index, string label, TextCleanerResultFormat resultFormat)
        {
            var lblResult = new TextViewLabel ();
            lblResult.Text = result;

            var vboxResult = new VBox ();
            vboxResult.MarginLeft = 5;
            vboxResult.MarginRight = 5;
            vboxResult.MarginBottom = 3;
            vboxResult.PackStart (lblResult, false, true);

            var frmResult = new Frame ();
            frmResult.Label = string.Format (T.GetString ("Result #{0} - {1}"), index, label);
            frmResult.Content = vboxResult;
            vboxResults.PackStart (frmResult);
        }
    }
}

