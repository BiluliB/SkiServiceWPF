namespace JetstreamSkiserviceAPI.DTO
{
    /// <summary>
    /// Data Transfer Object representing status
    /// </summary>
    public class StatusDto
    {
        public int? StatusId { get; set; }

        public string? StatusName { get; set; }

        public List<RegistrationDto> Registration { get; set; } = new List<RegistrationDto>();
    }
}
