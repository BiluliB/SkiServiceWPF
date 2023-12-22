using Newtonsoft.Json;

namespace SkiServiceWPF.DTOs
{
    /// <summary>
    /// Data Transfer Object for login response
    /// </summary>
    public class LoginResponseDto
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
