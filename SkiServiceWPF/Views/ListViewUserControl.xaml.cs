using SkiServiceWPF.ViewModels;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;
using SkiServiceWPF.Models;
using SkiServiceWPF.Common;


namespace SkiServiceWPF.Views
{
    /// <summary>
    /// Interaction logic for ListViewUserControl.xaml
    /// </summary>
    public partial class ListViewUserControl : UserControl
    {
        public ListViewUserControl()
        {
            InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           var grid = sender as DataGrid;
            var selectedItem = grid.SelectedItem as RegistrationModel;
            SelectionHelper.Selected = selectedItem;
        }
    }
}
