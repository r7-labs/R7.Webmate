using System;
using System.Text;
using NGettext;
using R7.Webmate.Text.Models;
using R7.Webmate.Xwt.Icons;
using R7.Webmate.Xwt.Text;
using Xwt;

namespace R7.Webmate.Xwt
{
    // TODO: Localize me!
    public class CaseChangerWidget: Widget
    {
        protected ICatalog T = TextCatalogKeeper.GetDefault ();

        protected CaseChangerModel Model = new CaseChangerModel ();

        #region Controls

        protected Button btnPaste;

        protected TextViewLabel lblSrc = new TextViewLabel ();

        protected Button btnProcess;

        protected CheckBox chkAutoProcess = new CheckBox ();

        protected VBox vboxResults = new VBox ();

        #endregion

        public CaseChangerWidget ()
        {
            lblSrc.AllowQuickCopy = false;
            lblSrc.AllowEdit = true;

            btnPaste = new Button (IconHelper.GetIcon ("paste").WithSize (IconSize.Medium), T.GetString ("Paste"));
            btnPaste.Clicked += btnPaste_Clicked;

            btnProcess = new Button (IconHelper.GetIcon ("play-circle").WithSize (IconSize.Medium), T.GetString ("Process"));
            btnProcess.Clicked += btnProcess_Clicked;

            chkAutoProcess.Label = T.GetString ("Process on paste?");
            chkAutoProcess.Active = true;

            var scrResults = new ScrollView (vboxResults);
            scrResults.HeightRequest = 200;

            var vbox = new VBox ();
            vbox.PackStart (btnPaste, true, true);
            vbox.PackStart (lblSrc, false, true);
            vbox.PackStart (btnProcess, false, true);
            vbox.PackStart (chkAutoProcess, false, true);
            vbox.PackStart (scrResults, false, true);

            vbox.Margin = Const.VBOX_MARGIN;
            Content = vbox;
            Content.Show ();
        }

        void btnPaste_Clicked (object sender, EventArgs e)
        {
            Model.Source = Clipboard.GetText () ?? string.Empty;
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

        void ShowResults ()
        {
            vboxResults.Clear ();
            var index = 0;
            foreach (var result in Model.Results) {
                AddResult (++index, result);
            }
        }

        protected void AddResult (int index, TextCleanerResult result)
        {
            var lblResult = new TextViewLabel ();
            lblResult.Text = result.Text;

            var vboxResult = new VBox ();
            vboxResult.MarginLeft = Const.VBOX_MARGIN;
            vboxResult.MarginRight = Const.VBOX_MARGIN;
            vboxResult.MarginBottom = 3;
            vboxResult.PackStart (lblResult, false, true);

            var frmResult = new Frame ();

            frmResult.Label = string.Format (T.GetString ("Result #{0} - {1}"), index, result.Label);

            frmResult.Content = vboxResult;
            vboxResults.PackStart (frmResult);
        }
    }
}
