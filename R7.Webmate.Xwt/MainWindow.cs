using NGettext;
using Xwt;
using R7.Webmate.Xwt.Icons;

namespace R7.Webmate.Xwt
{
    public class MainWindow: Window
    {
        protected ICatalog T = TextCatalogKeeper.GetDefault ();

        public MainWindow ()
        {
            Title = T.GetString ("R7.Webmate.Xwt - ") + OSHelper.GetXwtToolkit ().ToString ();
            Width = 500;
            Height = 400;

            var notebook = new Notebook ();
            notebook.TabOrientation = NotebookTabOrientation.Bottom;
            notebook.Add (new TextCleanerWidget (), T.GetString ("Text Cleaner"));

            var vbox = new VBox ();
            vbox.PackStart (notebook, true, true);

            Content = vbox;
            Content.Show ();
        }
    }
}
