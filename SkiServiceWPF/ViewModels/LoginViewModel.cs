﻿using SkiServiceWPF.Commands;
using SkiServiceWPF.DTOs;
using SkiServiceWPF.Interfaces;
using SkiServiceWPF.Services;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace SkiServiceWPF.ViewModel
{
    /// <summary>
    /// ViewModel for login functionality
    /// </summary>
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private string _errorMessage;
        private readonly BackendService _backendService;
        private readonly INavigationService _navigationService;

        // Command for handling login
        public ICommand LoginCommand { get; }

        public void ClearInputs()
        {
            UserName = string.Empty;
            Password = string.Empty;
            ErrorMessage = string.Empty;
        }

        // Event to notify when a property changes
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies listeners about property changes
        /// </summary>
        /// <param name="propertyName">Name of the changed property</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// User name for login
        /// </summary>
        public string UserName
        {
            get => _username;
            set
            {
                _username = value.Trim();
                OnPropertyChanged(nameof(UserName));
            }
        }

        /// <summary>
        /// Password for login
        /// </summary>
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        /// <summary>
        /// Error message for login failures
        /// </summary>
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        /// <summary>
        /// Initializes the ViewModel with necessary services
        /// </summary>
        /// <param name="backendService">Backend service for authentication</param>
        /// <param name="navigationService">Service for navigation</param>
        #region LoginViewModel
        public LoginViewModel(BackendService backendService, INavigationService navigationService)
        {
            _backendService = backendService;
            _navigationService = navigationService;
            LoginCommand = new AsyncRelayCommand(async () => await ExecuteLogin(), CanExecuteLogin);
        }
        #endregion

        /// <summary>
        /// Executes the login process
        /// </summary>
        /// <returns>Task for asynchronous login execution</returns>
        #region ExecuteLogin
        private async Task ExecuteLogin()
        {
            try
            {
                var authRequestDto = new AuthRequestDto
                {
                    UserName = this.UserName,
                    Password = this.Password
                };

                var responseDto = await _backendService.LoginAsync(authRequestDto);

                if (responseDto.IsSuccess)
                {
                    _backendService.SetAuthToken(responseDto.Token);
                    NavigateToDashboard();
                }
                else
                {
                    ErrorMessage = responseDto.ResponseMessage;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        #endregion

        /// <summary>
        /// Determines if the login command can be executed
        /// </summary>
        /// <returns>True if username and password are not empty</returns>
        #region CanExecuteLogin
        private bool CanExecuteLogin()
        {
            return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);
        }
        #endregion

        /// <summary>
        /// Navigates to the dashboard after successful login
        /// </summary>
        #region NavigateToDashboard
        private void NavigateToDashboard()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _navigationService.NavigateTo("Dashboard");
            });
        }
        #endregion
    }
}
