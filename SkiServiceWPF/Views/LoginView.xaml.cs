using SkiServiceWPF.ViewModel;
using System.Windows.Controls;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace SkiServiceWPF.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
            // Hier wird das LoginViewModel über DI abgerufen
            DataContext = ((App)Application.Current).ServiceProvider.GetRequiredService<LoginViewModel>();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                // Setze das Passwort im ViewModel, wenn es sich ändert
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password;
            }
        }
    }
}
