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
    public class SendSmsController : BaseController
    {
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
                Logger?.Exception(ex, $"Status Code is {StatusCodes.Status500InternalServerError}");
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

                Logger?.Info($"Info of Message {JsonConvert.SerializeObject(info)}");
                var messageHandler = new MessageHandler();
                Logger?.Info("Type of Message is Send");

                //Cancel Task after 5s
                TokenSource.CancelAfter(TimeSpan.FromSeconds(5));

                //Push Message to SMS Service
                var sendSuccess = await messageHandler.SendInfoMessageToSmsService(info, TokenSource.Token);
                Logger?.Info($"Send message is {sendSuccess.Message}");
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                Logger?.Exception(ex, $"Status Code {StatusCodes.Status500InternalServerError}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}