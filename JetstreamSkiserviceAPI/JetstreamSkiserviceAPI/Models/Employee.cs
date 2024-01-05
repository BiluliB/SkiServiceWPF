using System.ComponentModel.DataAnnotations;

namespace JetstreamSkiserviceAPI.Models
{
    /// <summary>
    /// Employee Class to create Database (Code first) - Database connection
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Unique identifier for Employee
        /// </summary>
        [Key]
        public int EmployeeId { get; set; }

        /// <summary>
        /// Username of the employee
        /// </summary>
        [StringLength(50)]
        public string? Username { get; set; }

        /// <summary>
        /// Password of the employee
        /// </summary>
        [StringLength(50)]
        public string? Password { get; set; }

        /// <summary>
        /// Attempts the employee needed - account gets banned after 3 false login attempts
        /// </summary>
        public int? Attempts { get; set; }
    }
}
