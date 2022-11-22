using AutoTrainer.ViewModels;
using System;
using System.Collections.Generic;
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

        public SubmitRevProCommand(SettingsViewModel settingsViewModel)
        {
            this.settingsViewModel = settingsViewModel;
        }

        public override void Execute(object parameter)
        {
            Properties.Settings.Default.Username = settingsViewModel.Username;
            Properties.Settings.Default.Password = ((PasswordBox)parameter).Password;

            Properties.Settings.Default.Save();

            MessageBox.Show($"RevPro credentails Saved!",
                            "Credentials",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);

            //Clear textboxes with values
            settingsViewModel.Username = "";
            ((PasswordBox)parameter).Password = "";
        }
    }
}
