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
using System.Collections.ObjectModel;
using SkiServiceWPF.Services;


namespace SkiServiceWPF.Views
{
    public partial class ListViewUserControl : UserControl
    {
        private DashboardView _dashboardView;
        private ObservableCollection<RegistrationModel> _originalItems;

        public ListViewUserControl(DashboardView dashboardView)
        {
            InitializeComponent();
            this.Unloaded += ListViewUserControl_Unloaded;

            _dashboardView = dashboardView;
            _dashboardView.OnSearch += DashboardView_OnSearch;
        }


        public void LoadItems(ObservableCollection<RegistrationModel> items)
        {
            _originalItems = new ObservableCollection<RegistrationModel>(items);
            DataGrid.ItemsSource = _originalItems;
        }


        private void ListViewUserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_dashboardView != null)
            {
                _dashboardView.OnSearch -= DashboardView_OnSearch;
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           var grid = sender as DataGrid;
            var selectedItem = grid.SelectedItem as RegistrationModel;
            SelectionHelper.Selected = selectedItem;
        }

        private void DashboardView_OnSearch(string searchText)
        {
            FilterItems(searchText);
        }
        

        public void FilterItems(string searchText)
        {
            if (_originalItems == null || _originalItems.Count == 0)
            {
                // _originalItems ist null oder leer, brechen Sie die Methode ab.
                return;
            }

            if (string.IsNullOrEmpty(searchText))
            {
                DataGrid.ItemsSource = _originalItems;
                return;
            }

            searchText = searchText.ToLower();
            var filteredItems = new ObservableCollection<RegistrationModel>(
                _originalItems.Where(item =>
                    (item.FirstName?.ToLower().Contains(searchText) ?? false) ||
                    (item.LastName?.ToLower().Contains(searchText) ?? false) ||
                    (item.PickupDate?.ToLower().Contains(searchText) ?? false) ||
                    (item.Priority?.ToLower().Contains(searchText) ?? false) ||
                    (item.Service?.ToLower().Contains(searchText) ?? false) ||
                    (item.Status?.ToLower().Contains(searchText) ?? false)));

            DataGrid.ItemsSource = filteredItems;
        }
    }
}
