using SkiServiceWPF.ViewModel;
using System.Windows.Controls;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Windows.Media;

namespace SkiServiceWPF.Views
{
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
            var viewModel = ((App)Application.Current).ServiceProvider.GetRequiredService<LoginViewModel>();
            DataContext = viewModel;

            // Eventhandler für PropertyChanged hinzufügen
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.Password = ((PasswordBox)sender).Password;
            }
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LoginViewModel.ErrorMessage))
            {
                var viewModel = sender as LoginViewModel;
                if (viewModel.ErrorMessage == "User not found")
                {
                    // Setzen des BorderBrush auf Rot
                    passwordBox.BorderBrush = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    // Zurücksetzen auf Standardfarbe
                    passwordBox.BorderBrush = SystemColors.ControlDarkBrush;
                }
            }
        }
    }
}
