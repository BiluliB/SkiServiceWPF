using SkiServiceWPF.Commands;
using SkiServiceWPF.Common;
using SkiServiceWPF.Models;
using System.ComponentModel;
using System.Windows.Input;

public class UserCreationViewModel : INotifyPropertyChanged
{
    private readonly UserCreationApi _userCreationApi;
    private string _userName;
    private string _password;

    public string UserName
    {
        get => _userName;
        set
        {
            _userName = value;
            OnPropertyChanged(nameof(UserName));
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    public ICommand CreateUserCommand { get; private set; }

    public UserCreationViewModel(UserCreationApi userCreationApi)
    {
        _userCreationApi = userCreationApi;
        CreateUserCommand = new RelayCommand(async () => await ExecuteCreateUser(), CanExecuteCreateUser);
    }

    private async Task ExecuteCreateUser()
    {
        var userModel = new UserModel { UserName = this.UserName, Password = this.Password };
        try
        {
            var result = await _userCreationApi.CreateUserAsync(userModel);
            // Erfolgsmeldung oder Weiterleitung
        }
        catch (Exception ex)
        {
            // Fehlerbehandlung
        }
    }

    private bool CanExecuteCreateUser()
    {
        return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
