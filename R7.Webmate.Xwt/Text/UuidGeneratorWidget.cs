using System;
using NGettext;
using R7.Webmate.Text.Models;
using R7.Webmate.Xwt.Icons;
using Xwt;

namespace R7.Webmate.Xwt.Text
{
    public class UuidGeneratorWidget: Widget
    {
        protected ICatalog T = TextCatalogKeeper.GetDefault ();

        protected UuidGeneratorModel Model = new UuidGeneratorModel ();

        protected Button btnGenerate;

        protected VBox vboxResults = new VBox ();

        protected HBox hboxGenerate = new HBox ();

        protected SpinButton spnNumberOfEntries = new SpinButton ();

        protected ScrollView scrResults;

        public UuidGeneratorWidget ()
        {
            var vbox = new VBox ();

            spnNumberOfEntries.MinimumValue = 1;
            spnNumberOfEntries.MaximumValue = 100;
            spnNumberOfEntries.IncrementValue = 1;
            spnNumberOfEntries.Digits = 0;
            spnNumberOfEntries.Value = 1;
            spnNumberOfEntries.TooltipText = T.GetString ("Number of UUIDs to generate");

            btnGenerate = new Button (IconHelper.GetIcon ("play-circle").WithSize (IconSize.Medium), T.GetString ("Generate"));
            btnGenerate.Clicked += btnGenerate_Clicked;

            hboxGenerate.PackStart (btnGenerate, true, true);
            hboxGenerate.PackStart (spnNumberOfEntries, false, true);

            scrResults = new ScrollView (vboxResults);

            vbox.PackStart (hboxGenerate, false, true);
            vbox.PackStart (scrResults, true, true);

            vbox.Margin = Const.VBOX_MARGIN;
            Content = vbox;
            Content.Show ();
        }

        void btnGenerate_Clicked (object sender, EventArgs e)
        {
            vboxResults.Clear ();

            Model.NumOfEntries = (int) spnNumberOfEntries.Value;
            Model.Process ();

            var i = 1;
            foreach (var result in Model.Results) {
                AddResult (i, result);
                i++;
            }
        }

        protected void AddResult (int index, TextResult result)
        {
            var lblResult = new TextViewLabel ();
            lblResult.Text = result.Text;

            var vboxResult = new VBox ();
            vboxResult.MarginLeft = Const.VBOX_MARGIN;
            vboxResult.MarginRight = Const.VBOX_MARGIN;
            vboxResult.MarginBottom = 3;
            vboxResult.PackStart (lblResult, false, true);

            var frmResult = new Frame ();

            frmResult.Label = string.Format (T.GetString ("Result #{0} - {1}"), index, T.GetString (result.Label));

            frmResult.Content = vboxResult;
            vboxResults.PackStart (frmResult);
        }

    }
}
