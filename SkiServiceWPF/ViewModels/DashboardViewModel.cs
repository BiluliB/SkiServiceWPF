using System.ComponentModel;
using System.Windows;
using SkiServiceWPF.Commands;
using SkiServiceWPF.Interfaces;
using SkiServiceWPF.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using SkiServiceWPF.Views;

namespace SkiServiceWPF.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        // Deklaration der privaten Felder
        private bool _isEditViewActive;

        public event PropertyChangedEventHandler PropertyChanged;


        // Public Properties
        public bool IsEditViewActive
        {
            get => _isEditViewActive;
            set
            {
                _isEditViewActive = value;
                OnPropertyChanged(nameof(IsEditViewActive));
            }
        }

        public AsyncRelayCommand LogoutCommand { get; }
        public ICommand OpenEditViewCommand { get; private set; }

        public event EventHandler RequestEditView;

        private readonly INavigationService _navigationService;

        // Konstruktor und Methoden
        public DashboardViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            LogoutCommand = new AsyncRelayCommand(ExecuteLogout, CanExecuteLogout);
            OpenEditViewCommand = new RelayCommandNotGeneric(OnOpenEditViewCommandExecuted);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            RequestEditView?.Invoke(this, EventArgs.Empty);
            IsEditViewActive = true;
        }
    }
}
