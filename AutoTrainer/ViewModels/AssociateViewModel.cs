using AutoTrainer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrainer.ViewModels
{
    public class AssociateViewModel : ViewModelBase
    {
        private readonly Associate _associate;
        public string Email => _associate.email;
        public string lastName => _associate.lastName;
        public string firstName => _associate.firstName;
        public string FullName
        {
            get { return firstName + " " + lastName; }
        }

        public string Github => _associate.gitUsername;

        public AssociateViewModel(Associate associate)
        {
            _associate = associate;
        }
    }
}
