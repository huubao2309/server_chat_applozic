using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServerChatOmnicasa.Models;

namespace ServerChatOmnicasa.Service
{
    public interface IMessageCodeHandler
    {
        // Public APIs
        Task<Person> SendInfoMessage(Person personMessage);
    }
}
