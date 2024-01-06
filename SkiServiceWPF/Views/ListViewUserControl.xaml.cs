using SkiServiceWPF.Common;
using SkiServiceWPF.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;


namespace SkiServiceWPF.Views
{
    /// <summary>
    /// Interaction logic for ListViewUserControl.xaml
    /// </summary>
    public partial class ListViewUserControl : UserControl
    {
        private DashboardView _dashboardView;

        /// <summary>
        /// Initializes the UserControl and subscribes to the OnSearch event of the DashboardView.
        /// </summary>
        /// <param name="dashboardView">DashboardView to interact with.</param>
        public ListViewUserControl(DashboardView dashboardView)
        {
            InitializeComponent();
            this.Unloaded += ListViewUserControl_Unloaded;

            _dashboardView = dashboardView;
            _dashboardView.OnSearch += DashboardView_OnSearch;
        }

        /// <summary>
        /// Handles the UserControl's Unloaded event, unsubscribing from the OnSearch event.
        /// </summary>
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

        /// <summary>
        /// Filters the DataGrid items based on the provided search text.
        /// </summary>
        /// <param name="searchText">Text used for filtering the items.</param>
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

        /// <summary>
        /// Updates the selected item in the DataGrid when the selection changes.
        /// </summary>
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           var grid = sender as DataGrid;
            var selectedItem = grid.SelectedItem as RegistrationModel;
            SelectionHelper.Selected = selectedItem;
        }
    }
}
