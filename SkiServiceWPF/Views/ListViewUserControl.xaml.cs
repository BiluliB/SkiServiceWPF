using SkiServiceWPF.ViewModels;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                Debug.WriteLine($"Button clicked. CommandParameter: {button.CommandParameter}");
            }
        }
    }
}
