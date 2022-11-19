using AutoTrainer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrainer.Stores
{
    /*
     * Will hold the currentViewModel of our app
     * 
     * We put it on the store since we want a universal access to this information and also allow any viewmodel to change _currentViewModel depending on what the user did
     */
    public class NavigationStore
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set 
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

        public event Action CurrentViewModelChanged;
    }
}
