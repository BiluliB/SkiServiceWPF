using SkiServiceWPF.Commands;
using SkiServiceWPF.Common;
using SkiServiceWPF.Models;
using SkiServiceWPF.Views;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SkiServiceWPF.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly UserLoginApi _userLoginApi;
        private string _username;
        private string _password;
        private string _errorMessage;

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

        public LoginViewModel(UserLoginApi userLoginApi)
        {
            _userLoginApi = userLoginApi;
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
                var response = await _userLoginApi.LoginAsync(authRequest);
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
                var dashboardView = new DashboardView();
                var mainWindow = Application.Current.MainWindow;
                mainWindow.Content = dashboardView;
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
