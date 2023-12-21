using Microsoft.Extensions.DependencyInjection;
using SkiServiceWPF.Interfaces;
using SkiServiceWPF.Services;
using SkiServiceWPF.Views;
using System.Windows;

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
            navigationService.NavigateTo("Login"); 
        }


        private void ConfigureServices(IServiceCollection services)
        {
            
            services.AddSingleton<INavigationService>(provider =>
            {
                var navigationService = new NavigationService();
                
                navigationService.RegisterView("Login", () => new LoginView());
                navigationService.RegisterView("Dashboard", () => new DashboardView());
                return navigationService;
            });

            services.AddSingleton<MainWindow>();
                                                 
        }

    }
}
