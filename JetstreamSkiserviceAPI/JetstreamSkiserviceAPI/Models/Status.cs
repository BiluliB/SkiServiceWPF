using System.ComponentModel.DataAnnotations;

namespace JetstreamSkiserviceAPI.Models
{
    /// <summary>
    /// Status Class to create Database (Code first) - Database connection
    /// </summary>
    public class Status
    {
        /// <summary>
        /// Unique identifier for Status
        /// </summary>
        [Key]
        public int StatusId { get; set; }

        /// <summary>
        /// Current status of the registration (Offen, InArbeit, abgeschlossen)
        /// </summary>
        [StringLength(15)]
        public string StatusName { get; set; }

        /// <summary>
        /// Collection of registrations associated with this service
        /// This is a navigation property for related records in the Registrations table
        /// </summary>
        public List<Registration> Registrations { get; set; }
    }
}
