using AutoTrainer.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrainer.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _store;
        public ViewModelBase CurrentViewModel => _store.CurrentViewModel;

        public MainViewModel(NavigationStore store)
        {
            _store = store;
        }
    }
}
