using System;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Xwt;
using NLog;
using R7.Webmate.Core;

namespace R7.Webmate.Xwt
{
    public class Config
    {
        #region Config options

        public ToolkitType? ToolkitType { get; set; }

        #endregion

        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static string DefaultConfigPath { get; set; }

        #region Singleton implementation

        public static Config Instance => new Lazy<Config> (() => LoadConfigOrDefault ()).Value;

        public static Config LoadConfigOrDefault ()
        {
            if (string.IsNullOrEmpty (DefaultConfigPath)) {
                Logger.Warn ("No default config path specified.");
                return new Config ();
            }

            var platformString = PlatformHelper.GetPlatformString ();
            var configPath = FileHelper.GetFirstSuffixedOrDefaultFile (DefaultConfigPath,
                    $".{platformString}.user",
                    $".{platformString}");

            if (configPath == null) {
                Logger.Warn ("Config file not found, fallback to default config.");
                return new Config ();
            }

            try {
                var configText = File.ReadAllText (configPath.FullName);
                var deserializer = new DeserializerBuilder ()
                    .WithNamingConvention (HyphenatedNamingConvention.Instance)
                    .Build ();
                return deserializer.Deserialize<Config> (configText);
            }
            catch (Exception ex) {
                Logger.Warn (ex, "Error loading config file, fallback to default config.");
            }

            return new Config ();
        }

        #endregion
    }
}
