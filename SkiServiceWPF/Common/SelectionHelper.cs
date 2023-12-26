using SkiServiceWPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiServiceWPF.Common
{
    public class SelectionHelper : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public static RegistrationModel Selected {  get; set; }

    }
}
