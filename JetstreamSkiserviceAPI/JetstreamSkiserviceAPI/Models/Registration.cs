using System.ComponentModel.DataAnnotations;

namespace JetstreamSkiserviceAPI.Models
{
    /// <summary>
    /// Priority Class to create Database (Code first) - Database connection
    /// It contains all the necessary information related to a registration
    /// </summary>
    public class Registration
    {
        /// <summary>
        /// Unique identifier for the registration
        /// </summary>
        [Key]
        public int RegistrationId { get; set; }

        /// <summary>
        /// FirstName of the person registering for the ski service
        /// </summary>
        [StringLength(255)]
        [Required(ErrorMessage = "Bitte einen gültigen Vornamen eingeben.")]
        [RegularExpression("^[a-zA-ZäöüÄÖÜ]+([- ][a-zA-ZäöüÄÖÜ]+)*$", ErrorMessage = "Ungültiges Format für den Vornamen.")]
        public string? FirstName { get; set; }

        /// <summary>
        /// LastName of the person registering for the ski service
        /// </summary>
        [StringLength(255)]
        [Required(ErrorMessage = "Bitte einen gültigen Nachnamen eingeben.")]
        [RegularExpression("^[a-zA-ZäöüÄÖÜ]+([- ][a-zA-ZäöüÄÖÜ]+)*$", ErrorMessage = "Ungültiges Format für den Nachnamen.")]
        public string? LastName { get; set; }

        /// <summary>
        /// Email address of the person registering for the ski service
        /// </summary>
        [StringLength(255)]
        [Required(ErrorMessage = "Bitte eine gültige E-Mail eingeben.")]
        [EmailAddress(ErrorMessage = "Ungültiges E-Mail Format.")]
        public string? Email { get; set; }

        /// <summary>
        /// Phone number of the person registering for the ski service
        /// </summary>
        [StringLength(30)]
        [Required(ErrorMessage = "Bitte eine gültige Telefonnummer eingeben.")]
        [RegularExpression(@"(\b(0041|0)|\B\+41)(\s?\(0\))?(\s)?[1-9]{2}(\s)?[0-9]{3}(\s)?[0-9]{2}(\s)?[0-9]{2}\b", ErrorMessage = "Ungültiges Telefonnummern-Format.")]
        public string? Phone { get; set; }

        /// <summary>
        /// The date and time when the registration was created/ Service begin date and time
        /// </summary>
        public DateTime Create_date { get; set; }

        /// <summary>
        /// The date and time when the skis should be picked up after service
        /// </summary>
        public DateTime Pickup_date { get; set; }

        public int StatusId { get; set; }

        /// <summary>
        /// Current status of the registration (Offen, InArbeit, abgeschlossen)
        /// </summary>
        public Status? Status { get; set; }

        public int PriorityId { get; set; }

        /// <summary>
        /// Priority level of the service request (Tief, Standard, Express)
        /// </summary>
        public Priority? Priority { get; set; }

        public int ServiceId { get; set; }

        /// <summary>
        /// Type of service requested from the client
        /// </summary>
        public Service? Service { get; set; }

        /// <summary>
        /// Price of the service
        /// </summary>
        public string? Price { get; set; }

        /// <summary>
        /// Additional comments or special instructions for the ski service
        /// </summary>
        [StringLength(255)]
        public string? Comment { get; set; }
    }
}
