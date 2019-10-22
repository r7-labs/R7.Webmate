using System;
using NGettext;
using Xwt;
using NLog;

namespace R7.Webmate.Xwt
{
    public static class Program
    {
        static void Initialize ()
        {
            Application.Initialize (OSHelper.GetXwtToolkit ());
            TextCatalogKeeper.SetDefault (new Catalog ("R7.Webmate.Xwt", "./resources/locale"));
            LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration ("./config/R7.Webmate.Xwt.NLog.config");
        }

        [STAThread]
        static void Main ()
        {
            Initialize ();

            var mainWindow = new MainWindow ();
            mainWindow.Show ();

            Application.Run ();
            mainWindow.Dispose ();
        }
    }
}
