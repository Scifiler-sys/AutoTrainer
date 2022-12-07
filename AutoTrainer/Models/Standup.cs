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
    }
}
