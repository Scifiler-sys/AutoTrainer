using AutoTrainer.Selenium;
using AutoTrainer.Services;
using AutoTrainer.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrainer.ViewModels
{
    public enum ViewModelType
    {
        ManageBatch,
        Settings,
        HomeView
    }

    public class FactoryViewModel
    {
        private readonly RevProService _revProService;
        private readonly BatchStore _batchStore;
        private readonly NavigationStore _navigationStore;
        private readonly EmailBot _emailBot;


        public FactoryViewModel(RevProService revProService, BatchStore batchStore, EmailBot emailBot, NavigationStore navigationStore)
        {
            _revProService = revProService;
            _batchStore = batchStore;
            _emailBot = emailBot;
            _navigationStore = navigationStore;
        }

        public ViewModelBase GetViewModel(ViewModelType type)
        {
            switch (type)
            {
                case ViewModelType.ManageBatch:
                    return new ManageBatchViewModel(_revProService,_batchStore, _emailBot);
                case ViewModelType.Settings:
                    return new SettingsViewModel();
                default:
                    return new MainViewModel(_navigationStore, CreateFactory());
            }
        }

        private FactoryViewModel CreateFactory()
        {
            return new FactoryViewModel(_revProService, _batchStore, _emailBot,_navigationStore);
        }
    }
}
