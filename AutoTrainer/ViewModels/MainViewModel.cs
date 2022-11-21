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
        private readonly FactoryViewModel _factoryViewModel;

        public ICommand ChangeSettingCommand { get; }
        public ICommand ChangeManageBatchCommand { get; }
        public ViewModelBase CurrentViewModel => _store.CurrentViewModel;

        public MainViewModel(NavigationStore store, FactoryViewModel factoryViewModel)
        {
            _store = store;
            _factoryViewModel = factoryViewModel;

            //Subscribing to Navigation store to see any changes
            _store.CurrentViewModelChanged += OnCurrentViewModelChanged;

            /*
             * Need to change the below commands to work with the factory methods
             */

            ChangeSettingCommand = new NavigateCommand<ViewModelBase>(_store, () => { return factoryViewModel.GetViewModel(ViewModelType.Settings); });
            ChangeManageBatchCommand = new NavigateCommand<ViewModelBase>(_store, () => { return factoryViewModel.GetViewModel(ViewModelType.ManageBatch); });
        }

        //If Nav store does change, it will call on the propertychange hook within this viewmodel
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
