using System.ComponentModel.DataAnnotations;

namespace JetstreamSkiserviceAPI.Models
{
    /// <summary>
    /// Service Class to create Database (Code first) - Database connection
    /// </summary>
    public class Service
    {
        /// <summary>
        /// Unique identifier for the service.
        /// </summary>
        [Key]
        public int ServiceId { get; set; }

        /// <summary>
        /// Name of the service offered.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string ServiceName { get; set; }

        /// <summary>
        /// Collection of registrations associated with this service
        /// This is a navigation property for related records in the Registrations table
        /// </summary>
        public List<Registration> Registrations { get; set; }
    }
}
