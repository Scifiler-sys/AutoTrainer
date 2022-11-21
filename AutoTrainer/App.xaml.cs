using AutoTrainer.DL;
using AutoTrainer.Models;
using AutoTrainer.Selenium;
using AutoTrainer.Services;
using AutoTrainer.Stores;
using AutoTrainer.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace AutoTrainer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// 
    /// This became our main hub that does dependency managing for our entire app
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _provider;

        public App()
        {
            //Object that holds all the services we need for this application
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<HttpClient>();
            services.AddSingleton<BatchRepository>();
            services.AddSingleton<BatchStore>();
            services.AddSingleton<NavigationStore>();
            services.AddSingleton<EmailBot>();

            services.AddSingleton<RevProService>();

            services.AddSingleton<BatchViewModel>(s => new BatchViewModel(s.GetRequiredService<BatchRepository>().Load()));
            services.AddSingleton<MainViewModel>(s => new MainViewModel(s.GetRequiredService<NavigationStore>(),s));
            services.AddSingleton<MainWindow>(s => new MainWindow() { DataContext = s.GetRequiredService<MainViewModel>() });

            services.AddTransient<ManageBatchViewModel>();
            services.AddTransient<SettingsViewModel>();

            _provider = services.BuildServiceProvider();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            //Setting default/custom data for our stores
            _provider.GetService<NavigationStore>().CurrentViewModel = _provider.GetRequiredService<ManageBatchViewModel>();
            _provider.GetService<BatchStore>().CurrentBatch = _provider.GetRequiredService<BatchViewModel>();

            MainWindow = _provider.GetRequiredService<MainWindow>();

            MainWindow.Show();

            base.OnStartup(e);
        }


    }
}
