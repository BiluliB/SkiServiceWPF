using Newtonsoft.Json;

namespace SkiServiceWPF.Models
{
    public class RegistrationsModel
    {
        [JsonProperty("registrationId")]
        public int RegistrationId { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("create_date")]
        public string CreateDate { get; set; }

        [JsonProperty("pickup_date")]
        public string PickupDate { get; set; }

        [JsonProperty("priority")]
        public string Priority { get; set; }

        [JsonProperty("service")]
        public string Service { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }
    }
}
