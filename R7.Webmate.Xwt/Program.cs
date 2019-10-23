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
            Config.DefaultConfigPath = "./config/R7.Webmate.Xwt.yml";
            Application.Initialize (Config.Instance.ToolkitType ?? XwtHelper.GetDefaultXwtToolkitType ());
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
