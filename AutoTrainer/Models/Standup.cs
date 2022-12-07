using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrainer.Models
{
    public class Standup
    {
        public DateTime StartDate { get; set; }
        public string CurrentTeam { get; set; }
        public int HowManyAssociates { get; set; }
        public string ListOfInitiatives { get; set; }
        public bool AwareOfBatch { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Standup standup &&
                   StartDate == standup.StartDate &&
                   CurrentTeam == standup.CurrentTeam &&
                   HowManyAssociates == standup.HowManyAssociates &&
                   ListOfInitiatives == standup.ListOfInitiatives &&
                   AwareOfBatch == standup.AwareOfBatch;
        }
    }
}
