using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator;
using Newtonsoft.Json;
using ServerChatOmnicasa.Models;

namespace ServerChatOmnicasa.Service
{
    public class MessageHandler : IMessageCodeHandler
    {
        // Add Handle Message
        public async Task<Person> SendInfoMessage(Person personMessage)
        {
            // Send Service SMS Nexmo
            //var abc = await IdentifyItMeBySMS<string>(personMessage.PhoneNumber, Convert.ToInt32(personMessage.PersonId), Convert.ToInt32(personMessage.UserId), 1);

            return null;
        }

        public async Task<T> IdentifyItMeBySMS<T>(string smsPhoneNumber, long PersonId, int siteId, int languageId, long? PropertyId = null)
        {
            try
            {
                //if (string.IsNullOrEmpty(smsPhoneNumber))
                //    return default(T);

                //var requestContent = new StringContent("", Encoding.UTF8, "application/json");
                //using (var httpClientHandler = ServiceLocator.Current.GetInstance<HttpClientHandler>() ?? new HttpClientHandler())
                //{
                //    using (var httpClient = new HttpClient(httpClientHandler, true))
                //    {
                //        httpClient.DefaultRequestHeaders.ConnectionClose = true;
                //        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
                //        var putUriPath = ServiceURLs.Person_IdentifyBySMS
                //                         + $"?secret-key={SecretKey}&customer-id={CustomerId}&user-id={UserId}&phone-number={smsPhoneNumber}&person-id={PersonId}&language-id={languageId}&site-id={siteId}";

                //        using (var httpResponse = await httpClient.PostAsync(new Uri(putUriPath), requestContent).ConfigureAwait(false))
                //        {
                //            // Convert response content to string data
                //            var serverResponse = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

                //            // Result
                //            var data = JsonConvert.DeserializeObject<T>(serverResponse);
                //            return data;
                //        }
                //    }
                //}
                return default(T);
            }
            catch (TaskCanceledException tEx)
            {
                Debug.WriteLine(tEx);
                return default(T);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return default(T);
            }
        }
    }


}
