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

        // Is Send Success: 1: Success, 0: Fail
        public int IsSendSuccess { get; set; }
    }
}
