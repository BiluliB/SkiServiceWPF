namespace SkiServiceWPF.Models
{
    /// <summary>
    /// Represents a model for authentication requests containing user credentials
    /// </summary>
    public class AuthRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
