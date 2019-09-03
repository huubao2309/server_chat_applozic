using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerChatOmnicasa.Data.Models;
using ServerChatOmnicasa.Entities;
using ServerChatOmnicasa.Infrastructure;
using ServerChatOmnicasa.Logger.Utils;
using ServerChatOmnicasa.Service;

namespace ServerChatOmnicasa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiveSmsController : Controller
    {
        public static ServiceLogger Logger { get; set; }

        public ReceiveSmsController()
        {
            Logger = new ServiceLogger(Config.LogPath);
        }

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

                Logger?.Info($"Info of Message {info}");
                var messageHandler = new MessageHandler();
                if (info.Type == 1) /*Type Send*/
                {
                    Logger?.Info("Type of Message is Receive");

                    // Push Message to SMS Service
                    var isPushMessageSuccess = await messageHandler.ReceiveInfoMessageToNexmoService(info);
                    Logger?.Info($"Push message is {isPushMessageSuccess}");
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