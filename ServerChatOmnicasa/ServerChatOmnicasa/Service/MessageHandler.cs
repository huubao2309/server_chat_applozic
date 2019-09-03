using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ServerChatOmnicasa.Data.Models;
using ServerChatOmnicasa.Entities;
using ServerChatOmnicasa.Infrastructure;
using ServerChatOmnicasa.Utils;

namespace ServerChatOmnicasa.Service
{
    public class MessageHandler
    {
        //#region Test

        //public static readonly string hosting = "http://dev-mobileservice.omnicasa.com";
        //public static readonly string version = "1.0.0";
        //public static readonly string Person_IdentifyBySMS = $"{hosting}/api/{version}/person/sms";
        //public static readonly string SecretKey = "jl9knq4ctrig7wqqqsmtg";
        //public static readonly string UserId = "95";
        //public static readonly string CustomerId = "606";
        //public static readonly string phone = "84979313803";
        //public static readonly long personId = 203877;
        //public static readonly int siteID = 7;
        //public static readonly int languageId = 2;

        //// Add Handle Message
        //public async Task<bool> SendInfoMessageToSmsService()
        //{
        //    // Send Service SMS Nexmo
        //    var abc = await IdentifyItMeBySMS<string>(phone, personId, siteID, languageId);

        //    return true;
        //}

        //public async Task<T> IdentifyItMeBySMS<T>(string smsPhoneNumber, long PersonId, int siteId, int languageId, long? PropertyId = null)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(smsPhoneNumber))
        //            return default(T);

        //        var requestContent = new StringContent("", Encoding.UTF8, "application/json");

        //        using (var httpClient = new HttpClient())
        //        {
        //            httpClient.DefaultRequestHeaders.ConnectionClose = true;
        //            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
        //            var putUriPath = Person_IdentifyBySMS
        //                             + $"?secret-key={SecretKey}&customer-id={CustomerId}&user-id={UserId}&phone-number={smsPhoneNumber}&person-id={PersonId}&language-id={languageId}&site-id={siteId}";

        //            using (var httpResponse = await httpClient.PostAsync(new Uri(putUriPath), requestContent).ConfigureAwait(false))
        //            {
        //                // Convert response content to string data
        //                var serverResponse = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

        //                // Result
        //                var data = JsonConvert.DeserializeObject<T>(serverResponse);
        //                return data;
        //            }
        //        }

        //        return default(T);
        //    }
        //    catch (TaskCanceledException tEx)
        //    {
        //        Debug.WriteLine(tEx);
        //        return default(T);
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.ToString());
        //        return default(T);
        //    }
        //}

        //#endregion


        #region Fields

        public static readonly string SendSmsUri = ConfigService.UriSendSms;

        private Data.Core.ConnectMongoDb _connect = new ConnectMongoDbForQuery().ConnectDbForQuery(TableName.Message.ToString());

        public static ServiceLogger Logger { get; set; }

        public MessageHandler()
        {
            Logger = new ServiceLogger(ConfigService.LogPath);
        }

        #endregion

        #region Methods

        // Add Handle Message
        public async Task<ServerResponse> SendInfoMessageToSmsService(InfoUserSms info, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Send Service SMS Nexmo
            return await SendInfoMessageBySms(info, cancellationToken);
        }

        public async Task<ServerResponse> ReceiveInfoMessageToNexmoService(InfoUserSms info, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Push Notify
            return await PushNotifyToClient(info, cancellationToken);
        }

        private async Task<ServerResponse> SendInfoMessageBySms(InfoUserSms info, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                if (string.IsNullOrEmpty(info.PhoneNumber))
                    return null;

                var requestContent = new StringContent("", Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.ConnectionClose = true;
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
                    var putUriPath = SendSmsUri
                                     + $"?secret-key={info.SecretKey}&customer-id={info.CustomerId}&user-id={info.UserId}&phone-number={info.PhoneNumber}&person-id={info.UserId}&language-id={info.LanguageId}";

                    using (var httpResponse = await httpClient.PostAsync(new Uri(putUriPath), requestContent, cancellationToken).ConfigureAwait(false))
                    {
                        // Convert response content to string data
                        var serverResponse = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

                        // Result
                        var data = JsonConvert.DeserializeObject<ServerResponse>(serverResponse);
                        if (data.Message.Contains("OK")) info.IsSendSuccess = 0;

                        return data;
                    }
                }
            }
            catch (TaskCanceledException tEx)
            {
                Debug.WriteLine(tEx);
                info.ErrorString = "Request Timeout: " + tEx;
                info.IsSendSuccess = 1;
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                info.ErrorString = ex.ToString();
                info.IsSendSuccess = 1;
                return null;
            }
            finally
            {
                // Save MongoDB
                Logger?.Info($"Insert value to DB");
                info.DateSend = ConvertDateTime.GetDateTimeNowUtc();
                _connect.InsertMessageDocumentAsync(info);
                Logger?.Info($"Value insert DB: {JsonConvert.SerializeObject(info)}");
            }
        }

        private async Task<ServerResponse> PushNotifyToClient(InfoUserSms info, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                // Push to Firebase
                // TODO
                return null;
            }
            catch (TaskCanceledException tEx)
            {
                Debug.WriteLine(tEx);
                if (!cancellationToken.IsCancellationRequested)
                    throw;

                info.ErrorString = "Request Timeout: " + tEx;
                info.IsSendSuccess = 1;
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                info.ErrorString = ex.ToString();
                info.IsSendSuccess = 1;
                return null;
            }
            finally
            {
                // Save MongoDB
                Logger?.Info($"Insert value to DB");
                info.DateSend = ConvertDateTime.GetDateTimeNowUtc();
                _connect.InsertMessageDocumentAsync(info);
                Logger?.Info($"Value insert DB: {JsonConvert.SerializeObject(info)}");
            }
        }

        #endregion
    }
}
