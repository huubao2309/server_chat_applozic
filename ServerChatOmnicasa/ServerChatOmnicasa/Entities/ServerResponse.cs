using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerChatOmnicasa.Entities
{
    public class ServerResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Elapse { get; set; }
        public string Worker { get; set; }
        public string Data { get; set; }
    }
}
