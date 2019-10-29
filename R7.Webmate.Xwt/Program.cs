using System;
using NGettext;
using Xwt;
using NLog;

namespace R7.Webmate.Xwt
{
    public class CmdlineArgs
    {
        public bool Silent { get; protected set; }

        public CmdlineArgs (string [] args)
        {
            foreach (var arg in args) {
                if (arg == "--silent") {
                    Silent = true;
                }
            }
        }
    }

    public static class Program
    {
        static internal CmdlineArgs CmdlineArgs;

        static internal MainWindow MainWindow;

        static void Initialize ()
        {
            Config.DefaultConfigPath = "./config/R7.Webmate.Xwt.yml";
            Application.Initialize (Config.Instance.ToolkitType ?? XwtHelper.GetDefaultXwtToolkitType ());
            TextCatalogKeeper.SetDefault (new Catalog ("R7.Webmate.Xwt", "./resources/locale"));
            LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration ("./config/R7.Webmate.Xwt.NLog.config");
        }

        [STAThread]
        static void Main (string [] args)
        {
            Initialize ();

            CmdlineArgs = new CmdlineArgs (args);

            MainWindow = new MainWindow ();
            if (!CmdlineArgs.Silent || MainWindow.StatusIcon == null) {
                MainWindow.Show ();
            }

            Application.Run ();
            MainWindow.Dispose ();
        }
    }
}
