using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerChatOmnicasa.Models
{
    public class Person
    {
        public string PersonId { get; set; }
        public string UserId { get; set; }
        public string CustomerId { get; set; }
        public IEnumerable<string> PhoneNumber { get; set; }
    }
}
