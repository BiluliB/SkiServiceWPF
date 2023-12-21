using SkiServiceWPF.Commands;
using SkiServiceWPF.Models;
using SkiServiceWPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SkiServiceWPF.ViewModels
{
    public class ListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand LoadRegistrationsCommand { get; private set; }
        public ObservableCollection<RegistrationsModel> Registrations { get; private set; }

        private readonly BackendService _backendService;

        public ListViewModel(BackendService backendService)
        {
            _backendService = backendService;
            Registrations = new ObservableCollection<RegistrationsModel>();
            LoadRegistrationsCommand = new RelayCommand(async () => await Load_Registrations());
        }

        private async Task Load_Registrations()
        {
            try
            {
                var registrations = await _backendService.GetRegistrations("https://localhost:7119/Registrations");
                Registrations.Clear();
                foreach (var registration in registrations)
                {
                    Registrations.Add(registration);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
