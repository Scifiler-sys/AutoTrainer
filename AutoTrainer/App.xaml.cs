﻿using AutoTrainer.DL;
using AutoTrainer.Models;
using AutoTrainer.Selenium;
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
        private readonly EmailBot _emailBot;
        private readonly HttpClient _httpClient;
        private readonly BatchRepository _batchRepository;
        private readonly FactoryViewModel _factoryViewModel;

        public App()
        {
            //Currently where most of our creation logic lies
            _httpClient = new HttpClient();
            _batchRepository = new BatchRepository();
            _revProService = new RevProService(_httpClient, _batchRepository);
            _emailBot = new EmailBot();


            _navigationStore = new NavigationStore();
            _batchStore = new BatchStore();

            _factoryViewModel = new FactoryViewModel(_revProService, _batchStore, _emailBot, _navigationStore);
            
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            //Setting default/custom data for our stores
            _navigationStore.CurrentViewModel = _factoryViewModel.GetViewModel(ViewModelType.ManageBatch);
            _batchStore.CurrentBatch = new BatchViewModel(_batchRepository.Load());

            MainWindow = new MainWindow()
            {
                DataContext = _factoryViewModel.GetViewModel(ViewModelType.HomeView)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }


    }
}
