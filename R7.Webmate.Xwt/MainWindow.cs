using System;
using NGettext;
using NLog;
using R7.Webmate.Xwt.Icons;
using R7.Webmate.Xwt.Text;
using Xwt;

namespace R7.Webmate.Xwt
{
    public class MainWindow: Window
    {
        protected ICatalog T = TextCatalogKeeper.GetDefault ();

        protected readonly Logger Logger = LogManager.GetCurrentClassLogger ();

        public StatusIcon StatusIcon { get; set; }

        public MainWindow ()
        {
            Icon = IconHelper.GetAppIcon ();

            if (Program.CmdlineArgs.TrayIcon) {
                InitStatusIcon ();
            }

            Width = 500;
            Height = 400;

            CloseRequested += MainWindow_CloseRequested;

            var notebook = new Notebook ();
            notebook.TabOrientation = NotebookTabOrientation.Bottom;
            notebook.Add (new TextCleanerWidget (), T.GetString ("Text Cleaner"));
            notebook.Add (new TableCleanerWidget (), T.GetString ("Table Cleaner"));
            notebook.Add (new UuidGeneratorWidget (), T.GetString ("UUID Generator"));
            notebook.CurrentTabChanged += Notebook_CurrentTabChanged;

            UpdateTitle (notebook.CurrentTab.Label);

            var vbox = new VBox ();
            vbox.PackStart (notebook, true, true);

            Content = vbox;
            Content.Show ();
        }

        void UpdateTitle (string suffix)
        {
            Title = T.GetString ("R7.Webmaster.Xwt - ") + suffix;
        }

        public void InitStatusIcon ()
        {
            try {
                StatusIcon = Application.CreateStatusIcon ();
                StatusIcon.Image = Icon;
                StatusIcon.Menu = BuildStatusMenu ();
            }
            catch (Exception ex) {
                Logger.Warn (ex, "Cannot create status icon - probably not supported by current XWT backend.");
            }
        }

        protected Menu BuildStatusMenu ()
        {
            var miRestore = new MenuItem {
                Label = T.GetString ("Open R7.Webmate"),
                Image = IconHelper.GetIcon ("arrow-up").WithSize (IconSize.Small),
            };
            miRestore.Clicked += MiRestore_Clicked;

            var miClose = new MenuItem {
                Label = T.GetString ("Close"),
                Image = IconHelper.GetIcon ("times-circle").WithSize (IconSize.Small)
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
            if (StatusIcon == null) {
                args.AllowClose = true;
            }
            else {
                args.AllowClose = false;
                Hide ();
            }
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
            UpdateTitle (notebook.CurrentTab.Label);
        }
    }
}
