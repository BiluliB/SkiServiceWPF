using Microsoft.Extensions.DependencyInjection;
using SkiServiceWPF.Interfaces;
using SkiServiceWPF.Services;
using SkiServiceWPF.Views;
using System.Windows;
using Microsoft.Extensions.Configuration;
using System.IO;
using SkiServiceWPF.ViewModel;

namespace SkiServiceWPF
{
    public partial class App : Application
    {
        public ServiceProvider ServiceProvider { get; private set; }
        public static IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Konfiguration erstellen
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // DI-Container konfigurieren
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            var navigationService = ServiceProvider.GetRequiredService<INavigationService>() as NavigationService;
            navigationService.SetMainFrame(mainWindow.MainContentFrame);
            navigationService.NavigateTo("Login");
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);

            // Konfigurieren des HttpClient für BackendService
            services.AddHttpClient<BackendService>(client =>
            {
                string baseUrl = Configuration["ApiSettings:BaseUrl"];
                client.BaseAddress = new Uri(baseUrl);
            });

            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<BackendService>();

            services.AddSingleton<INavigationService>(provider =>
            {
                var navigationService = new NavigationService();
                navigationService.RegisterView("Login", () => new LoginView());
                navigationService.RegisterView("Dashboard", () => new DashboardView(provider.GetRequiredService<BackendService>()));
                return navigationService;
            });

            services.AddSingleton<MainWindow>();
        }
    }
}
