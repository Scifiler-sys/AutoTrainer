using AutoTrainer.Models;
using AutoTrainer.Services;
using AutoTrainer.Stores;
using AutoTrainer.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            try
            {
                Batch newBatch = await _revProService.SyncBatch();

                //Setting Batch store to whatever we got from the revapi
                if (newBatch != null)
                {
                    _batchStore.CurrentBatch = new BatchViewModel(newBatch);

                }
                MessageBox.Show("Successfully synced the batch!",
                            "Sync Batch",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);

            }
            catch (ValidationException exc)
            {
                MessageBox.Show($"Unable to sync batch. Double check your RevPro login credentials in Settings.\nError: {exc.Message}",
                                "Failed",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }

            

            
        }
    }
}
