using AutoTrainer.Commands;
using AutoTrainer.Selenium;
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

        private string _emailSignature;

        public string EmailSignature
        {
            get { return _emailSignature; }
            set 
            { 
                _emailSignature = value;
                Properties.Settings.Default.EmailSignature = value;
                Properties.Settings.Default.Save();
                OnPropertyChanged(nameof(EmailSignature));
            }
        }

        private bool _headless;

        public bool Headless
        {
            get { return _headless; }
            set 
            { 
                _headless = value;
                Properties.Settings.Default.Headless = value;
                OnPropertyChanged(nameof(Headless));
            }
        }

        public ICommand SubmitRevProCred { get; }

        public SettingsViewModel(RevProBot revProBot)
        {
            SubmitRevProCred = new SubmitRevProCommand(this, revProBot);
            this.EmailSignature = Properties.Settings.Default.EmailSignature;
            this.Headless = Properties.Settings.Default.Headless;
        }
    }
}
