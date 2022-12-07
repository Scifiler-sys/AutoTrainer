using AutoTrainer.DL;
using AutoTrainer.Selenium;
using AutoTrainer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoTrainer.Commands
{
    public class StartStandupBotCommand : CommandBase
    {
        private readonly SendStandupViewModel _sendStandupViewModel;
        private readonly StandupRepository _standupRepository;
        private readonly StandupBot _standupBot;

        public StartStandupBotCommand(SendStandupViewModel sendStandupViewModel, StandupRepository standupRepository, StandupBot standupBot)
        {
            _sendStandupViewModel = sendStandupViewModel;
            _standupRepository = standupRepository;
            _standupBot = standupBot;
        }

        public override void Execute(object parameter)
        {
            _standupRepository.Save(new Models.Standup(_sendStandupViewModel.CurrentStandup));

            if(MessageBox.Show("You sure you want to start the bot?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                _standupBot.SendStandupNormal();
            }
        }
    }
}
