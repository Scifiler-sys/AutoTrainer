using AutoTrainer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrainer.Models
{
    public class Standup
    {
        public DateTime SelectedDate { get; set; }
        public string CurrentTeam { get; set; }
        public int HowManyAssociates { get; set; }
        public int HowManyWarnings { get; set; }
        public int ProjectToStaging { get; set; }
        public string GeneralNote { get; set; }
        public int WorkLoad { get; set; }
        public string NotMatchingSF { get; set; }
        public string ListOfInitiatives { get; set; }
        public bool AwareOfBatch { get; set; }

        public Standup() { }

        public Standup(StandupViewModel standupViewModel)
        {
            SelectedDate = standupViewModel.SelectedDate;
            CurrentTeam = standupViewModel.CurrentTeam;
            HowManyAssociates = standupViewModel.HowManyAssociate;
            ListOfInitiatives = standupViewModel.ListOfInitiatives;
            AwareOfBatch = standupViewModel.AwareOfBatch;
            NotMatchingSF = standupViewModel.NotMatchingSF;
            HowManyWarnings = standupViewModel.HowManyWarnings;
            ProjectToStaging = standupViewModel.ProjectToStaging;
            GeneralNote = standupViewModel.GeneralNote;
            WorkLoad = standupViewModel.WorkLoad;
        }

        public override bool Equals(object obj)
        {
            return obj is Standup standup &&
                   SelectedDate == standup.SelectedDate &&
                   CurrentTeam == standup.CurrentTeam &&
                   HowManyAssociates == standup.HowManyAssociates &&
                   HowManyWarnings == standup.HowManyWarnings &&
                   ProjectToStaging == standup.ProjectToStaging &&
                   GeneralNote == standup.GeneralNote &&
                   WorkLoad == standup.WorkLoad &&
                   NotMatchingSF == standup.NotMatchingSF &&
                   ListOfInitiatives == standup.ListOfInitiatives &&
                   AwareOfBatch == standup.AwareOfBatch;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(SelectedDate);
            hash.Add(CurrentTeam);
            hash.Add(HowManyAssociates);
            hash.Add(HowManyWarnings);
            hash.Add(ProjectToStaging);
            hash.Add(GeneralNote);
            hash.Add(WorkLoad);
            hash.Add(NotMatchingSF);
            hash.Add(ListOfInitiatives);
            hash.Add(AwareOfBatch);
            return hash.ToHashCode();
        }
    }
}
