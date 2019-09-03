using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServerChatOmnicasa.Base;
using ServerChatOmnicasa.Data.Models;
using ServerChatOmnicasa.Service;

namespace ServerChatOmnicasa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiveSmsController : BaseController
    {
        [HttpPost] // Send SMS
        public async Task<ActionResult<InfoUserSms>> Post(InfoUserSms info)
        {
            try
            {
                if (info == null)
                {
                    Logger?.Info("Info of Message is null");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
                }

                // If Id don't have value
                if (info.Id == 0)
                    info.Id = -1;

                Logger?.Info($"Info of Message {JsonConvert.SerializeObject(info)}");
                var messageHandler = new MessageHandler();
                if (info.Type == 1) /*Type Send*/
                {
                    Logger?.Info("Type of Message is Receive");

                    // Cancel Task after 5s
                    TokenSource.CancelAfter(TimeSpan.FromSeconds(5));

                    // Push Message to SMS Service
                    var pushMessageSuccess = await messageHandler.ReceiveInfoMessageToNexmoService(info, TokenSource.Token);
                    Logger?.Info($"Push message is {pushMessageSuccess.Message}");
                }

                Logger?.Info($"Status Code {StatusCodes.Status401Unauthorized}");
                return StatusCode(StatusCodes.Status401Unauthorized, "Don't have type SMS");
            }
            catch (Exception ex)
            {
                Logger?.Exception(ex, $"Status Code {StatusCodes.Status500InternalServerError}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}