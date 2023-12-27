using SkiServiceWPF.Services;
using SkiServiceWPF.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Diagnostics;
using SkiServiceWPF.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using SkiServiceWPF.Common;

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
  
        public DashboardView(IConfiguration configuration, BackendService backendService)
        {
            InitializeComponent();
            _backendService = backendService;
            _configuration = configuration;

            // ViewModel aus dem DI-Container holen
            var dashboardViewModel = ((App)Application.Current).ServiceProvider.GetRequiredService<DashboardViewModel>();
            DataContext = dashboardViewModel;

            // Ereignis abonnieren
            if (dashboardViewModel is DashboardViewModel viewModel)
            {
                viewModel.RequestEditView += ViewModel_RequestEditView;
                viewModel.OnRequireRefresh += ViewModel_OnRequireRefresh;
            }

            // ListView initialisieren
            InitializeListView();
            Unloaded += DashboardView_Unloaded;
        }

        private void ViewModel_OnRequireRefresh()
        {
            InitializeListView();
        }

        private void InitializeListView()
        {
            if (_previousView == null)
            {
                var listViewViewModel = new ListViewModel(_backendService);
                var listViewControl = new ListViewUserControl { DataContext = listViewViewModel };
                listViewViewModel.LoadRegistrationsCommand.Execute(null);
                _previousView = listViewControl;
            }
            this.ContentPlaceholder.Content = _previousView;
        }

        private void ViewModel_RequestEditView(object sender, EventArgs e)
        {
            var editViewControl = new EditViewUserControl(SelectionHelper.Selected);
            this.ContentPlaceholder.Content = editViewControl;
        }

        private void DashboardView_Unloaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is DashboardViewModel viewModel)
            {
                viewModel.RequestEditView -= ViewModel_RequestEditView;
                viewModel.OnRequireRefresh -= ViewModel_OnRequireRefresh;
            }
        }

        private void TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is TreeViewItem item && item.Header is string header)
            {
                var listViewViewModel = new ListViewModel(_backendService);
                ListViewUserControl listViewControl;

                if (header == "Alle Aufträge")
                {
                    listViewViewModel.LoadRegistrationsCommand.Execute(null);
                }
                else if (header == "Offene Aufträge")
                {
                    listViewViewModel.LoadOpenRegistrationsCommand.Execute(null);
                }
                else if (header == "In Arbeit")
                {
                    listViewViewModel.LoadWorkRegistrationsCommand.Execute(null);
                }
                else if (header == "Abgeschlossene Aufträge")
                {
                    listViewViewModel.LoadDoneRegistrationsCommand.Execute(null);
                }

                listViewControl = new ListViewUserControl
                {
                    DataContext = listViewViewModel
                };

                this.ContentPlaceholder.Content = listViewControl;

                // Setzen von IsEditViewActive auf false im ViewModel
                if (DataContext is DashboardViewModel viewModel)
                {
                    viewModel.IsEditViewActive = false;
                }
            }
        }


        private void ReturnToListView_Click(object sender, RoutedEventArgs e)
        {
            InitializeListView();

            if (DataContext is DashboardViewModel viewModel)
            {
                viewModel.IsEditViewActive = false;
            }
        }
    }
}