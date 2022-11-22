using AutoTrainer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoTrainer.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private string _username;

        public string Username
        {
            get { return _username; }
            set 
            { 
                _username = value; 
                OnPropertyChanged(nameof(Username));
            }
        }

        public ICommand SubmitRevProCred { get; }

        public SettingsViewModel()
        {
            SubmitRevProCred = new SubmitRevProCommand(this);
        }
    }
}
