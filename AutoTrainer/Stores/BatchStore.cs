using AutoTrainer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrainer.Stores
{
    public class BatchStore
    {
        private BatchViewModel _currentBatch;

        public BatchViewModel CurrentBatch
        {
            get { return _currentBatch; }
            set 
            { 
                _currentBatch = value;
                //Everytime this BatchViewModel sets its data call on this method
                OnCurrentBatchModelChange();
            }
        }

        //This method will invoke 
        private void OnCurrentBatchModelChange()
        {
            //We will then invoke the action that is currently stored in this class
            CurrentBatchModelChange?.Invoke();
        }

        //Pub sub design pattern
        public event Action CurrentBatchModelChange;
    }
}
