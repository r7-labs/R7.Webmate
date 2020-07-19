using System;
using R7.Webmate.Text;
using R7.Webmate.Text.Models;
using R7.Webmate.Xwt.Icons;
using Xwt;

namespace R7.Webmate.Xwt.Text
{
    public class TableCleanerWidget: TextCleanerWidgetBase
    {
        protected Button btnPaste;

        protected Button btnPasteHtml;

        protected TextViewLabel lblSrc = new TextViewLabel ();

        protected Button btnProcess;

        protected CheckBox chkAutoProcess = new CheckBox ();

        protected CheckBox chkBootstrapTable = new CheckBox ();

        protected CheckBox chkBootstrapResponsiveTable = new CheckBox ();

        public TableCleanerWidget ()
        {
            Model = new TableCleanerModel ();

            lblSrc.AllowQuickCopy = false;
            lblSrc.AllowEdit = true;

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
            // TODO: Implement model update via events
            if (Model.Source != lblSrc.Text) {
                Model.Source = lblSrc.Text;
            }

            (Model as TableCleanerModel).BootstrapTable = chkBootstrapTable.Active;
            (Model as TableCleanerModel).BootstrapResponsiveTable = chkBootstrapResponsiveTable.Active;
            Model.Process ();
        }
    }
}

