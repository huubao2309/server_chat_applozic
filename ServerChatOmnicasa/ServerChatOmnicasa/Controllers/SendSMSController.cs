using System;
using System.IO;
using System.Threading.Tasks;
using CommonServiceLocator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerChatOmnicasa.Data.Core;
using ServerChatOmnicasa.Data.Models;
using ServerChatOmnicasa.Entities;
using ServerChatOmnicasa.Infrastructure;
using ServerChatOmnicasa.Logger.Utils;
using ServerChatOmnicasa.Service;
using ConnectMongoDb = ServerChatOmnicasa.Data.Core.ConnectMongoDb;

namespace ServerChatOmnicasa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendSmsController : Controller
    {
        public static ServiceLogger Logger { get; set; }

        public SendSmsController()
        {
            Logger = new ServiceLogger(Config.LogPath);
        }

        #region Test

        public static readonly string SecretKey = "jl9knq4ctrig7wqqqsmtg";
        public static readonly int UserId = 95;
        public static readonly int CustomerId = 606;
        public static readonly string phone = "84979313803";
        public static readonly long personId = 203877;
        public static readonly int siteID = 7;
        public static readonly int languageId = 2;

        #endregion

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                #region Test Insert DB to MongoDB

                //// Connect to Table "Message"
                //var connect = new ConnectMongoDbForQuery().ConnectDbForQuery(TableName.Message.ToString());

                //var asdad = new InfoUserSms
                //{
                //    SecretKey = "679345678",
                //    PersonId = 05,
                //    UserId = 56,
                //    CustomerId = 12134,
                //    PhoneNumber = "+840876323456",
                //    LanguageId = 1,
                //    MessageContent = "MessageContent",
                //    DateSend = "20190606T12:23:23",
                //    ErrorString = "Error Error",
                //    Type = 0,
                //    IsSendSuccess = 0
                //};

                //connect.InsertMessageDocument(asdad);

                #endregion

                Logger?.Info($"Get: Status Code is {StatusCodes.Status200OK}");
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                Logger?.Exception(ex,$"Status Code is {StatusCodes.Status500InternalServerError}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
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
                if (info.Type == 0) /*Type Send*/
                {
                    Logger?.Info("Type of Message is Send");

                    // Push Message to SMS Service
                    var isSendSuccess = await messageHandler.SendInfoMessageToSmsService(info);
                    Logger?.Info($"Send message is {isSendSuccess.Message}");
                    return StatusCode(StatusCodes.Status200OK);
                }

                Logger?.Info($"Status Code {StatusCodes.Status401Unauthorized}");
                return StatusCode(StatusCodes.Status401Unauthorized, "Don't have type SMS");
            }
            catch (Exception ex)
            {
                Logger?.Exception(ex,$"Status Code {StatusCodes.Status500InternalServerError}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}