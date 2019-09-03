using System;
using System.IO;
using ServerChatOmnicasa.Data.Core;

namespace ServerChatOmnicasa.Entities
{
    public class Config
    {
        // Config MongoDB
        public static readonly string ConnectionString = "mongodb://localhost:27017";
        public static readonly string DatabaseName = "ChatServiceDB";

        // Config Log File
        public static string CurrentServiceDirectory => Environment.CurrentDirectory;
        public static string LogPath => Path.Combine(CurrentServiceDirectory, "Logs");
    }
}
