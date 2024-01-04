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


namespace SkiServiceWPF.Views
{
    /// <summary>
    /// Interaction logic for ListViewUserControl.xaml
    /// </summary>
    public partial class ListViewUserControl : UserControl
    {
        private DashboardView _dashboardView;

        public ListViewUserControl(DashboardView dashboardView)
        {
            InitializeComponent();
            this.Unloaded += ListViewUserControl_Unloaded;

            _dashboardView = dashboardView;
            _dashboardView.OnSearch += DashboardView_OnSearch;
        }

        private void ListViewUserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_dashboardView != null)
            {
                _dashboardView.OnSearch -= DashboardView_OnSearch;
            }
        }

        private void DashboardView_OnSearch(string searchText)
        {
            FilterItems(searchText);
        }

        public void FilterItems(string searchText)
        {
            var listViewItems = DataGrid.ItemsSource as ObservableCollection<RegistrationModel>;
            if (listViewItems == null) return;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                DataGrid.ItemsSource = listViewItems;
                return;
            }

            searchText = searchText.ToLower();
            var filteredItems = new ObservableCollection<RegistrationModel>(
                listViewItems.Where(item =>
                    item.FirstName.ToLower().Contains(searchText) ||
                    item.LastName.ToLower().Contains(searchText) ||
                    item.PickupDate.ToLower().Contains(searchText) ||
                    item.Priority.ToLower().Contains(searchText) ||
                    item.Service.ToLower().Contains(searchText) ||
                    item.Status.ToLower().Contains(searchText)));

            DataGrid.ItemsSource = filteredItems;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           var grid = sender as DataGrid;
            var selectedItem = grid.SelectedItem as RegistrationModel;
            SelectionHelper.Selected = selectedItem;
        }
    }
}
