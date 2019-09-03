using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ServerChatOmnicasa.Data.Models
{
    public class InfoUserSms
    {
        [BsonId]
        public ObjectId _id { get; set; }

        public string SecretKey { get; set; }

        public int PersonId { get; set; }
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public string PhoneNumber { get; set; }
        public int LanguageId { get; set; }

        public string MessageContent { get; set; }

        public string DateSend { get; set; }
        public string ErrorString { get; set; }

        // Is Send Success: 0: Success, 1: Fail
        public int IsSendSuccess { get; set; }
    }
}
