using ServerChatOmnicasa.Utils;

namespace ServerChatOmnicasa.Infrastructure
{
    public class ConnectMongoDbForQuery
    {
        public Data.Core.ConnectMongoDb ConnectDbForQuery(string tableName)
        {
            var connect = new Data.Core.ConnectMongoDb();
            connect.ConnectDb(ConfigService.Config.ConnectionString, ConfigService.Config.DatabaseName, tableName);

            return connect;
        }
    }
}
