using System;
using NGettext;
using R7.Webmate.Xwt.Icons;
using R7.Webmate.Xwt.Text;
using Xwt;

namespace R7.Webmate.Xwt
{
    public class UuidGeneratorWidget: Widget
    {
        protected ICatalog T = TextCatalogKeeper.GetDefault ();

        protected UuidGeneratorModel Model = new UuidGeneratorModel ();

        protected Button btnGenerate;

        protected VBox vboxResults = new VBox ();

        protected Table tblOptions = new Table ();

        protected CheckBox chkUppercase;

        protected SpinButton spnNumberOfEntries = new SpinButton ();

        public UuidGeneratorWidget ()
        {
            var vbox = new VBox ();

            spnNumberOfEntries.MinimumValue = 1;
            spnNumberOfEntries.MaximumValue = 1000;
            spnNumberOfEntries.IncrementValue = 1;
            spnNumberOfEntries.Digits = 0;
            spnNumberOfEntries.Value = 4;

            chkUppercase = new CheckBox (T.GetString ("Uppercase?"));

            var lblNumberOfEntries = new Label (T.GetString ("Number of Entries:"));
            lblNumberOfEntries.TextAlignment = Alignment.End;

            tblOptions.Add (lblNumberOfEntries, 0, 0, hexpand: true);
            tblOptions.Add (spnNumberOfEntries, 1, 0, hexpand: true);
            tblOptions.Add (chkUppercase, 0, 1, colspan: 2);

            btnGenerate = new Button (IconHelper.GetIcon ("play-circle").WithSize (IconSize.Medium), T.GetString ("Generate"));
            btnGenerate.Clicked += BtnGenerate_Clicked;

            vbox.PackStart (tblOptions, false, true);
            vbox.PackStart (btnGenerate, false, true);
            vbox.PackStart (vboxResults, false, true);

            vbox.Margin = Const.VBOX_MARGIN;
            Content = vbox;
            Content.Show ();
        }

        void BtnGenerate_Clicked (object sender, EventArgs e)
        {
            vboxResults.Clear ();

            Model.Uppercase = chkUppercase.Active;

            for (var i = 0; i < (int) spnNumberOfEntries.Value; i++) {
                AddResult (i + 1, Model.GenerateUuid ());
            }
        }

        protected virtual void AddResult (int index, string result)
        {
            var lblResult = new TextViewLabel ();
            lblResult.Text = result;

            var vboxResult = new VBox ();
            vboxResult.MarginLeft = Const.VBOX_MARGIN;
            vboxResult.MarginRight = Const.VBOX_MARGIN;
            vboxResult.MarginBottom = 3;
            vboxResult.PackStart (lblResult, false, true);

            var frmResult = new Frame ();
            frmResult.Label = string.Format (T.GetString ("Result #{0}"), index);
            frmResult.Content = vboxResult;
            vboxResults.PackStart (frmResult);
        }
    }
}
