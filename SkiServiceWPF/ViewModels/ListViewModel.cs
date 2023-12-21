using SkiServiceWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiServiceWPF.ViewModels
{
    public class ListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<RegistrationsModel> Registrations { get; private set; }
        public ListViewModel() 
        {
            Registrations = new ObservableCollection<RegistrationsModel>();
            Load_Registrations();
        }

        private void Load_Registrations()
        {

        }
    }
}
