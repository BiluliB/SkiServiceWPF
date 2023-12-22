using Newtonsoft.Json;

namespace SkiServiceWPF.DTOs
{
    /// <summary>
    /// Data Transfer Object for authentication request
    /// </summary>
    public class AuthRequestDto
    {
        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
