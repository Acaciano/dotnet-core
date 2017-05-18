
using Newtonsoft.Json;

namespace Middleware.Api.Models
{
    public class RequestError
    {
        [JsonProperty(PropertyName = "messageResult")]
        public string MessageResult { get; set; }

        [JsonProperty(PropertyName = "messageError")]
        public string MessageError { get; set; }

        [JsonProperty(PropertyName = "codeResult")]
        public int CodeResult { get; set; }
    }
}