using System.Collections.Generic;
using Newtonsoft.Json;

namespace Middleware.Api.Models
{
    public class RequestErrorReturn
    {
        public RequestErrorReturn()
        {
            Errors = new List<RequestError>();
        }

        [JsonProperty(PropertyName = "errors")]
        public List<RequestError> Errors { get; set; }
    }
}