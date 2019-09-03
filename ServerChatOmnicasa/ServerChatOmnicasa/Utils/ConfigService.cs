using System;
using System.IO;
using Newtonsoft.Json;
using ServerChatOmnicasa.Entities;

namespace ServerChatOmnicasa.Utils
{
    public class ConfigService
    {
        #region Fields

        /// <summary>
        /// Get current directory of service
        /// </summary>
        public static string CurrentServiceDirectory => Environment.CurrentDirectory;

        /// <summary>
        /// Config Log File
        /// </summary>
        public static string LogPath => Path.Combine(CurrentServiceDirectory, "Logs");

        /// <summary>
        /// Get config file path
        /// </summary>
        public static string ConfigFilePath = Path.Combine(CurrentServiceDirectory, "Config.json");

        /// <summary>
        /// Get config object value
        /// </summary>
        private static Config _config;
        public static Config Config = _config ?? (_config = GetConfig());

        /// <summary>
        /// URI Send SMS
        /// </summary>
        public static string UriSendSms = $"{GetConfig().Hosting}/api/{GetConfig().Version}/person/sms";

        #endregion

        #region Configuration

        public static Config GetConfig()
        {
            var data = File.ReadAllText(ConfigFilePath);
            var config = JsonConvert.DeserializeObject<Config>(data);
            return config;
        }

        #endregion
    }
}
