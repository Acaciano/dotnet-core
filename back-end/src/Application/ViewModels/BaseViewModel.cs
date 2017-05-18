using System;
using Newtonsoft.Json;

namespace Application.ViewModels
{
    public class BaseViewModel
    {
        public BaseViewModel()
        {
            Id = Guid.NewGuid();
            RegistrationDate = DateTime.Now;
            Active = true;
        }

        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        
        [JsonProperty(PropertyName = "active")]
        public bool Active { get; set; }

        [JsonProperty(PropertyName = "registrationDate")]
        public DateTime RegistrationDate { get; set; }

        [JsonProperty(PropertyName = "dateModified")]
        public DateTime? DateModified { get; set; }
    }
}
