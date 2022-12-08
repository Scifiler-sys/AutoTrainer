using AutoTrainer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrainer.Models
{
    public class Associate
    {
        public int internId { get; set; }
        public int orgId { get; set; }
        public string email { get; set; }
        public string contactNo { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string salesForceId { get; set; }
        public bool isActive { get; set; }
        public string profileImageUrl { get; set; }
        public string primaryPortfolioStatus { get; set; }
        public int primaryPortfolioId { get; set; }
        public string primaryPortfolioStatusCode { get; set; }
        public string primaryPortfolioTitle { get; set; }
        public string primaryPortfolioStatusLabel { get; set; }
        public string profileURL { get; set; }
        public DateTime submittedDate { get; set; }
        public int statusId { get; set; }
        public bool isQuizAssigned { get; set; }
        public bool isDroppedTrainee { get; set; }
        public int totalRecords { get; set; }
        public bool isNewTrainee { get; set; }
        public string gitUsername { get; set; }

        public bool isWarned { get; set; }

        public Associate()
        {

        }

        public Associate(AssociateViewModel associate)
        {
            this.email = associate.Email;
            this.firstName = associate.firstName;
            this.lastName = associate.lastName;
            this.gitUsername = associate.Github;
        }

        public override bool Equals(object obj)
        {
            return obj is Associate associate &&
                   internId == associate.internId &&
                   orgId == associate.orgId &&
                   email == associate.email &&
                   contactNo == associate.contactNo &&
                   lastName == associate.lastName &&
                   firstName == associate.firstName &&
                   salesForceId == associate.salesForceId &&
                   isActive == associate.isActive &&
                   profileImageUrl == associate.profileImageUrl &&
                   primaryPortfolioStatus == associate.primaryPortfolioStatus &&
                   primaryPortfolioId == associate.primaryPortfolioId &&
                   primaryPortfolioStatusCode == associate.primaryPortfolioStatusCode &&
                   primaryPortfolioTitle == associate.primaryPortfolioTitle &&
                   primaryPortfolioStatusLabel == associate.primaryPortfolioStatusLabel &&
                   profileURL == associate.profileURL &&
                   submittedDate == associate.submittedDate &&
                   statusId == associate.statusId &&
                   isQuizAssigned == associate.isQuizAssigned &&
                   isDroppedTrainee == associate.isDroppedTrainee &&
                   totalRecords == associate.totalRecords &&
                   isNewTrainee == associate.isNewTrainee &&
                   gitUsername == associate.gitUsername;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(internId);
            hash.Add(orgId);
            hash.Add(email);
            hash.Add(contactNo);
            hash.Add(lastName);
            hash.Add(firstName);
            hash.Add(salesForceId);
            hash.Add(isActive);
            hash.Add(profileImageUrl);
            hash.Add(primaryPortfolioStatus);
            hash.Add(primaryPortfolioId);
            hash.Add(primaryPortfolioStatusCode);
            hash.Add(primaryPortfolioTitle);
            hash.Add(primaryPortfolioStatusLabel);
            hash.Add(profileURL);
            hash.Add(submittedDate);
            hash.Add(statusId);
            hash.Add(isQuizAssigned);
            hash.Add(isDroppedTrainee);
            hash.Add(totalRecords);
            hash.Add(isNewTrainee);
            hash.Add(gitUsername);
            return hash.ToHashCode();
        }
    }
}
