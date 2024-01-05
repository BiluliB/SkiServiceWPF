using System.ComponentModel.DataAnnotations;

namespace JetstreamSkiserviceAPI.Models
{
    /// <summary>
    /// Priority Class to create Database (Code first) - Database connection
    /// </summary>
    public class Priority
    {
        /// <summary>
        /// Unique identifier for Priority
        /// </summary>
        [Key]
        public int PriorityId { get; set; }

        /// <summary>
        /// Priority name choosen by client
        /// </summary>
        [Required]
        [StringLength(15)]
        public string PriorityName { get; set; }

        /// <summary>
        /// Collection of registrations associated with this priority
        /// This is a navigation property for related records in the Registrations table
        /// </summary>
        public List<Registration>? Registrations { get; set; }
    }
}
