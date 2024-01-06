using SkiServiceWPF.Models;
using System.Collections.ObjectModel;

namespace SkiServiceWPF.ViewModels
{
    /// <summary>
    /// ViewModel for the EditView UserControl.
    /// </summary>
    public class EditViewUserControlModel
    {
        public RegistrationModel Model { get; set; }

        public ObservableCollection<DataDropdown> StatusDropdown { get; set; }
        public ObservableCollection<DataDropdown> PriorityDropdown { get; set; }
        public ObservableCollection<DataDropdown> ServiceDropdown { get; set; }
        

        public DataDropdown SelectedStatus { get; set; }
        public DataDropdown SelectedPriority { get; set; }
        public DataDropdown SelectedService { get; set; }

        /// <summary>
        /// Initializes the ViewModel with registrationModel and dropdowns.
        /// </summary>
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
                    Display = "InArbeit"
                },
                new DataDropdown 
                { 
                    Id= 3, 
                    Display = "abgeschlossen"
                },
                new DataDropdown
                { 
                    Id= 4,
                    Display = "storniert"
                }
             };
            SelectedStatus = StatusDropdown.Where(s => s.Display.Equals(Model.Status)).FirstOrDefault();

            PriorityDropdown = new ObservableCollection<DataDropdown>
            {
                new DataDropdown
                {

                    Id = 1,
                    Display = "Tief",

                },
                new DataDropdown
                {
                    Id= 2,
                    Display = "Standard"
                },
                new DataDropdown
                {
                    Id= 3,
                    Display = "Express"
                }                
             };
            SelectedPriority = PriorityDropdown.Where(s => s.Display.Equals(Model.Priority)).FirstOrDefault();


            ServiceDropdown = new ObservableCollection<DataDropdown>
            {
                new DataDropdown
                {

                    Id = 1,
                    Display = "Kleiner Service",

                },
                new DataDropdown
                {
                    Id= 2,
                    Display = "Grosser Service"
                },
                new DataDropdown
                {
                    Id= 3,
                    Display = "Rennski Service"
                },
                new DataDropdown
                {
                    Id= 4,
                    Display = "Bindungen montieren und einstellen"
                },
                new DataDropdown
                {
                    Id = 5,
                    Display = "Fell zuschneiden"
                },
                new DataDropdown { 
                    Id= 6,
                    Display = "Heisswachsen"
                }

             };
            SelectedService = ServiceDropdown.Where(s => s.Display.Equals(Model.Service)).FirstOrDefault();
        }
    }
}
