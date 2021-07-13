using System;
using NGettext;
using Xwt;
using System.Diagnostics;

namespace R7.Webmate.Xwt
{
    public class ExternalToolsWidget: Widget
    {
        protected ICatalog T = TextCatalogKeeper.GetDefault ();

        #region Controls

        #endregion

        public ExternalToolsWidget ()
        {
            var btnMergePdf = new Button ("Merge PDF");
            btnMergePdf.Clicked += (sender, e) => {
                Process.Start ("x-www-browser", "https://www.ilovepdf.com/merge_pdf");
            };

            var btnSplitPdf = new Button ("Split PDF");
            btnSplitPdf.Clicked += (sender, e) => {
                Process.Start ("x-www-browser", "https://www.ilovepdf.com/split_pdf");
            };

            var btnCompressPdf = new Button ("Compress PDF");
            btnCompressPdf.Clicked += (sender, e) => {
                Process.Start ("x-www-browser", "https://www.ilovepdf.com/compress_pdf");
            };

            var btnILovePdf = new Button ("More Tools...");
            btnILovePdf.Clicked += (sender, e) => {
                Process.Start ("x-www-browser", "https://www.ilovepdf.com");
            };

            var btnCharmap = new Button ("Character Map");
            btnCharmap.Clicked += (sender, e) => {
                Process.Start ("charmap");
            };

            var table1 = new Table ();
            table1.Margin = Const.VBOX_MARGIN;
            table1.Add (btnMergePdf, 0, 0);
            table1.Add (btnSplitPdf, 1, 0);
            table1.Add (btnCompressPdf, 2, 0);
            table1.Add (btnILovePdf, 3, 0);

            var table2 = new Table ();
            table2.Margin = Const.VBOX_MARGIN;
            table2.Add (btnCharmap, 0, 0);

            var vbox = new VBox ();
            vbox.Margin = Const.VBOX_MARGIN;

            var expander1 = new Expander ();
            expander1.Expanded = true;
            expander1.Label = "I Love PDF";
            expander1.Content = table1;

            var expander2 = new Expander ();
            expander2.Expanded = true;
            expander2.Label = "Character map";
            expander2.Content = table2;

            vbox.PackStart (expander1, false, true);
            vbox.PackStart (expander2, false, true);

            var scrollView = new ScrollView (vbox);

            Content = scrollView;
            Content.Show ();
        }
    }
}
