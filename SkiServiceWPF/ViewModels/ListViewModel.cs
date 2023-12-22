using SkiServiceWPF.Commands;
using SkiServiceWPF.Models;
using SkiServiceWPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SkiServiceWPF.ViewModels
{
    public class ListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand LoadRegistrationsCommand { get; private set; }
        public ObservableCollection<RegistrationModel> Registrations { get; private set; }

        private readonly BackendService _backendService;

        public ListViewModel(BackendService backendService)
        {
            _backendService = backendService;
            Registrations = new ObservableCollection<RegistrationModel>();
            LoadRegistrationsCommand = new RelayCommand(async () => await Load_Registrations());
        }

        private async Task Load_Registrations()
        {
            try
            {
                var registrationDtos = await _backendService.GetRegistrations("GetAllRegistrationsEndpoint");
                Registrations.Clear();
                foreach (var registrationdto in registrationDtos)
                {
                    var model = new RegistrationModel
                    {
                        RegistrationId = registrationdto.RegistrationId,
                        LastName = registrationdto.LastName,
                        FirstName = registrationdto.FirstName,
                        PickupDate = registrationdto.PickupDate,
                        Priority = registrationdto.Priority,
                        Service = registrationdto.Service,
                        Status = registrationdto.Status

                    };
                    Registrations.Add(model);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
