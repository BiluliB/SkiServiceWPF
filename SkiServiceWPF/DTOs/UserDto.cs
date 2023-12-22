using Newtonsoft.Json;

namespace SkiServiceWPF.DTOs
{
    /// <summary>
    /// Data Transfer Object for user data
    /// </summary>
    public class UserDto
    {
        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
