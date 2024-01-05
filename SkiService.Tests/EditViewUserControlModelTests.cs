using SkiServiceWPF.Models;
using SkiServiceWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiService.Tests
{
    public class EditViewUserControlModelTests
    {
        [Fact]
        public void EditViewUserControlModel_InitializesAllPropertiesCorrectly()
        {
            // Arrange
            var registrationModel = new RegistrationModel
            {
                Status = "InArbeit",
                Priority = "Express",
                Service = "Grosser Service",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "1234567890"
                // Add other properties of RegistrationModel as needed
            };

            // Act
            var editViewModel = new EditViewUserControlModel(registrationModel);

            // Assert Dropdowns Not Null
            Assert.NotNull(editViewModel.StatusDropdown);
            Assert.NotNull(editViewModel.PriorityDropdown);
            Assert.NotNull(editViewModel.ServiceDropdown);

            // Assert Dropdown Counts
            Assert.Equal(4, editViewModel.StatusDropdown.Count);
            Assert.Equal(3, editViewModel.PriorityDropdown.Count);
            Assert.Equal(6, editViewModel.ServiceDropdown.Count);

            // Assert Correct Selections
            Assert.Equal("InArbeit", editViewModel.SelectedStatus.Display);
            Assert.Equal("Express", editViewModel.SelectedPriority.Display);
            Assert.Equal("Grosser Service", editViewModel.SelectedService.Display);

            // Assert Model Properties
            Assert.Equal(registrationModel.FirstName, editViewModel.Model.FirstName);
            Assert.Equal(registrationModel.LastName, editViewModel.Model.LastName);
            Assert.Equal(registrationModel.Email, editViewModel.Model.Email);
            Assert.Equal(registrationModel.Phone, editViewModel.Model.Phone);
            // Add assertions for other RegistrationModel properties as needed

            // Additional assertions to verify dropdown values
            Assert.Contains(editViewModel.StatusDropdown, item => item.Display == "Offen");
            Assert.Contains(editViewModel.PriorityDropdown, item => item.Display == "Tief");
            Assert.Contains(editViewModel.ServiceDropdown, item => item.Display == "Kleiner Service");

            // Test for Selection Change
            var newStatus = editViewModel.StatusDropdown.FirstOrDefault(x => x.Display == "Offen");
            editViewModel.SelectedStatus = newStatus;
            Assert.Equal("Offen", editViewModel.SelectedStatus.Display);
        }
    }
}
