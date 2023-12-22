using SkiServiceWPF.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SkiServiceWPF.Helpers
{
    /// <summary>
    /// Represents a helper class for password handling
    /// </summary>
    public class PasswordHandlingHelper
    {
        /// <summary>
        /// Updates the password property in the view model when the password in the PasswordBox changes
        /// </summary>
        /// <param name="passwordBox">The password input field</param>
        /// <param name="viewModel">The view model containing the password property</param>
        #region HandlePasswordChanged
        public static void HandlePasswordChanged(PasswordBox passwordBox, LoginViewModel viewModel)
        {
            viewModel.Password = passwordBox.Password;
        }
        #endregion

        /// <summary>
        /// Updates the border color of the password box and username text box based on login error messages
        /// </summary>
        /// <param name="viewModel">The view model containing the error message</param>
        /// <param name="passwordBox">The password input field</param>
        /// <param name="usernameTextBox">The username input field</param>
        #region UpdatePasswordBoxBorder
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
        #endregion

        /// <summary>
        /// Shows the password in a text box and hides the password box
        /// </summary>
        /// <param name="passwordBox">The password input field</param>
        /// <param name="textBox">The text box to display the password</param>
        #region ShowPassword
        public static void ShowPassword(PasswordBox passwordBox, TextBox textBox)
        {
            textBox.Text = passwordBox.Password;
            passwordBox.Visibility = Visibility.Collapsed;
            textBox.Visibility = Visibility.Visible;
        }
        #endregion

        /// <summary>
        /// Hides the text box and shows the password in the password box
        /// </summary>
        /// <param name="passwordBox">The password input field</param>
        /// <param name="textBox">The text box displaying the password</param>
        #region HidePassword
        public static void HidePassword(PasswordBox passwordBox, TextBox textBox)
        {
            passwordBox.Visibility = Visibility.Visible;
            textBox.Visibility = Visibility.Collapsed;
        }
        #endregion
    }
}
