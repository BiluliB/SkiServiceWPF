using SkiServiceWPF.Models;
using SkiServiceWPF.ViewModels;
using System.Windows.Controls;

namespace SkiServiceWPF.Views
{
    /// <summary>
    /// Interaction logic for DetailViewUserControl.xaml
    /// </summary>
    public partial class EditViewUserControl : UserControl
    { 
        public EditViewUserControl(RegistrationModel registrationModel)
        {
            InitializeComponent();

            DataContext = new EditViewUserControlModel(registrationModel);
        }
    }
}
