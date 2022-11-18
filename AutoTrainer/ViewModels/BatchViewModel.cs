using AutoTrainer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrainer.ViewModels
{
    public class BatchViewModel : ViewModelBase
    {
        private readonly Batch _batch;

        public BatchViewModel(Batch batch)
        {
            _batch = batch;
        }

        public int statusCode => _batch.statusCode;
        public string description => _batch.description;
        public IList<AssociateViewModel> data => _batch.data.Select(associate => new AssociateViewModel(associate)).ToList();

    }
}
