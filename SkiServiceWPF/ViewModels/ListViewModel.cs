using SkiServiceWPF.Commands;
using SkiServiceWPF.Models;
using SkiServiceWPF.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace SkiServiceWPF.ViewModels
{

    /// <summary>
    /// ViewModel for managing and displaying a list of registrations
    /// </summary>
    public class ListViewModel : INotifyPropertyChanged
    {
        private readonly BackendService _backendService;

        private bool _isAscending = true;

        // Event for property change notifications
        public event PropertyChangedEventHandler? PropertyChanged;

        // Collection of registration models for data binding
        public ObservableCollection<RegistrationModel> Registrations { get; private set; }

        // Command to load registration data
        public ICommand LoadRegistrationsCommand { get; private set; }

        public ICommand SortCommand { get; private set; }

        /// <summary>
        /// Constructor initializing the ViewModel with a backend service
        /// </summary>
        /// <param name="backendService">Service for backend operations</param>
        #region ListViewModel
        public ListViewModel(BackendService backendService)
        {
            _backendService = backendService;
            Registrations = new ObservableCollection<RegistrationModel>();
            LoadRegistrationsCommand = new AsyncRelayCommand(Load_Registrations);
            SortCommand = new RelayCommand<string>(SortRegistrations);
        }
        #endregion

        /// <summary>
        /// Asynchronously loads registrations from the backend
        /// </summary>
        /// <returns>Task representing the asynchronous operation</returns>
        /// <exception cref="Exception">Throws an exception if the loading process fails</exception>
        #region Load_Registrations
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
        #endregion

        private void SortRegistrations(string sortProperty)
        {
            if (sortProperty == "PickupDate")
            {
                if (_isAscending)
                {
                    Registrations = new ObservableCollection<RegistrationModel>(Registrations.OrderBy(r => r.PickupDate));
                }
                else
                {
                    Registrations = new ObservableCollection<RegistrationModel>(Registrations.OrderByDescending(r => r.PickupDate));
                }
                _isAscending = !_isAscending;
            }
           
        }
    }
}
