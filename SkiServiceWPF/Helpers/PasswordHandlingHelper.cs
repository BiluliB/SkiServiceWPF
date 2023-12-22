using SkiServiceWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace SkiServiceWPF.Helpers
{
    public class PasswordHandlingHelper
    {
        public static void HandlePasswordChanged(PasswordBox passwordBox, LoginViewModel viewModel)
        {
            viewModel.Password = passwordBox.Password;
        }

        public static void UpdatePasswordBoxBorder(LoginViewModel viewModel, PasswordBox passwordBox, TextBox usernameTextBox)
        {
            if (viewModel.ErrorMessage == "User not found" ||
                viewModel.ErrorMessage == "Your account is locked. Please contact administrator.")
            {
                passwordBox.BorderBrush = new SolidColorBrush(Colors.Red);
                usernameTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else if (viewModel.ErrorMessage == "Invalid password!! 2 attempts left." ||
                     viewModel.ErrorMessage == "Invalid password!! 1 attempts left.")
            {
                passwordBox.BorderBrush = new SolidColorBrush(Colors.Red);
                usernameTextBox.BorderBrush = SystemColors.ControlDarkBrush;
            }
            else
            {
                passwordBox.BorderBrush = SystemColors.ControlDarkBrush;
                usernameTextBox.BorderBrush = SystemColors.ControlDarkBrush;
            }
        }




        public static void ShowPassword(PasswordBox passwordBox, TextBox textBox)
        {
            textBox.Text = passwordBox.Password;
            passwordBox.Visibility = Visibility.Collapsed;
            textBox.Visibility = Visibility.Visible;
        }

        public static void HidePassword(PasswordBox passwordBox, TextBox textBox)
        {
            passwordBox.Visibility = Visibility.Visible;
            textBox.Visibility = Visibility.Collapsed;
        }
    }
}

