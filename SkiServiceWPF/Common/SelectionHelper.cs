using SkiServiceWPF.Models;
using System.ComponentModel;

namespace SkiServiceWPF.Common
{
    /// <summary>
    /// Helper class for managing selected item with property change notification.
    /// </summary>
    public class SelectionHelper : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public static RegistrationModel Selected {  get; set; }
    }
}
