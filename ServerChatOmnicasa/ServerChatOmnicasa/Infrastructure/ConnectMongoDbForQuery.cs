using ServerChatOmnicasa.Entities;

namespace ServerChatOmnicasa.Infrastructure
{
    public class ConnectMongoDbForQuery
    {
        public Data.Core.ConnectMongoDb ConnectDbForQuery(string tableName)
        {
            var connect = new Data.Core.ConnectMongoDb();
            connect.ConnectDb(Config.ConnectionString, Config.DatabaseName, tableName);

            return connect;
        }
    }
}
