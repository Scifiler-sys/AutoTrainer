using AutoTrainer.Commands;
using AutoTrainer.Selenium;
using AutoTrainer.Services;
using AutoTrainer.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AutoTrainer.ViewModels
{
    public class ManageBatchViewModel : ViewModelBase
    {
        //ObservableCollection implements INoticeChange so it will re-render the page everytime the elements changes
        private readonly BatchStore _batchStore;

        private readonly RevProService _revProServices;
        private readonly EmailBot _emailBot;

        //Exposing as IEnumerable for encapsulation so that other classes can't just grab this property to add/remove
        public IEnumerable<AssociateViewModel> Associates => _batchStore.CurrentBatch.data;
        public ICommand SyncCommand { get; }

        public ICommand WarnCommand { get; }

        public ManageBatchViewModel(RevProService revProServices, BatchStore batchStore, EmailBot emailBot)
        {
            _revProServices = revProServices;
            _batchStore = batchStore;
            _emailBot = emailBot;

            //The moment batch store value has changed, we are passing a OnCurrentBatchModelChanged action
            //We have subscribe to that event to always re-render the page if that value changes
            _batchStore.CurrentBatchModelChange += OnCurrentBatchModelChanged;

            //These most likely need to change to go to somewhere else
            SyncCommand = new SyncBatchCommand(_revProServices, batchStore);
            WarnCommand = new WarnCommand(_emailBot);
        }

        private void OnCurrentBatchModelChanged()
        {
            //Re-renders the view
            OnPropertyChanged(nameof(Associates));
        }

        public void WarnButton_Click(object sender, RoutedEventArgs e)
        {
            AssociateViewModel associate = (AssociateViewModel)((Button)e.Source).DataContext;

            MessageBox.Show($"{associate.firstName}");
        }
    }
}
