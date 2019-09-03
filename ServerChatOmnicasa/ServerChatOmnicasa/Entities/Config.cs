namespace ServerChatOmnicasa.Entities
{
    public class Config
    {
        #region Fields

        // Connection String of MongoDB
        public string ConnectionString { get; set; }

        // Database Name of MongoDb
        public string DatabaseName { get; set; }

        // Hosting Web API
        public string Hosting { get; set; }
        
        // Version
        public string Version { get; set; }

        #endregion
    }
}
