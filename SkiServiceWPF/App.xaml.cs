using Microsoft.Extensions.DependencyInjection;
using SkiServiceWPF.Services;
using SkiServiceWPF.Views;
using SkiServiceWPF.ViewModel;
using System.Windows;
using SkiServiceWPF.Interfaces;
using System;

namespace SkiServiceWPF
{
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;

        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            var navigationService = _serviceProvider.GetRequiredService<INavigationService>() as NavigationService;
            navigationService.SetMainFrame(mainWindow.MainContentFrame);
            navigationService.NavigateTo("Login"); // Hier wird LoginView im Frame angezeigt
        }


        private void ConfigureServices(IServiceCollection services)
        {
            
            services.AddSingleton<INavigationService>(provider =>
            {
                var navigationService = new NavigationService();
                // Hier Views registrieren
                navigationService.RegisterView("Login", () => new LoginView());
                navigationService.RegisterView("Dashboard", () => new DashboardView());
                return navigationService;
            });

            services.AddSingleton<MainWindow>(); // MainWindow als Singleton registrieren
                                                 
        }

    }
}
