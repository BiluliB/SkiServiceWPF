using SkiServiceWPF.Commands;
using SkiServiceWPF.Views;
using System.Windows;
using System.Windows.Input;

namespace SkiServiceWPF.ViewModel
{
    public class LoginViewModel
    {
        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }

        private void ExecuteLogin()
        {
            Application.Current.Dispatcher.Invoke(NavigateToDashboard);
        }

        private bool CanExecuteLogin()
        {
            return true; 
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
    }
}
