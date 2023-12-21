using SkiServiceWPF.Commands;
using SkiServiceWPF.Interfaces;
using SkiServiceWPF.Models;
using SkiServiceWPF.Services;
using SkiServiceWPF.Views;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace SkiServiceWPF.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private string _errorMessage;
        private readonly BackendService _backendService;
        private readonly INavigationService _navigationService;

        public string UserName
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel(BackendService backendService, INavigationService navigationService)
        {
            _backendService = backendService;
            _navigationService = navigationService;
            LoginCommand = new RelayCommand(async () => await ExecuteLogin(), CanExecuteLogin);
        }


        private async Task ExecuteLogin()
        {
            try
            {
                var authRequest = new AuthRequestModel
                {
                    UserName = this.UserName,
                    Password = this.Password
                };
                var response = await _backendService.LoginAsync(authRequest);
                NavigateToDashboard();
            }
            catch (Exception ex)
            {
                ErrorMessage = "Login fehlgeschlagen: " + ex.Message;
            }
        }

        private bool CanExecuteLogin()
        {
            return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);
        }

        private void NavigateToDashboard()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _navigationService.NavigateTo("Dashboard");
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
