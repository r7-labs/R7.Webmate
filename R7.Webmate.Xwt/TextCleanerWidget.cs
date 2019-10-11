using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NGettext;
using R7.Webmate.Core.Text.Processings;
using R7.Webmate.Xwt.Icons;
using Xwt;

namespace R7.Webmate.Xwt
{
    public class TextCleanerModel
    {
        public string Source { get; set; }

        public IList<string> Results = new List<string> ();
    }

    public class TextCleanerWidget: Widget
    {
        protected ICatalog T = TextCatalogKeeper.GetDefault ();

        protected Button btnPaste;

        protected Label lblSrc = new Label ();

        protected Button btnProcess;

        protected VBox vboxResults = new VBox ();

        protected TextCleanerModel Model = new TextCleanerModel ();

        protected ITextProcessing TextToTextProcessing = TextProcessingLoader.LoadDefaultFromFile ("text-to-text.yml");

        public TextCleanerWidget ()
        {
            btnPaste = new Button (FAIconHelper.GetIcon ("paste").WithSize (IconSize.Medium), T.GetString ("Paste"));
            btnPaste.Clicked += BtnPaste_Clicked;

            var btnPasteMenu = new MenuButton ();

            btnProcess = new Button (FAIconHelper.GetIcon ("play-circle").WithSize (IconSize.Medium), T.GetString ("Process"));
            btnProcess.Clicked += BtnProcess_Clicked;

            var hboxPaste = new HBox ();
            hboxPaste.PackStart (btnPaste, true, true);
            hboxPaste.PackStart (btnPasteMenu, false, true);

            lblSrc.Ellipsize = EllipsizeMode.End;
            lblSrc.Selectable = true;

            var vbox = new VBox ();
            vbox.PackStart (hboxPaste, true, true);
            vbox.PackStart (lblSrc, false, true);
            vbox.PackStart (btnProcess, true, true);
            vbox.PackStart (vboxResults, true, true);

            vbox.Margin = 5;
            Content = vbox;
            Content.Show ();
        }

        void BtnPaste_Clicked (object sender, EventArgs e)
        {
            Model.Source = Clipboard.GetText ();
            lblSrc.Text = FormatLabel (Model.Source);
        }

        void BtnProcess_Clicked (object sender, EventArgs e)
        {
            // clear previos results
            vboxResults.Clear ();
            Model.Results.Clear ();

            // process text
            Model.Results.Add (TextToTextProcessing.Execute (Model.Source));

            // display new results
            var index = 0;
            foreach (var result in Model.Results) {
                AddResult (++index, T.GetString ("Text"), result);
            }
        }

        void AddResult (int index, string format, string result)
        {
            var lblResult = new Label ();
            lblResult.Ellipsize = EllipsizeMode.End;
            lblResult.Selectable = true;
            lblResult.Text = FormatLabel (result);

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

        string FormatLabel (string text)
        {
            return Regex.Replace (text.Replace ("\r\n", " ").Replace ("\n", " "), @"\s+", " ").TrimStart ();
        }
    }
}
