using AutoTrainer.DL;
using AutoTrainer.Models;
using AutoTrainer.Services;
using AutoTrainer.Stores;
using AutoTrainer.ViewModels;
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
        private readonly NavigationStore _navigationStore;
        private readonly BatchStore _batchStore;
        private readonly RevProService _revProService;
        private readonly HttpClient _httpClient;
        private readonly BatchRepository _batchRepository;

        public App()
        {
            //Currently where most of our creation logic lies
            _httpClient = new HttpClient();
            _batchRepository = new BatchRepository();
            _revProService = new RevProService(_httpClient, _batchRepository);


            _navigationStore = new NavigationStore();
            _batchStore = new BatchStore();
            
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            //Setting default/custom data for our stores
            _navigationStore.CurrentViewModel = new ManageBatchViewModel(_revProService, _batchStore);
            _batchStore.CurrentBatch = new BatchViewModel(_batchRepository.Load());

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
