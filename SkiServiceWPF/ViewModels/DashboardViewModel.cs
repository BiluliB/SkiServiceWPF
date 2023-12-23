using System.ComponentModel;
using System.Windows;
using SkiServiceWPF.Commands;
using SkiServiceWPF.Interfaces;
using SkiServiceWPF.Services;
using System.Threading.Tasks;

public class DashboardViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public AsyncRelayCommand LogoutCommand { get; }

    private readonly INavigationService _navigationService;

    /// <summary>
    /// Benachrichtigt Abonnenten über Änderungen an Eigenschaften.
    /// </summary>
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Konstruktor für DashboardViewModel.
    /// </summary>
    /// <param name="navigationService">Der Navigationsservice zur Steuerung der Ansichten.</param>
    public DashboardViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        LogoutCommand = new AsyncRelayCommand(ExecuteLogout, CanExecuteLogout);
    }

    /// <summary>
    /// Bestimmt, ob der Logout-Befehl ausgeführt werden kann.
    /// </summary>
    private bool CanExecuteLogout()
    {
        // Hier können Sie Bedingungen prüfen, wann der Logout möglich ist
        return true;
    }

    /// <summary>
    /// Führt den Logout-Prozess aus.
    /// </summary>
    private async Task ExecuteLogout()
    {
        // Bestätigungsdialog
        MessageBoxResult result = MessageBox.Show("Möchten Sie sich wirklich abmelden?", "Abmeldung", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            // Führen Sie hier den Logout-Prozess durch (z.B. Token löschen, Session beenden)
            // Navigieren zur LoginView
            _navigationService.NavigateTo("LoginView");
        }
    }
}
