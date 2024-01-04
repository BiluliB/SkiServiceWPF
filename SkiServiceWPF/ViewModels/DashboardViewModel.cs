using System.ComponentModel;
using System.Windows;
using SkiServiceWPF.Commands;
using SkiServiceWPF.Interfaces;
using SkiServiceWPF.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using SkiServiceWPF.Views;
using SkiServiceWPF.Models;
using SkiServiceWPF.Common;
using System.Collections.ObjectModel;

namespace SkiServiceWPF.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        // Deklaration der privaten Felder
        private bool _isEditViewActive;

        public event PropertyChangedEventHandler PropertyChanged;
        private readonly BackendService _backendService;
        private ObservableCollection<RegistrationModel> _registrations;
        public ICommand SaveEditCommand { get; private set; }
        public event Action OnRequireRefresh;

        public bool IsEditViewActive
        {
            get => _isEditViewActive;
            set
            {
                _isEditViewActive = value;
                OnPropertyChanged(nameof(IsEditViewActive));
            }
        }

        private RegistrationModel _selectedItem;
        public RegistrationModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public AsyncRelayCommand LogoutCommand { get; }
        public ICommand OpenEditViewCommand { get; private set; }
        public ICommand DeleteCommand { get; }

        public event EventHandler RequestEditView;

        private readonly INavigationService _navigationService;

        // Konstruktor und Methoden
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

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public ObservableCollection<RegistrationModel> Registrations
        {
            get => _registrations;
            set
            {
                _registrations = value;
                OnPropertyChanged(nameof(Registrations));
            }
        }

        private bool CanExecuteLogout()
        {
            return true;
        }

        private async Task ExecuteLogout()
        {
            MessageBoxResult result = MessageBox.Show("Möchten Sie sich wirklich abmelden?", "Abmeldung", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _navigationService.NavigateTo("LoginView");
            }
        }

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


        private async void ExecuteDeleteCommand()
        {
            var selectedRegistration = SelectionHelper.Selected as RegistrationModel;
            if (selectedRegistration == null)
            {
                MessageBox.Show("Bitte wählen Sie einen Auftrag aus.");
                return;
            }

            var deleteWindow = new DeleteWindow
            {
                DataContext = selectedRegistration
            };

            var dialogResult = deleteWindow.ShowDialog();
            if (dialogResult == true)
            {
                var success = await _backendService.DeleteRegistrationAsync(selectedRegistration.RegistrationId);
                if (success)
                {
                    MessageBox.Show("Auftrag wurde gelöscht.");
                    Registrations.Remove(selectedRegistration);
                    SelectedItem = null;

                    OnRequireRefresh?.Invoke();
                }
                else
                {
                    MessageBox.Show("Fehler beim Löschen des Auftrags.");
                }
            }

        }

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
    }
}
