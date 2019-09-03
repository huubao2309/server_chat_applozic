using System;
using System.Collections.Generic;
using System.Globalization;
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
                Id = document[nameof(InfoUserSms.Id)].AsInt32,
                SecretKey = document[nameof(InfoUserSms.SecretKey)].AsString,
                PersonId = document[nameof(InfoUserSms.PersonId)].AsInt32,
                UserId = document[nameof(InfoUserSms.UserId)].AsInt32,
                CustomerId = document[nameof(InfoUserSms.CustomerId)].AsInt32,
                PhoneNumber = document[nameof(InfoUserSms.PhoneNumber)].AsString,
                LanguageId = document[nameof(InfoUserSms.LanguageId)].AsInt32,
                MessageContent = document[nameof(InfoUserSms.MessageContent)].AsString,
                DateSend = document[nameof(InfoUserSms.DateSend)].AsString,
                ErrorString = document[nameof(InfoUserSms.ErrorString)].AsString,
                Type = document[nameof(InfoUserSms.Type)].AsInt32,
                IsSendSuccess = document[nameof(InfoUserSms.IsSendSuccess)].AsInt32
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
                { nameof(info.Id), info.Id}, // Set Id sequence
                { nameof(info.SecretKey), info.SecretKey},
                { nameof(info.PersonId), info.PersonId},
                { nameof(info.UserId), info.UserId},
                { nameof(info.CustomerId), info.CustomerId},
                { nameof(info.PhoneNumber), info.PhoneNumber},
                { nameof(info.LanguageId), info.LanguageId},
                { nameof(info.MessageContent), info.MessageContent},
                { nameof(info.ErrorString), info.ErrorString},
                { nameof(info.DateSend), info.DateSend},
                { nameof(info.Type), info.Type},
                { nameof(info.IsSendSuccess), info.IsSendSuccess}
            };
        }

        /// <summary>
        /// Connect to MongoDb
        /// </summary>
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
        public async Task<List<InfoUserSms>> SortListWithValue(string keySearch, bool isSort = true)
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
        public async void InsertMessageDocument(InfoUserSms info)
        {
            // Set id sequence
            var getListCollection = await GetAllCollection();
            info.Id = getListCollection.Max(a => a.Id) + 1;

            // Insert Document
            var document = DocumentInsert(info);
            _collection.InsertOne(document);
        }

        #endregion

    }
}
