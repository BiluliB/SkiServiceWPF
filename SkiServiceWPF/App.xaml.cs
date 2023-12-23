using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkiServiceWPF.Interfaces;
using SkiServiceWPF.Services;
using SkiServiceWPF.ViewModel;
using SkiServiceWPF.ViewModels;
using SkiServiceWPF.Views;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace SkiServiceWPF
{
    /// <summary>
    /// Main application class with startup and configuration logic
    /// </summary>
    public partial class App : Application
    {
        public ServiceProvider ServiceProvider { get; private set; }
        public static IConfiguration Configuration { get; private set; }

        /// <summary>
        /// Handles the application startup process
        /// </summary>
        /// <param name="e">Startup event arguments</param>
        #region OnStartup
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Create configuration
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Configure DI container
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            var navigationService = ServiceProvider.GetRequiredService<INavigationService>() as NavigationService;
            navigationService.SetMainFrame(mainWindow.MainContentFrame);
            navigationService.NavigateTo("Login");
        }
        #endregion

        /// <summary>
        /// Configures services for dependency injection
        /// </summary>
        /// <param name="services">Service collection for DI</param>
        #region ConfigureServices
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);

            // Configure HttpClient for BackendService
            services.AddHttpClient<BackendService>(client =>
            {
                string baseUrl = Configuration["ApiSettings:BaseUrl"];
                client.BaseAddress = new Uri(baseUrl);
            });

            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<BackendService>();
            services.AddSingleton<ListViewModel>();
            services.AddSingleton<DashboardViewModel>();


            services.AddSingleton<INavigationService>(provider =>
            {
                var navigationService = new NavigationService();
                navigationService.RegisterView("Login", () => new LoginView());
                navigationService.RegisterView("Dashboard", () => new DashboardView(provider.GetRequiredService<BackendService>()));
                navigationService.RegisterView("LoginView", () => new LoginView());

                return navigationService;
            });

            // Register MainWindow for application
            services.AddSingleton<MainWindow>();
        }
        #endregion
    }
}
