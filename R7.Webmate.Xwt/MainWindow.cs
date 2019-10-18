using NGettext;
using R7.Webmate.Xwt.Icons;
using Xwt;
using Xwt.Drawing;

namespace R7.Webmate.Xwt
{
    public class MainWindow: Window
    {
        protected ICatalog T = TextCatalogKeeper.GetDefault ();

        public StatusIcon StatusIcon { get; protected set; }

        public MainWindow ()
        {
            Icon = Image.FromFile ("./resources/app-icons/r7-webmate-plain.svg");
            Width = 500;
            Height = 400;

            CloseRequested += MainWindow_CloseRequested;

            var notebook = new Notebook ();
            notebook.TabOrientation = NotebookTabOrientation.Bottom;
            notebook.Add (new TextCleanerWidget (), T.GetString ("Text Cleaner"));
            notebook.Add (new TableCleanerWidget (), T.GetString ("Table Cleaner"));
            notebook.CurrentTabChanged += Notebook_CurrentTabChanged;

            UpdateWindowTitle (notebook.CurrentTab.Label);

            var vbox = new VBox ();
            vbox.PackStart (notebook, true, true);

            Content = vbox;
            Content.Show ();
        }

        void UpdateWindowTitle (string suffix)
        {
            Title = T.GetString ("R7.Webmaster.Xwt - " + suffix);
        }

        public void InitStatusIcon (StatusIcon statusIcon)
        {
            StatusIcon = statusIcon;
            statusIcon.Image = Icon;

            statusIcon.Menu = BuildStatusMenu ();
        }

        protected Menu BuildStatusMenu ()
        {
            var miRestore = new MenuItem {
                Label = T.GetString ("Restore"),
                Image = FAIconHelper.GetIcon ("arrow-up").WithSize (IconSize.Small),
            };
            miRestore.Clicked += MiRestore_Clicked;

            var miClose = new MenuItem {
                Label = T.GetString ("Close"),
                Image = FAIconHelper.GetIcon ("times-circle").WithSize (IconSize.Small)
            };
            miClose.Clicked += MiClose_Clicked;

            var menu = new Menu ();
            menu.Items.Add (miRestore);
            menu.Items.Add (new SeparatorMenuItem ());
            menu.Items.Add (miClose);

            return menu;
        }

        void MainWindow_CloseRequested (object sender, CloseRequestedEventArgs args)
        {
            args.AllowClose = false;
            Hide ();
        }

        void MiRestore_Clicked (object sender, System.EventArgs e)
        {
            Present ();
        }

        void MiClose_Clicked (object sender, System.EventArgs e)
        {
            Application.Exit ();
        }

        void Notebook_CurrentTabChanged (object sender, System.EventArgs e)
        {
            var notebook = (Notebook) sender;
            UpdateWindowTitle (notebook.CurrentTab.Label);
        }
    }
}
