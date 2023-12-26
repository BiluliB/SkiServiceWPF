using SkiServiceWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiServiceWPF.ViewModels
{
    
    public class EditViewUserControlModel
    {
        public ObservableCollection<DataDropdown> StatusDropdown { get; set; }
        public RegistrationModel Model { get; set; }

        public DataDropdown SelectedStatus { get; set; }
        public EditViewUserControlModel(Models.RegistrationModel registrationModel)
        {
            Model = registrationModel;
            StatusDropdown = new ObservableCollection<DataDropdown>
            {
                new DataDropdown
                {

                    Id = 1,
                    Display = "Offen",

                },
                new DataDropdown
                {
                    Id= 2,
                    Display = "In Arbeit"
                },
                new DataDropdown 
                { 
                    Id= 3, 
                    Display = "Abgeschlossen"
                },
                new DataDropdown
                { 
                    Id= 4,
                    Display = "Storniert"
                }
             };
            SelectedStatus = StatusDropdown.Where(s => s.Display.Equals(Model.Status)).FirstOrDefault();
        }
    }
}
