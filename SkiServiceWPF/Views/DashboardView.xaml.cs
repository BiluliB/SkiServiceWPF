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

namespace SkiServiceWPF.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        private readonly BackendService _backendService;
        private IConfiguration _configuration;

        public DashboardView(IConfiguration configuration, BackendService backendService)
        {
            InitializeComponent();
            _backendService = backendService;
            _configuration = configuration;

            var dashboardViewModel = ((App)Application.Current).ServiceProvider.GetRequiredService<DashboardViewModel>();
            DataContext = dashboardViewModel;

            var listViewViewModel = new ListViewModel(_configuration, _backendService);
            var listViewControl = new ListViewUserControl
            {
                DataContext = listViewViewModel
            };

            this.ContentPlaceholder.Content = listViewControl;
        }

        private void TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is TreeViewItem item && item.Header is string header)
            {
                var listViewViewModel = new ListViewModel(_configuration, _backendService);

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

                var listViewControl = new ListViewUserControl
                {
                    DataContext = listViewViewModel
                };

                this.ContentPlaceholder.Content = listViewControl;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var editViewControl = new EditViewUserControl();
            this.ContentPlaceholder.Content = editViewControl;
        }
    }
}
