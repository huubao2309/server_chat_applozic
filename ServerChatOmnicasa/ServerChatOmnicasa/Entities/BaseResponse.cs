using Newtonsoft.Json;

namespace ServerChatOmnicasa.Entities
{
    public abstract class BaseResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }

    public class BaseResponse<T> : BaseResponse
    {
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
