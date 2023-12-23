using SkiServiceWPF.Services;
using SkiServiceWPF.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Diagnostics;

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
        }

        private void TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
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
