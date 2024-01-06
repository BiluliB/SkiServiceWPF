using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SkiServiceWPF.Models
{
    /// <summary>
    /// Represents a list entry with property change notification.
    /// </summary>
    public class ListEntry : INotifyPropertyChanged
    {
        private readonly RegistrationModel _registration;
      public ListEntry(RegistrationModel registration)
        {
            _registration = registration;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void SetProperty<T>(ref T backingStore, T value, string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
