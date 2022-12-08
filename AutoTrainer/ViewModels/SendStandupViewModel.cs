using AutoTrainer.Commands;
using AutoTrainer.DL;
using AutoTrainer.Models;
using AutoTrainer.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AutoTrainer.ViewModels
{
    public class SendStandupViewModel : ViewModelBase
    {
        private StandupViewModel _currentStandup;
        private readonly StandupRepository _standupRepository;
                
        public StandupViewModel CurrentStandup
        {
            get { return _currentStandup; }
            set 
            { 
                _currentStandup = value;
                OnPropertyChanged(nameof(CurrentStandup));
            }
        }

        public ICommand StartStandupBotCommand { get; set; }

        public SendStandupViewModel(StandupRepository repository, StandupBot standupBot)
        {
            _standupRepository = repository;
            CurrentStandup = new StandupViewModel(repository.Load());
            StartStandupBotCommand = new StartStandupBotCommand(this, repository, standupBot);
        }
    }
}
