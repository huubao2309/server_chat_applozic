using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerChatOmnicasa.Models
{
    public class InfoUserSms
    {
        public string SecretKey { get; set; }

        // Information of Person
        public Person Person { get; set; }

        //Type
        public Type Type { get; set; }
    }
}
