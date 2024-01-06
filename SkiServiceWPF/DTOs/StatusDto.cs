using Newtonsoft.Json;

namespace SkiServiceWPF.DTOs
{
    /// <summary>
    /// Data Transfer Object for status.
    /// </summary>
    public class StatusDto
    {
        [JsonProperty("statusId")]
        public int StatusId { get; set; }

        [JsonProperty("statusName")]
        public string StatusName { get; set; }

        [JsonProperty("registration")]
        public List<RegistrationsDto> Registrations { get; set; } = new List<RegistrationsDto>();
    }
}
