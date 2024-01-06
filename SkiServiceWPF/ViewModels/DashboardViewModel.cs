using SkiServiceWPF.Commands;
using SkiServiceWPF.Common;
using SkiServiceWPF.Interfaces;
using SkiServiceWPF.Models;
using SkiServiceWPF.Services;
using SkiServiceWPF.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace SkiServiceWPF.ViewModels
{
    /// <summary>
    /// ViewModel for the Dashboard view.
    /// </summary>
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        private readonly BackendService _backendService;

        private bool _isEditViewActive;

        private ObservableCollection<RegistrationModel> _registrations;
        private RegistrationModel _selectedItem;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler RequestEditView;
        public event Action OnRequireRefresh;

        /// <summary>
        /// Indicates if the edit view is active.
        /// </summary>
        public bool IsEditViewActive
        {
            get => _isEditViewActive;
            set
            {
                _isEditViewActive = value;
                OnPropertyChanged(nameof(IsEditViewActive));
            }
        }

        /// <summary>
        /// The selected registration item.
        /// </summary>
        public RegistrationModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        /// <summary>
        /// Collection of registration models.
        /// </summary>
        public ObservableCollection<RegistrationModel> Registrations
        {
            get => _registrations;
            set
            {
                _registrations = value;
                OnPropertyChanged(nameof(Registrations));
            }
        }

        // Commands
        public AsyncRelayCommand LogoutCommand { get; }
        public ICommand OpenEditViewCommand { get; private set; }
        public ICommand DeleteCommand { get; }
        public ICommand SaveEditCommand { get; private set; }


        /// <summary>
        /// Constructor initializing commands and services.
        /// </summary>
        public DashboardViewModel(INavigationService navigationService, BackendService backendService)
        {
            _backendService = backendService;
            _navigationService = navigationService;
            LogoutCommand = new AsyncRelayCommand(ExecuteLogout, CanExecuteLogout);
            OpenEditViewCommand = new RelayCommandNotGeneric(OnOpenEditViewCommandExecuted);
            DeleteCommand = new RelayCommandNotGeneric(ExecuteDeleteCommand);
            Registrations = new ObservableCollection<RegistrationModel>();
            SaveEditCommand = new RelayCommandNotGeneric(SaveEdit);
        }

        // Method to check if logout can be executed
        private bool CanExecuteLogout()
        {
            return true;
        }

        // Method to perform logout operation
        private async Task ExecuteLogout()
        {
            MessageBoxResult result = MessageBox.Show("Möchten Sie sich wirklich abmelden?", "Abmeldung", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _navigationService.NavigateTo("LoginView");
            }
        }

        // Command execution for opening the edit view
        private void OnOpenEditViewCommandExecuted()
        {
            var selectedRegistration = SelectionHelper.Selected as RegistrationModel;
            if (selectedRegistration == null)
            {
                MessageBox.Show("Bitte wählen Sie einen Auftrag aus der Liste aus, bevor Sie bearbeiten.");
                return;
            }

            RequestEditView?.Invoke(this, EventArgs.Empty);
            IsEditViewActive = true;
        }

        // Method to execute the delete command
        private async void ExecuteDeleteCommand()
        {
            var selectedRegistration = SelectionHelper.Selected as RegistrationModel;
            if (selectedRegistration == null)
            {
                MessageBox.Show("Bitte wählen Sie einen Auftrag aus.");
                return;
            }

            var confirmCancelWindow = new DeleteWindow
            {
                DataContext = selectedRegistration
            };

            var dialogResult = confirmCancelWindow.ShowDialog();
            if (dialogResult == true)
            {
                selectedRegistration.Status = "storniert";
                var success = await _backendService.UpdateRegistrationAsync(selectedRegistration);
                if (success)
                {
                    MessageBox.Show("Auftrag wurde storniert.");
                    OnRequireRefresh?.Invoke();
                }
                else
                {
                    MessageBox.Show("Fehler beim Stornieren des Auftrags.");
                }
            }
        }

        // Method to save edits
        private async void SaveEdit()
        {
            var editModel = SelectionHelper.Selected as RegistrationModel;
            if (editModel != null)
            {
                var success = await _backendService.UpdateRegistrationAsync(editModel);
                if (success)
                {
                    MessageBox.Show("Änderungen gespeichert.");
                    OnRequireRefresh?.Invoke();
                    IsEditViewActive = false;
                }
                else
                {
                    MessageBox.Show("Fehler beim Speichern der Änderungen.");
                }
            }
            else
            {
                MessageBox.Show("Kein Auftrag ausgewählt.");
            }
        }

        // Property change notification
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
    }
}
