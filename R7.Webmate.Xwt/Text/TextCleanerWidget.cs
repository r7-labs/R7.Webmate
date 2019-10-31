using System;
using System.Collections.Generic;
using NGettext;
using R7.Webmate.Core.Text;
using R7.Webmate.Xwt.Icons;
using Xwt;

namespace R7.Webmate.Xwt.Text
{
    // TODO: Extract base class
    public class TextCleanerWidget: Widget
    {
        protected ICatalog T = TextCatalogKeeper.GetDefault ();

        protected Button btnPaste;

        protected Button btnPasteHtml;

        protected TextViewLabel lblSrc = new TextViewLabel ();

        protected Button btnProcess;

        protected Button btnProcessOptions;

        protected Dialog dlgProcessOptions;

        protected CheckBox chkAutoProcess = new CheckBox ();

        protected VBox vboxResults = new VBox ();

        protected TextCleanerModel Model = new TextCleanerModel ();

        public TextCleanerWidget ()
        {
            lblSrc.AllowQuickCopy = false;
            lblSrc.AllowEdit = true;

            btnPaste = new Button (IconHelper.GetIcon ("paste").WithSize (IconSize.Medium), T.GetString ("Paste"));
            btnPaste.Clicked += BtnPaste_Clicked;

            btnPasteHtml = new Button (IconHelper.GetIcon ("paste").WithSize (IconSize.Medium), T.GetString ("Paste HTML"));
            btnPasteHtml.Clicked += BtnPasteHtml_Clicked;

            var btnPasteMenu = new MenuButton ();

            btnProcess = new Button (IconHelper.GetIcon ("play-circle").WithSize (IconSize.Medium), T.GetString ("Process"));
            btnProcess.Clicked += BtnProcess_Clicked;

            dlgProcessOptions = new Dialog ();
            dlgProcessOptions.Title = T.GetString ("Text processing options");
            dlgProcessOptions.Width = 400;
            dlgProcessOptions.Height = 300;
            dlgProcessOptions.Content = new ProcessingOptionsWidget (
                new List<LabeledTextProcessing> {
                    new LabeledTextProcessing {
                        Label = "text-to-text",
                        Processing = Model.TextToTextProcessing
                    },
                    new LabeledTextProcessing {
                        Label = "text-to-ascii",
                        Processing = Model.TextToAsciiProcessing
                    },
                    new LabeledTextProcessing {
                        Label = "text-to-html",
                        Processing = Model.TextToHtmlProcessing
                    }
                }
            );

            btnProcessOptions = new Button (IconHelper.GetIcon ("cog").WithBoxSize (IconSize.Small));
            btnProcessOptions.TooltipText = T.GetString ("Click to open text processing options.");
            btnProcessOptions.Clicked += (sender, e) => {
                dlgProcessOptions.Run (ParentWindow);
            };

            var hboxPaste = new HBox ();
            hboxPaste.PackStart (btnPaste, true, true);
            hboxPaste.PackStart (btnPasteHtml, true, true);

            chkAutoProcess.Label = T.GetString ("Process on paste?");
            chkAutoProcess.Active = true;

            var hboxProcess = new HBox ();
            hboxProcess.PackStart (btnProcess, true, true);
            hboxProcess.PackStart (btnProcessOptions, false, true);

            var vbox = new VBox ();
            vbox.PackStart (hboxPaste, true, true);
            vbox.PackStart (lblSrc, false, true);
            vbox.PackStart (hboxProcess, false, true);
            vbox.PackStart (chkAutoProcess, false, true);
            vbox.PackStart (vboxResults, false, true);

            vbox.Margin = 5;
            Content = vbox;
            Content.Show ();
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
            // TODO: Implement model update via events
            if (Model.Source != lblSrc.Text) {
                Model.Source = lblSrc.Text;
            }

            Model.Process ();
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

