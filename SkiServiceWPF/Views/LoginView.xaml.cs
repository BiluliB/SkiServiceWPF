using SkiServiceWPF.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SkiServiceWPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text == "Benutzername")
            {
                tb.Text = string.Empty;
                tb.Foreground = Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Benutzername";
                tb.Foreground = Brushes.Gray;
            }
        }


        private void txtPlaceholder_GotFocus(object sender, RoutedEventArgs e)
        {
            // Hide the placeholder TextBox and show the PasswordBox when the TextBox gets focus
            txtPlaceholder.Visibility = Visibility.Hidden;
            pwdBox.Visibility = Visibility.Visible;
            pwdBox.Focus();
        }

        private void pwdBox_LostFocus(object sender, RoutedEventArgs e)
        {
            // Show the placeholder TextBox and hide the PasswordBox when the PasswordBox loses focus and has no password entered
            if (String.IsNullOrEmpty(pwdBox.Password))
            {
                pwdBox.Visibility = Visibility.Hidden;
                txtPlaceholder.Visibility = Visibility.Visible;
            }
        }






    }
}