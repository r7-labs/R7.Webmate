using System.Collections.Generic;
using NGettext;
using R7.Webmate.Text.Processings;
using Xwt;

namespace R7.Webmate.Xwt.Text
{
    // TODO: Better way to name processings
    public struct LabeledTextProcessing
    {
        public string Label;

        public ITextProcessing Processing;
    }

    public class ProcessingOptionsWidget: Widget
    {
        protected ICatalog T = TextCatalogKeeper.GetDefault ();

        #region Widgets

        protected ListView lstProcessings = new ListView ();

        protected ComboBox cbxProcessings = new ComboBox ();

        protected Frame frmOptions = new Frame ();

        protected VBox vboxOptions = new VBox ();

        #endregion

        public IList<LabeledTextProcessing> Processings { get; set; }

        public ProcessingOptionsWidget (IList<LabeledTextProcessing> processings)
        {
            Processings = processings;

            vboxOptions.Margin = 5;
            frmOptions.Content = vboxOptions;
            frmOptions.Label = T.GetString ("Options:");

            var vbox = new VBox ();
            vbox.PackStart (new Label (T.GetString ("Text processing:")), false, true);
            vbox.PackStart (cbxProcessings, false, true);
            vbox.PackStart (frmOptions, true, true);

            cbxProcessings.Items.Add (T.GetString ("<not selected>"));
            cbxProcessings.SelectedIndex = 0;

            foreach (var processing in Processings) {
                cbxProcessings.Items.Add (processing.Label);
            }

            cbxProcessings.SelectionChanged += (sender, e) => {
                var selectedIndex = ((ComboBox) sender).SelectedIndex;
                if (selectedIndex >= 1) {
                    UpdateOptionsView (Processings [selectedIndex - 1].Processing);
                }
                else {
                    vboxOptions.Clear ();
                }
            };

            Content = vbox;
            Content.Show ();
        }

        protected void UpdateOptionsView (ITextProcessing processing)
        {
            vboxOptions.Clear ();
            foreach (var option in processing.Options) {
                var chkOption = new CheckBox (option.Key);
                chkOption.Active = processing.Options [option.Key];
                chkOption.Clicked += (sender, e) => {
                    processing.Options [option.Key] = ((CheckBox) sender).Active;
                };

                vboxOptions.PackStart (chkOption, false, true);
            }
        }
    }
}
