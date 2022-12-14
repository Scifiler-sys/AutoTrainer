
using AutoTrainer.Selenium;
using AutoTrainer.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AutoTrainer.Commands
{
    public class SubmitRevProCommand : CommandBase
    {
        private readonly SettingsViewModel settingsViewModel;
        private readonly RevProBot revProBot;

        public SubmitRevProCommand(SettingsViewModel settingsViewModel, RevProBot revProBot)
        {
            this.settingsViewModel = settingsViewModel;
            this.revProBot = revProBot;
        }

        public override async void Execute(object parameter)
        {
            Properties.Settings.Default.Username = settingsViewModel.Username;
            Properties.Settings.Default.Password = ((PasswordBox)parameter).Password;

            Properties.Settings.Default.Save();

            bool success = false;

            MessageBox.Show($"RevPro credentails Saved!",
                            "Credentials",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);

            if (MessageBox.Show("Would you like the bot to start initialization process for RevPro automation?\n(Highly recommended if this is your first time setting your RevPro credentials)",
                            "Confirmation",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                success = await revProBot.SaveEncryptedKey(Properties.Settings.Default.Username, Properties.Settings.Default.Password);
            }

            if (success)
            {
                //Clear textboxes with values
                settingsViewModel.Username = "";
                ((PasswordBox)parameter).Password = "";

                MessageBox.Show("Successfully initialize RevPro Sync",
                    "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Username and Password is incorrect. Please try again",
                    "Failure",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

        }
    }
}
