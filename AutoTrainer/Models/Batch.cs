﻿using AutoTrainer.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrainer.Models
{
    public class Batch
    {
        public int statusCode { get; set; }
        public string description { get; set; }
        public IList<Associate> data { get; set; }

        private readonly BatchRepository batchRepo;

        public Batch()
        {
            this.data = new List<Associate>();
        }

        //Questionable constructor, might delete later
        public Batch(BatchRepository batchRepository)
        {
            batchRepo = batchRepository;

            this.data = batchRepo.Load().data;
        }

        public override bool Equals(object obj)
        {
            Batch other = obj as Batch;

            if (other == null)
                return false;

            for (int i = 0; i < other.data.Count; i++)
            {
                if (!other.data[i].Equals(this.data[i]))
                {
                    return false;
                }
            }

            return true;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(data, batchRepo);
        }
    }
}
