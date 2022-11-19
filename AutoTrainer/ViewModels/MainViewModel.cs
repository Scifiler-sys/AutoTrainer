using AutoTrainer.Commands;
using AutoTrainer.Stores;
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
        public ViewModelBase CurrentViewModel => _store.CurrentViewModel;

        public MainViewModel(NavigationStore store)
        {
            _store = store;

            //Subscribing to Navigation store to see any changes
            _store.CurrentViewModelChanged += OnCurrentViewModelChanged;

            ChangeSettingCommand = new NavigateCommand<SettingsViewModel>(_store, () => new SettingsViewModel());
            ChangeManageBatchCommand = new NavigateCommand<ManageBatchViewModel>(_store, () => new ManageBatchViewModel());
        }

        //If Nav store does change, it will call on the propertychange hook within this viewmodel
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
