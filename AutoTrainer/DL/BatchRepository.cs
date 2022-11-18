using AutoTrainer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AutoTrainer.DL
{
    public class BatchRepository : Repository<Batch>
    {
        public BatchRepository() : base()
        { }

        public override Batch Load()
        {
            Batch currentBatch = base.Load();

            //In the event that file does not exist, go make a default batch json file
            if (currentBatch == null)
            {
                using (StreamWriter writer = new StreamWriter(filepath))
                {
                    currentBatch = new Batch();
                    writer.WriteLine(JsonSerializer.Serialize(currentBatch));
                }
            }

            return currentBatch;
        }
    }
}
