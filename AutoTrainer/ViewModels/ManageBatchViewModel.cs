using AutoTrainer.Commands;
using AutoTrainer.Services;
using AutoTrainer.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoTrainer.ViewModels
{
    public class ManageBatchViewModel : ViewModelBase
    {
        //ObservableCollection implements INoticeChange so it will re-render the page everytime the elements changes
        private readonly BatchStore _batchStore;

        private readonly RevProService _revProServices;

        //Exposing as IEnumerable for encapsulation so that other classes can't just grab this property to add/remove
        public IEnumerable<AssociateViewModel> Associates => _batchStore.CurrentBatch.data;
        public ICommand SyncCommand { get; }

        public ManageBatchViewModel(RevProService revProServices, BatchStore batchStore)
        {
            _revProServices = revProServices;
            _batchStore = batchStore;

            //The moment batch store value has changed, we are passing a OnCurrentBatchModelChanged action
            //We have subscribe to that event to always re-render the page if that value changes
            _batchStore.CurrentBatchModelChange += OnCurrentBatchModelChanged;

            SyncCommand = new SyncBatchCommand(_revProServices, batchStore);
        }

        private void OnCurrentBatchModelChanged()
        {
            //Re-renders the view
            OnPropertyChanged(nameof(Associates));
        }
    }
}
