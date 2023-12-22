using Newtonsoft.Json;

namespace SkiServiceWPF.Models
{
    public class RegistrationModel
    {
        public int RegistrationId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PickupDate { get; set; }
        public string Priority { get; set; }
        public string Service { get; set; }
        public string Status { get; set; }
    }

}
