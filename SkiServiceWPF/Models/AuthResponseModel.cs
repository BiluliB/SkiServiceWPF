namespace SkiServiceWPF.Models
{
    /// <summary>
    /// Represents a response model for authentication, indicating success and providing a response message
    /// </summary>
    public class AuthResponseModel
    {
        public bool IsSuccess { get; set; }
        public string ResponseMessage { get; set; }
    }
}
