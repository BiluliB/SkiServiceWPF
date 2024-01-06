using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkiServiceWPF.Common;
using SkiServiceWPF.Services;
using SkiServiceWPF.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SkiServiceWPF.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        private readonly BackendService _backendService;
        private IConfiguration _configuration;
        private object _previousView;

        /// <summary>
        /// Constructor for DashboardView.
        /// </summary>
        public DashboardView(IConfiguration configuration, BackendService backendService)
        {
            InitializeComponent();

            _backendService = backendService;
            _configuration = configuration;

            // Setup methods for initializing ViewModel and ListView.
            SetupViewModel();
            InitializeListView();
            Unloaded += DashboardView_Unloaded;
        }

        // Event for handling search operations.
        public delegate void SearchEventHandler(string searchText);
        public event SearchEventHandler OnSearch;

        /// <summary>
        /// Click event handler for the Search button.
        /// </summary>        
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            OnSearch?.Invoke(SearchTextBox.Text);
        }

        /// <summary>
        /// Initializes the ListView with the default view.
        /// </summary>
        private void InitializeListView()
        {
            if (_previousView == null)
            {
                var listViewViewModel = new ListViewModel(_backendService);
                var listViewControl = new ListViewUserControl(this) { DataContext = listViewViewModel };
                listViewViewModel.LoadRegistrationsCommand.Execute(null);
                _previousView = listViewControl;
            }
            this.ContentPlaceholder.Content = _previousView;
        }

        /// <summary>
        /// Sets up the ViewModel for the dashboard.
        /// </summary>
        private void SetupViewModel()
        {
            var dashboardViewModel = ((App)Application.Current).ServiceProvider.GetRequiredService<DashboardViewModel>();
            DataContext = dashboardViewModel;

            if (dashboardViewModel is DashboardViewModel viewModel)
            {
                viewModel.RequestEditView += ViewModel_RequestEditView;
                viewModel.OnRequireRefresh += ViewModel_OnRequireRefresh;
            }
        }

        /// <summary>
        /// Event handler for requesting the edit view.
        /// </summary>
        private void ViewModel_RequestEditView(object sender, EventArgs e)
        {
            var editViewControl = new EditViewUserControl(SelectionHelper.Selected);
            this.ContentPlaceholder.Content = editViewControl;
        }

        /// <summary>
        /// Event handler for refreshing the view.
        /// </summary>
        private void ViewModel_OnRequireRefresh()
        {
            InitializeListView();
        }

        /// <summary>
        /// Event handler for when DashboardView is unloaded.
        /// </summary>
        private void DashboardView_Unloaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is DashboardViewModel viewModel)
            {
                viewModel.RequestEditView -= ViewModel_RequestEditView;
                viewModel.OnRequireRefresh -= ViewModel_OnRequireRefresh;
            }
        }

        /// <summary>
        /// Handles double click on a TreeView item.
        /// </summary>
        private void TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is TreeViewItem item && item.Header is string header)
            {
                HandleTreeViewItemSelection(header);
            }
        }

        /// <summary>
        /// Handles the selection of a TreeView item.
        /// </summary>
        private void HandleTreeViewItemSelection(string header)
        {
            var listViewViewModel = new ListViewModel(_backendService);
            ListViewUserControl listViewControl;

            switch (header)
            {
                case "Alle Aufträge":
                    listViewViewModel.LoadRegistrationsCommand.Execute(null);
                    break;
                case "Offene Aufträge":
                    listViewViewModel.LoadOpenRegistrationsCommand.Execute(null);
                    break;
                case "In Arbeit":
                    listViewViewModel.LoadWorkRegistrationsCommand.Execute(null);
                    break;
                case "Abgeschlossene Aufträge":
                    listViewViewModel.LoadDoneRegistrationsCommand.Execute(null);
                    break;
            }

            listViewControl = new ListViewUserControl(this) { DataContext = listViewViewModel };
            this.ContentPlaceholder.Content = listViewControl;
            ResetEditViewActive();
        }

        /// <summary>
        /// Click event handler for returning to the list view.
        /// </summary>
        private void ResetEditViewActive()
        {
            if (DataContext is DashboardViewModel viewModel)
            {
                viewModel.IsEditViewActive = false;
            }
        }

        private void ReturnToListView_Click(object sender, RoutedEventArgs e)
        {
            InitializeListView();
            ResetEditViewActive();
        }
    }
}
