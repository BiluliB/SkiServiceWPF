﻿using Microsoft.Extensions.DependencyInjection;
using SkiServiceWPF.Interfaces;
using SkiServiceWPF.Services;
using SkiServiceWPF.Views;
using System.Windows;
using Microsoft.Extensions.Configuration;
using System.IO;
using SkiServiceWPF.Common;
using SkiServiceWPF.ViewModel;

namespace SkiServiceWPF
{
    public partial class App : Application
    {
        public ServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
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
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false, reloadOnChange: true)
                .Build();

            var apiSettings = configuration.GetSection("ApiSettings").Get<ApiSettings>();
            services.AddSingleton(apiSettings);

            services.AddHttpClient<UserLoginApi>();
            services.AddSingleton<UserLoginApi>();
            services.AddSingleton<UserCreationApi>();
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
