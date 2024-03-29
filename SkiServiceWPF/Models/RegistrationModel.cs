﻿namespace SkiServiceWPF.Models
{
    /// <summary>
    /// Represents a model for ski service registration, including personal details and service information
    /// This is for the ListViewUserControl
    /// </summary>
    public class RegistrationModel
    {
        
        public int RegistrationId { get; set; }

        
        public string LastName { get; set; }

        
        public string FirstName { get; set; }

        
        public string Email { get; set; }

        
        public string Phone { get; set; }

        
        public string CreateDate { get; set; }

        
        public string PickupDate { get; set; }

        
        public string Priority { get; set; }

        
        public string Service { get; set; }

        
        public string Status { get; set; }

        
        public string Price { get; set; }


        public string Comment { get; set; }
    }
}
