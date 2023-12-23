using SkiServiceWPF.Services;
using SkiServiceWPF.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Diagnostics;
using SkiServiceWPF.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace SkiServiceWPF.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        private readonly BackendService _backendService;

        public DashboardView(BackendService backendService)
        {
            InitializeComponent();
            _backendService = backendService;

            // ViewModel aus dem DI-Container holen
            var dashboardViewModel = ((App)Application.Current).ServiceProvider.GetRequiredService<DashboardViewModel>();
            DataContext = dashboardViewModel;

            // Ereignis abonnieren
            if (dashboardViewModel is DashboardViewModel viewModel)
            {
                viewModel.RequestEditView += ViewModel_RequestEditView;
            }

            // ListView initialisieren
            var listViewViewModel = new ListViewModel(_backendService);
            var listViewControl = new ListViewUserControl
            {
                DataContext = listViewViewModel
            };
            this.ContentPlaceholder.Content = listViewControl;
            listViewViewModel.LoadRegistrationsCommand.Execute(null);

            // Unloaded Event hinzufügen
            Unloaded += DashboardView_Unloaded;
        }

        private void ViewModel_RequestEditView(object sender, EventArgs e)
        {
            // ContentPlaceholder aktualisieren
            this.ContentPlaceholder.Content = new EditViewUserControl();
        }

        private void DashboardView_Unloaded(object sender, RoutedEventArgs e)
        {
            // Ereignis abbestellen, um Speicherlecks zu vermeiden
            if (DataContext is DashboardViewModel viewModel)
            {
                viewModel.RequestEditView -= ViewModel_RequestEditView;
            }
        }

        private void TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Ihre vorhandene Logik
            if (e.Source is TreeViewItem item && item.Header is string header)
            {
                if (header == "Alle Aufträge")
                {
                    var listViewViewModel = new ListViewModel(_backendService);
                    var listViewControl = new ListViewUserControl
                    {
                        DataContext = listViewViewModel
                    };
                    this.ContentPlaceholder.Content = listViewControl;
                    listViewViewModel.LoadRegistrationsCommand.Execute(null);
                }
            }
        }
    }
}
