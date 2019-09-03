using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerChatOmnicasa.Service
{
    public interface IMessageCodeHandler
    {
        // Public APIs
        Task<bool> SendInfoMessageToSmsService();
    }
}
