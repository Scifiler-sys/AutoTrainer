using AutoTrainer.Stores;
using AutoTrainer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrainer.Commands
{
    public class NavigateCommand<TViewModel> : CommandBase
        where TViewModel:ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModelBase> _createViewModel;

        public NavigateCommand(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
