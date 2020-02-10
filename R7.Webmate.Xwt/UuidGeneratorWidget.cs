using System;
using System.Text;
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

        protected HBox hboxGenerate = new HBox ();

        protected Box boxOptions = new HBox ();

        protected CheckBox chkUppercase;

        protected CheckBox chkNoDashes;

        protected CheckBox chkJoinResults;

        protected SpinButton spnNumberOfEntries = new SpinButton ();

        protected ScrollView scrResults;

        public UuidGeneratorWidget ()
        {
            var vbox = new VBox ();

            spnNumberOfEntries.MinimumValue = 1;
            spnNumberOfEntries.MaximumValue = 100;
            spnNumberOfEntries.IncrementValue = 1;
            spnNumberOfEntries.Digits = 0;
            spnNumberOfEntries.Value = 4;
            spnNumberOfEntries.TooltipText = T.GetString ("Number of UUIDs to generate");

            chkUppercase = new CheckBox (T.GetString ("Uppercase?"));
            chkNoDashes = new CheckBox (T.GetString ("No Dashes?"));
            chkJoinResults = new CheckBox(T.GetString ("Join Results?"));

            boxOptions.PackStart (chkUppercase, false, true);
            boxOptions.PackStart (chkNoDashes, false, true);
            boxOptions.PackStart(chkJoinResults, false, true);

            btnGenerate = new Button (IconHelper.GetIcon ("play-circle").WithSize (IconSize.Medium), T.GetString ("Generate"));
            btnGenerate.Clicked += BtnGenerate_Clicked;

            hboxGenerate.PackStart (btnGenerate, true, true);
            hboxGenerate.PackStart (spnNumberOfEntries, false, true);

            scrResults = new ScrollView (vboxResults);

            vbox.PackStart (hboxGenerate, false, true);
            vbox.PackStart (boxOptions, false, true);
            vbox.PackStart (scrResults, true, true);

            vbox.Margin = Const.VBOX_MARGIN;
            Content = vbox;
            Content.Show ();
        }

        void BtnGenerate_Clicked (object sender, EventArgs e)
        {
            vboxResults.Clear ();

            Model.Uppercase = chkUppercase.Active;
            Model.NoDashes = chkNoDashes.Active;

            if (chkJoinResults.Active) {
                var result = new StringBuilder ();
                for (var i = 0; i < (int) spnNumberOfEntries.Value; i++) {
                    result.AppendLine (Model.GenerateUuid ());
                }

                AddResult (1, (int) spnNumberOfEntries.Value, result.ToString ());
            }
            else {
                for (var i = 0; i < (int) spnNumberOfEntries.Value; i++) {
                    AddResult (i + 1, i + 1, Model.GenerateUuid ());
                }
            }
        }

        protected virtual void AddResult (int fromIndex, int toIndex, string result)
        {
            var lblResult = new TextViewLabel ();
            lblResult.Text = result;

            var vboxResult = new VBox ();
            vboxResult.MarginLeft = Const.VBOX_MARGIN;
            vboxResult.MarginRight = Const.VBOX_MARGIN;
            vboxResult.MarginBottom = 3;
            vboxResult.PackStart (lblResult, false, true);

            var frmResult = new Frame ();

            if (fromIndex == toIndex) {
                frmResult.Label = string.Format (T.GetString ("Result #{0}"), fromIndex);
            }
            else {
                frmResult.Label = string.Format (T.GetString ("Results #{0}-#{1}"), fromIndex, toIndex);
            }

            frmResult.Content = vboxResult;
            vboxResults.PackStart (frmResult);
        }
    }
}
