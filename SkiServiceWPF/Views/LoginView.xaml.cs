using Microsoft.Extensions.DependencyInjection;
using SkiServiceWPF.Helpers;
using SkiServiceWPF.ViewModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SkiServiceWPF.Views
{
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();

            var loginViewModel = ((App)Application.Current).ServiceProvider.GetRequiredService<LoginViewModel>();
            loginViewModel.ClearInputs();


            var viewModel = ((App)Application.Current).ServiceProvider.GetRequiredService<LoginViewModel>();
            DataContext = viewModel;

            // Eventhandler für PropertyChanged hinzufügen
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                PasswordHandlingHelper.HandlePasswordChanged((PasswordBox)sender, viewModel);
            }
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LoginViewModel.ErrorMessage) && sender is LoginViewModel viewModel)
            {
                PasswordHandlingHelper.UpdatePasswordBoxBorder(viewModel, passwordBox, usernameTextBox);
            }
        }

        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PasswordHandlingHelper.ShowPassword(passwordBox, passwordTxtBox);
            e.Handled = true;
        }

        private void Button_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            PasswordHandlingHelper.HidePassword(passwordBox, passwordTxtBox);
            e.Handled = true;
        }


    }
}
