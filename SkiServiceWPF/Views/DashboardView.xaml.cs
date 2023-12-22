using SkiServiceWPF.Services;
using SkiServiceWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
