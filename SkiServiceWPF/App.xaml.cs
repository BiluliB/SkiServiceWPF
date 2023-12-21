using Microsoft.Extensions.DependencyInjection;
using SkiServiceWPF.Interfaces;
using SkiServiceWPF.Services;
using SkiServiceWPF.Views;
using System.Windows;
using Microsoft.Extensions.Configuration;
using System.IO;
using SkiServiceWPF.ViewModel;
using SkiServiceWPF.Services;
using System.Net.Http;

namespace SkiServiceWPF
{
    public partial class App : Application
    {
        public static HttpClient HttpClient { get; private set; }
        public ServiceProvider ServiceProvider { get; private set; }

        public static IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            HttpClient = new HttpClient();
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            // Endpoint setter
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            var navigationService = ServiceProvider.GetRequiredService<INavigationService>() as NavigationService;
            navigationService.SetMainFrame(mainWindow.MainContentFrame);
            navigationService.NavigateTo("Login");
        }

        private void ConfigureServices(IServiceCollection services)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            services.AddSingleton<IConfiguration>(Configuration);

            // Konfigurieren des HttpClient für BackendService
            services.AddHttpClient<BackendService>(client =>
            {
                // Hier wird die Basis-URL aus der Konfiguration gesetzt
                string baseUrl = Configuration["ApiSettings:BaseUrl"];
                client.BaseAddress = new Uri(baseUrl);
            });

            services.AddSingleton<BackendService>();
            services.AddSingleton<LoginViewModel>();

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
