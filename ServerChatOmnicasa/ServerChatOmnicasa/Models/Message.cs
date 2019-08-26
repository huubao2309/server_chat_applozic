using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerChatOmnicasa.Models
{
    public class Message
    {
        private int PersonId { get; set; }
        private int UserId { get; set; }
        private string MessageContain { get; set; }
    }
}
