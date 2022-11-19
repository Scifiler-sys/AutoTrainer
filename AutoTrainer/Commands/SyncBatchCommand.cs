using AutoTrainer.Models;
using AutoTrainer.Services;
using AutoTrainer.Stores;
using AutoTrainer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoTrainer.Commands
{
    public class SyncBatchCommand : CommandBase
    {
        private readonly RevProService _revProService;
        private readonly BatchStore _batchStore;

        public SyncBatchCommand(RevProService revProService, BatchStore batchStore)
        {
            _revProService = revProService;
            _batchStore = batchStore;
        }

        public async override void Execute(object parameter)
        {
            Batch newBatch = await _revProService.SyncBatch();

            //After syncing, I want to refresh the page. Most likely need to have a pub sub dp with batch store
            //pub sub already setup just need to change the property directly
            //Setter in batch store will see that change and automatically invoke the necesary 
            _batchStore.CurrentBatch = new BatchViewModel(newBatch);

            MessageBox.Show("Successfully synced the batch!",
                            "Sync Batch",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }
    }
}
