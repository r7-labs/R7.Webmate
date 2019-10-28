using System;
using NGettext;
using R7.Webmate.Core.Text;
using R7.Webmate.Xwt.Icons;
using Xwt;

namespace R7.Webmate.Xwt.Text
{
    // TODO: Extract base class
    public class TableCleanerWidget: Widget
    {
        protected ICatalog T = TextCatalogKeeper.GetDefault ();

        protected Button btnPaste;

        protected Button btnPasteHtml;

        protected TextViewLabel lblSrc = new TextViewLabel ();

        protected Button btnProcess;

        protected CheckBox chkAutoProcess = new CheckBox ();

        protected CheckBox chkBootstrapTable = new CheckBox ();

        protected CheckBox chkBootstrapResponsiveTable = new CheckBox ();

        protected VBox vboxResults = new VBox ();

        protected TableCleanerModel Model = new TableCleanerModel ();

        public TableCleanerWidget ()
        {
            lblSrc.AllowQuickCopy = false;

            btnPaste = new Button (IconHelper.GetIcon ("paste").WithSize (IconSize.Medium), T.GetString ("Paste"));
            btnPaste.Clicked += BtnPaste_Clicked;

            btnPasteHtml = new Button (IconHelper.GetIcon ("paste").WithSize (IconSize.Medium), T.GetString ("Paste HTML"));
            btnPasteHtml.Clicked += BtnPasteHtml_Clicked;

            btnProcess = new Button (IconHelper.GetIcon ("play-circle").WithSize (IconSize.Medium), T.GetString ("Process"));
            btnProcess.Clicked += BtnProcess_Clicked;

            var hboxPaste = new HBox ();
            hboxPaste.PackStart (btnPaste, true, true);
            hboxPaste.PackStart (btnPasteHtml, true, true);

            chkAutoProcess.Label = T.GetString ("Process on paste?");
            chkAutoProcess.Active = true;

            chkBootstrapTable.Label = T.GetString ("Generate Bootstrap table?");
            chkBootstrapTable.Active = true;
            chkBootstrapTable.Clicked += (sender, e) => {
                chkBootstrapResponsiveTable.Visible = ((CheckBox) sender).Active;
            };

            chkBootstrapResponsiveTable.Label = T.GetString ("Generate Bootstrap responsive table?");
            chkBootstrapResponsiveTable.Active = true;

            var vbox = new VBox ();
            vbox.PackStart (hboxPaste, true, true);
            vbox.PackStart (lblSrc, false, true);
            vbox.PackStart (btnProcess, false, true);
            vbox.PackStart (chkAutoProcess, false, true);
            vbox.PackStart (chkBootstrapTable, false, true);
            vbox.PackStart (chkBootstrapResponsiveTable, false, true);
            vbox.PackStart (vboxResults, false, true);

            vbox.Margin = 5;
            Content = vbox;
            Content.Show ();
        }

        void BtnPaste_Clicked (object sender, EventArgs e)
        {
            Model.Source = HtmlHelper.GetFirstTable (Clipboard.GetText () ?? string.Empty);
            lblSrc.Text = Model.Source;

            if (chkAutoProcess.Active) {
                Process ();
                ShowResults ();
            }
        }

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
            Model.BootstrapTable = chkBootstrapTable.Active;
            Model.BootstrapResponsiveTable = chkBootstrapResponsiveTable.Active;
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

