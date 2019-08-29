using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MongoDB.Bson;
using MongoDB.Driver;
using ServerChatOmnicasa.Data.Models;

namespace ServerChatOmnicasa.Data.Core
{
    public class ConnectMongoDb
    {
        #region Fields

        private MongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<BsonDocument> _collection;

        #endregion

        #region Methods

        /// <summary>
        /// Get Data of Table
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private InfoUserSms GetDataOfTable(BsonDocument document)
        {
            return new InfoUserSms
            {
                SecretKey = document[nameof(InfoUserSms.SecretKey)].AsString,
                PersonId = Convert.ToInt32(document[nameof(InfoUserSms.PersonId)].AsString),
                UserId = Convert.ToInt32(document[nameof(InfoUserSms.UserId)].AsString),
                CustomerId = Convert.ToInt32(document[nameof(InfoUserSms.CustomerId)].AsString),
                PhoneNumber = document[nameof(InfoUserSms.PhoneNumber)].AsString,
                LanguageId = Convert.ToInt32(document[nameof(InfoUserSms.LanguageId)].AsString),
                MessageContent = document[nameof(InfoUserSms.LanguageId)].AsString,
                DateSend = document[nameof(InfoUserSms.DateSend)].AsString,
                Type = Convert.ToInt32(document[nameof(InfoUserSms.Type)].AsString),
                IsSendSuccess = Convert.ToInt32(document[nameof(InfoUserSms.IsSendSuccess)].AsString),
                ErrorString = document[nameof(InfoUserSms.ErrorString)].AsString
            };
        }

        /// <summary>
        /// Document for Insert
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private BsonDocument DocumentInsert(InfoUserSms info)
        {
            return new BsonDocument
            {
                { nameof(info.SecretKey), info.SecretKey},
                { nameof(info.PersonId), info.PersonId},
                { nameof(info.UserId), info.UserId},
                { nameof(info.CustomerId), info.CustomerId},
                { nameof(info.PhoneNumber), info.PhoneNumber},
                { nameof(info.LanguageId), info.LanguageId},
                { nameof(info.MessageContent), info.MessageContent},
                { nameof(info.DateSend), info.DateSend},
                { nameof(info.Type), info.Type},
                { nameof(info.IsSendSuccess), info.IsSendSuccess},
                { nameof(info.ErrorString), info.ErrorString}
            };
        }

        /// <summary>
        /// Connect to MongoDb
        /// </summary>
        /// <param name="config"></param>
        /// <param name="connectString">Connect String with MogoDB</param>
        /// <param name="datatbaseName">Database Name</param>
        public void ConnectDb(string connectString, string datatbaseName, string tableName)
        {
            _client = new MongoClient(connectString);
            _database = _client.GetDatabase(datatbaseName);
            // GetCollection for accessing DB
            _collection = _database.GetCollection<BsonDocument>(tableName);
        }

        /// <summary>
        /// Get All Collection of Table
        /// </summary>
        /// <returns>List InfoUserSms</returns>
        public async Task<List<InfoUserSms>> GetAllCollection()
        {
            var listInfoUser = new List<InfoUserSms>();

            // Get all Data of Table
            await _collection.Find(new BsonDocument()).ForEachAsync(document =>
                {
                    var info = GetDataOfTable(document);
                    listInfoUser.Add(info);
                }
            );

            return listInfoUser;
        }

        /// <summary>
        /// Get All Collection of Table with value Search
        /// </summary>
        /// <returns>List InfoUserSms</returns>
        public async Task<List<InfoUserSms>> GetDataOfTableWithValueSearch(string keySearch, string value)
        {
            var listInfoUser = new List<InfoUserSms>();
            var builder = Builders<BsonDocument>.Filter;
            var query = builder.Eq(keySearch, value);

            // Get all Data of Table
            await _collection.Find(query).ForEachAsync(document =>
                {
                    var info = GetDataOfTable(document);
                    listInfoUser.Add(info);
                }
            );

            return listInfoUser;
        }

        /// <summary>
        /// Sort List with value Search
        /// </summary>
        /// <returns>List InfoUserSms</returns>
        /// <param name="keySearch">Column want sort</param>
        /// <param name="isSort">true: Ascending, false: Descending</param>
        public async Task<List<InfoUserSms>> SortListWithValue(string keySearch, bool isSort)
        {
            var listInfoUser = new List<InfoUserSms>();
            var sort = isSort ? Builders<BsonDocument>.Sort.Ascending(keySearch) : Builders<BsonDocument>.Sort.Descending(keySearch);

            // Get all Data of Table
            await _collection.Find(new BsonDocument()).Sort(sort).ForEachAsync(document =>
                {
                    var info = GetDataOfTable(document);
                    listInfoUser.Add(info);
                }
            );

            return listInfoUser;
        }

        /// <summary>
        /// Insert Message Document on MongoDB
        /// </summary>
        /// <param name="info">Info Message</param>
        public void InsertMessageDocument(InfoUserSms info)
        {
            var document = DocumentInsert(info);
            _collection.InsertOne(document);
        }

        #endregion

    }
}
