﻿using SkiServiceWPF.Services;
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
            this.ContentPlaceholder.Content = new EditViewUserControl(SelectionHelper.Selected);
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
            if (e.Source is TreeViewItem item && item.Header is string header)
            {
                // Deklarieren Sie die Variablen einmal außerhalb der if-Anweisungen
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

                // Erstellen des ListViewUserControls
                listViewControl = new ListViewUserControl
                {
                    DataContext = listViewViewModel
                };

                this.ContentPlaceholder.Content = listViewControl;
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var editViewControl = new EditViewUserControl(SelectionHelper.Selected);
            this.ContentPlaceholder.Content = editViewControl;
        }
    }
}
