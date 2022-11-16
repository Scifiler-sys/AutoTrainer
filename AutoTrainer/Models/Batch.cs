using AutoTrainer.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrainer.Models
{
    public class Batch
    {
        public IList<Associate> Associates { get; set; }

        private readonly BatchRepository batchRepo;

        public Batch(BatchRepository batchRepository)
        {
            batchRepo = batchRepository;

            this.Associates = batchRepo.Load().Associates;
        }

        public void DeleteAssociate(Associate associate)
        {

        }

        public override bool Equals(object obj)
        {
            Batch other = obj as Batch;

            if (other == null)
                return false;

            for (int i = 0; i < other.Associates.Count; i++)
            {
                if (!other.Associates[i].Equals(this.Associates[i]))
                {
                    return false;
                }
            }

            return true;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Associates, batchRepo);
        }
    }
}
