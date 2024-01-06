using Microsoft.Extensions.DependencyInjection;
using SkiServiceWPF.Helpers;
using SkiServiceWPF.ViewModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SkiServiceWPF.Views
{
    /// <summary>
    /// Represents the login view control for the application.
    /// </summary>
    public partial class LoginView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the LoginView class.
        /// </summary>
        public LoginView()
        {
            InitializeComponent();

            var loginViewModel = ((App)Application.Current).ServiceProvider.GetRequiredService<LoginViewModel>();
            loginViewModel.ClearInputs();

            var viewModel = ((App)Application.Current).ServiceProvider.GetRequiredService<LoginViewModel>();
            DataContext = viewModel;

            viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        /// <summary>
        /// Handles password change events, updating the ViewModel accordingly.
        /// </summary>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                PasswordHandlingHelper.HandlePasswordChanged((PasswordBox)sender, viewModel);
            }
        }

        /// <summary>
        /// Responds to property changes in the ViewModel, updating UI elements as needed.
        /// </summary>
        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LoginViewModel.ErrorMessage) && sender is LoginViewModel viewModel)
            {
                PasswordHandlingHelper.UpdatePasswordBoxBorder(viewModel, passwordBox, usernameTextBox);
            }
        }

        /// <summary>
        /// Temporarily shows the password in plain text on mouse down.
        /// </summary>
        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PasswordHandlingHelper.ShowPassword(passwordBox, passwordTxtBox);
            e.Handled = true;
        }

        /// <summary>
        /// Hides the password text and shows the password field again on mouse up.
        /// </summary>
        private void Button_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            PasswordHandlingHelper.HidePassword(passwordBox, passwordTxtBox);
            e.Handled = true;
        }


    }
}
