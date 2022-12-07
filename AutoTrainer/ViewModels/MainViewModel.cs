using AutoTrainer.Commands;
using AutoTrainer.Stores;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoTrainer.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _store;

        public ICommand ChangeSettingCommand { get; }
        public ICommand ChangeManageBatchCommand { get; }
        public ICommand ChangeSendStandupCommand { get; }
        public ViewModelBase CurrentViewModel => _store.CurrentViewModel;

        public MainViewModel(NavigationStore store, IServiceProvider s)
        {
            _store = store;

            //Subscribing to Navigation store to see any changes
            _store.CurrentViewModelChanged += OnCurrentViewModelChanged;

            /*
             * Need to change the below commands to work with the factory methods
             */

            ChangeSettingCommand = new NavigateCommand<ViewModelBase>(_store, () => s.GetRequiredService<SettingsViewModel>());
            ChangeManageBatchCommand = new NavigateCommand<ViewModelBase>(_store, () => s.GetRequiredService<ManageBatchViewModel>());
            ChangeSendStandupCommand = new NavigateCommand<ViewModelBase>(_store, () => s.GetRequiredService<SendStandupViewModel>());
        }

        //If Nav store does change, it will call on the propertychange hook within this viewmodel
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
