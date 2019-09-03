using MongoDB.Bson.Serialization.Attributes;

namespace ServerChatOmnicasa.Data.Models
{
    public class InfoUserSms
    {
        public string SecretKey { get; set; }

        public int PersonId { get; set; }
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public string PhoneNumber { get; set; }
        public int LanguageId { get; set; }

        public string MessageContent { get; set; }

        public string DateSend { get; set; }
        public string ErrorString { get; set; }

        // Type: 0: Send, 1: Receive
        public int Type { get; set; }

        // Is Send Success: 0: Success, 1: Fail
        public int IsSendSuccess { get; set; }
    }
}
