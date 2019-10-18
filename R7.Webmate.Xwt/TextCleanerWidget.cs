using System;
using System.Collections.Generic;
using System.Text;
using NGettext;
using R7.Webmate.Core.Text.Processings;
using R7.Webmate.Xwt.Icons;
using Xwt;

namespace R7.Webmate.Xwt
{
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

        protected ITextProcessing TextToTextProcessing = TextProcessingLoader.LoadDefaultFromFile ("text-to-text.yml");
        protected ITextProcessing TextToHtmlProcessing = TextProcessingLoader.LoadDefaultFromFile ("text-to-html.yml");

        public TextCleanerWidget ()
        {
            btnPaste = new Button (FAIconHelper.GetIcon ("paste").WithSize (IconSize.Medium), T.GetString ("Paste"));
            btnPasteHtml = new Button (FAIconHelper.GetIcon ("paste").WithSize (IconSize.Medium), T.GetString ("Paste HTML"));
            btnPaste.Clicked += BtnPaste_Clicked;
            btnPasteHtml.Clicked += BtnPasteHtml_Clicked;

            var btnPasteMenu = new MenuButton ();

            btnProcess = new Button (FAIconHelper.GetIcon ("play-circle").WithSize (IconSize.Medium), T.GetString ("Process"));
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
            vbox.PackStart (vboxResults, true, true);

            vbox.Margin = 5;
            Content = vbox;
            Content.Show ();
        }

        void BtnPaste_Clicked (object sender, EventArgs e)
        {
            Model.Source = Clipboard.GetText ();
            lblSrc.Text = Model.Source;

            // autoprocess
            if (chkAutoProcess.Active) {
                BtnProcess_Clicked (sender, e);
            }
        }

        void BtnPasteHtml_Clicked (object sender, EventArgs e)
        {
            if (Clipboard.ContainsData (TransferDataType.Html)) {
                Model.Source = Encoding.UTF8.GetString ((byte []) Clipboard.GetData (TransferDataType.Html));
            }
            else {
                Model.Source = string.Empty;
            }

            lblSrc.Text = Model.Source;

            /* Disabled then pasting HTML for now
            // autoprocess
            if (chkAutoProcess.Active) {
                BtnProcess_Clicked (sender, e);
            }*/
        }

        void BtnProcess_Clicked (object sender, EventArgs e)
        {
            // clear previos results
            vboxResults.Clear ();
            Model.Results.Clear ();

            // process text and add results
            var result1 = TextToTextProcessing.Execute (Model.Source);
            Model.Results.Add (result1);
            AddResult (1, T.GetString ("Text"), result1);

            var result2 = TextToHtmlProcessing.Execute (result1);
            Model.Results.Add (result2);
            AddResult (2, T.GetString ("HTML"), result2);
        }

        void AddResult (int index, string format, string result)
        {
            var lblResult = new TextViewLabel ();
            lblResult.Text = result;
           
            var btnCopy = new Button (FAIconHelper.GetIcon ("copy").WithSize (IconSize.Small), T.GetString ("Copy"));
            btnCopy.Clicked += (sender1, e1) => {
                Clipboard.SetText (result);
            };

            var vboxResult = new VBox ();
            vboxResult.Margin = 5;
            vboxResult.PackStart (lblResult, false, true);
            vboxResult.PackStart (btnCopy, true, true);

            var frmResult = new Frame ();
            frmResult.Label = "#" + index + " " + format;
            frmResult.Content = vboxResult;
            vboxResults.PackStart (frmResult);
        }
    }
}

