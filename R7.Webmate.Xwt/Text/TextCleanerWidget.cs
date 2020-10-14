using System;
using System.Collections.Generic;
using R7.Webmate.Text;
using R7.Webmate.Text.Models;
using R7.Webmate.Xwt.Icons;
using Xwt;

namespace R7.Webmate.Xwt.Text
{
    public class TextCleanerWidget: TextCleanerWidgetBase
    {
        protected Button btnPaste;

        protected Button btnPasteHtml;

        protected TextViewLabel lblSrc = new TextViewLabel ();

        protected Button btnProcess;

        protected Button btnProcessOptions;

        protected Dialog dlgProcessOptions;

        protected CheckBox chkAutoProcess = new CheckBox ();

        public TextCleanerWidget ()
        {
            Model = new TextCleanerModel ();

            lblSrc.AllowQuickCopy = false;
            lblSrc.AllowEdit = true;

            btnPaste = new Button (IconHelper.GetIcon ("paste").WithSize (IconSize.Medium), T.GetString ("Paste"));
            btnPaste.Clicked += BtnPaste_Clicked;

            btnPasteHtml = new Button (IconHelper.GetIcon ("paste").WithSize (IconSize.Medium), T.GetString ("Paste HTML"));
            btnPasteHtml.Clicked += BtnPasteHtml_Clicked;

            var btnPasteMenu = new MenuButton ();

            btnProcess = new Button (IconHelper.GetIcon ("play-circle").WithSize (IconSize.Medium), T.GetString ("Process"));
            btnProcess.Clicked += btnProcess_Clicked;

            dlgProcessOptions = new Dialog ();
            dlgProcessOptions.Title = T.GetString ("Text processing options");
            dlgProcessOptions.Width = 400;
            dlgProcessOptions.Height = 300;
            dlgProcessOptions.Content = new ProcessingOptionsWidget (
                new List<LabeledTextProcessing> {
                    new LabeledTextProcessing {
                        Label = "text-to-text",
                        Processing = (Model as TextCleanerModel).TextToTextProcessing
                    },
                    new LabeledTextProcessing {
                        Label = "text-simplify",
                        Processing = (Model as TextCleanerModel).TextSimplifyProcessing
                    },
                    new LabeledTextProcessing {
                        Label = "text-to-html",
                        Processing = (Model as TextCleanerModel).TextToHtmlProcessing
                    },
                    new LabeledTextProcessing {
                        Label = "html-to-text",
                        Processing = (Model as TextCleanerModel).HtmlToTextProcessing
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
                btnProcess_Clicked (sender, e);
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

        void btnProcess_Clicked (object sender, EventArgs e)
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
    }
}

