using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace ServerChatOmnicasa.Data.Models
{
    public class Person
    {
        public string PersonId { get; set; }
        public string UserId { get; set; }
        public string CustomerId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
