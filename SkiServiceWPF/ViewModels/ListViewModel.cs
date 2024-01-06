using SkiServiceWPF.Commands;
using SkiServiceWPF.Models;
using SkiServiceWPF.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace SkiServiceWPF.ViewModels
{

    /// <summary>
    /// ViewModel for managing and displaying a list of registrations.
    /// </summary>
    public class ListViewModel : INotifyPropertyChanged
    {
        private readonly BackendService _backendService;
        private bool _isAscending = true;
        private ObservableCollection<RegistrationModel> _registrations;
        private RegistrationModel _selectedItem;
        private double _sortArrowAngle;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<RegistrationModel> Registrations
        {
            get => _registrations;
            set
            {
                if (_registrations != value)
                {
                    _registrations = value;
                    OnPropertyChanged(nameof(Registrations));
                }
            }
        }

        public RegistrationModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                }
            }
        }

        public double SortArrowTransform
        {
            get => _sortArrowAngle;
            set
            {
                if (_sortArrowAngle != value)
                {
                    _sortArrowAngle = value;
                    OnPropertyChanged(nameof(SortArrowTransform));
                }
            }
        }

        // Commands for various operations like loading and sorting registrations
        public ICommand LoadOpenRegistrationsCommand { get; private set; }
        public ICommand LoadRegistrationsCommand { get; private set; }
        public ICommand LoadWorkRegistrationsCommand { get; private set; }
        public ICommand LoadDoneRegistrationsCommand { get; private set; }
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
            LoadOpenRegistrationsCommand = new AsyncRelayCommand(Load_OpenRegistrations);
            LoadWorkRegistrationsCommand = new AsyncRelayCommand(Load_WorkRegistrations);
            LoadDoneRegistrationsCommand = new AsyncRelayCommand(Load_DoneRegistrations);
            SortCommand = new RelayCommand<string>(SortRegistrations);
        }
        #endregion

        /// <summary>
        /// Property changed event handler
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Asynchronously loads registrations from the backend
        /// </summary>
        /// <returns>Task representing the asynchronous operation</returns>
        /// <exception cref="Exception">Throws an exception if the loading process fails</exception>        
        private async Task Load_Registrations()
        {
            try
            {
                var registrationDtos = await _backendService.GetRegistrations("GetAllRegistrationsEndpoint");

                Registrations.Clear();

                foreach (var registrationDto in registrationDtos)
                {
                    var model = new RegistrationModel
                    {
                        RegistrationId = registrationDto.RegistrationId,
                        LastName = registrationDto.LastName,
                        FirstName = registrationDto.FirstName,
                        PickupDate = registrationDto.PickupDate,
                        Priority = registrationDto.Priority,
                        Service = registrationDto.Service,
                        Status = registrationDto.Status,
                        CreateDate = registrationDto.CreateDate,
                        Email = registrationDto.Email,
                        Phone = registrationDto.Phone,
                        Price = registrationDto.Price,
                        Comment = registrationDto.Comment
                    };
                    Registrations.Add(model);
                }
                SortRegistrations("PickupDate");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private async Task Load_OpenRegistrations()
        {
            try
            {
                var statusDtos = await _backendService.GetStatuses("GetStatusEndpoint");

                Registrations.Clear();

                foreach (var statusDto in statusDtos)
                {
                    if (statusDto.StatusName == "Offen" && statusDto.Registrations != null)
                    {
                        foreach (var registrationDto in statusDto.Registrations)
                        {
                            var model = new RegistrationModel
                            {
                                RegistrationId = registrationDto.RegistrationId,
                                LastName = registrationDto.LastName,
                                FirstName = registrationDto.FirstName,
                                PickupDate = registrationDto.PickupDate,
                                Priority = registrationDto.Priority,
                                Service = registrationDto.Service,
                                Status = registrationDto.Status,
                                CreateDate = registrationDto.CreateDate,
                                Email = registrationDto.Email,
                                Phone = registrationDto.Phone,
                                Price = registrationDto.Price,
                                Comment = registrationDto.Comment
                            };
                            Registrations.Add(model);
                        }
                    }
                }
                SortRegistrations("PickupDate");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private async Task Load_WorkRegistrations()
        {
            try
            {
                var statusDtos = await _backendService.GetStatuses("GetStatusEndpoint");

                Registrations.Clear();

                foreach (var statusDto in statusDtos)
                {
                    if (statusDto.StatusName == "InArbeit" && statusDto.Registrations != null)
                    {
                        foreach (var registrationDto in statusDto.Registrations)
                        {
                            var model = new RegistrationModel
                            {
                                RegistrationId = registrationDto.RegistrationId,
                                LastName = registrationDto.LastName,
                                FirstName = registrationDto.FirstName,
                                PickupDate = registrationDto.PickupDate,
                                Priority = registrationDto.Priority,
                                Service = registrationDto.Service,
                                Status = registrationDto.Status,
                                CreateDate = registrationDto.CreateDate,
                                Email = registrationDto.Email,
                                Phone = registrationDto.Phone,
                                Price = registrationDto.Price,
                                Comment = registrationDto.Comment
                            };
                            Registrations.Add(model);
                        }
                    }
                }
                SortRegistrations("PickupDate");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private async Task Load_DoneRegistrations()
        {
            try
            {
                var statusDtos = await _backendService.GetStatuses("GetStatusEndpoint");

                Registrations.Clear();

                foreach (var statusDto in statusDtos)
                {
                    if (statusDto.StatusName == "abgeschlossen" && statusDto.Registrations != null)
                    {
                        foreach (var registrationDto in statusDto.Registrations)
                        {
                            var model = new RegistrationModel
                            {
                                RegistrationId = registrationDto.RegistrationId,
                                LastName = registrationDto.LastName,
                                FirstName = registrationDto.FirstName,
                                PickupDate = registrationDto.PickupDate,
                                Priority = registrationDto.Priority,
                                Service = registrationDto.Service,
                                Status = registrationDto.Status,
                                CreateDate = registrationDto.CreateDate,
                                Email = registrationDto.Email,
                                Phone = registrationDto.Phone,
                                Price = registrationDto.Price,
                                Comment = registrationDto.Comment
                            };
                            Registrations.Add(model);
                        }
                    }
                }
                SortRegistrations("PickupDate");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Sorts the registrations by the given property
        /// </summary>
        /// <param name="sortProperty"></param>
        private void SortRegistrations(string sortProperty)
        {
            if (sortProperty == "PickupDate")
            {
                var sortedList = _isAscending
                    ? Registrations.OrderBy(r => r.PickupDate).ToList()
                    : Registrations.OrderByDescending(r => r.PickupDate).ToList();

                Registrations.Clear();
                foreach (var item in sortedList)
                {
                    Registrations.Add(item);
                }
                SortArrowTransform = _isAscending ? 0 : 180;
                _isAscending = !_isAscending;
            }
        }        
    }
}
