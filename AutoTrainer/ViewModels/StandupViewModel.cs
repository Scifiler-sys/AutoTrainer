using AutoTrainer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrainer.ViewModels
{
    public class StandupViewModel : ViewModelBase
    {
        public DateTime SelectedDate { get; set; }
        public string CurrentTeam { get; set; }
        public int HowManyAssociate { get; set; }
        public string ListOfInitiatives { get; set; }
        public bool AwareOfBatch { get; set; }
        public string NotMatchingSF { get; set; }
        public int HowManyWarnings { get; set; }
        public int ProjectToStaging { get; set; }
        public string GeneralNote { get; set; }
        public int WorkLoad { get; set; }
        public StandupViewModel(Standup standup)
        {
            SelectedDate = standup.SelectedDate;
            CurrentTeam = standup.CurrentTeam;
            HowManyAssociate = standup.HowManyAssociates;
            ListOfInitiatives = standup.ListOfInitiatives;
            AwareOfBatch = standup.AwareOfBatch;
            NotMatchingSF = standup.NotMatchingSF;
            HowManyWarnings = standup.HowManyWarnings;
            ProjectToStaging = standup.ProjectToStaging;
            GeneralNote = standup.GeneralNote;
            WorkLoad = standup.WorkLoad;
        }
    }
}
