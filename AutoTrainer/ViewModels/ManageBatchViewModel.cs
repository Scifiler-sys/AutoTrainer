using AutoTrainer.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoTrainer.ViewModels
{
    public class ManageBatchViewModel : ViewModelBase
    {
        //ObservableCollection implements INoticeChange so it will re-render the page everytime the elements changes
        private readonly ObservableCollection<AssociateViewModel> _associates;

        //Exposing as IEnumerable for encapsulation so that other classes can't just grab this property to add/remove
        public IEnumerable<AssociateViewModel> Associates => _associates;
        public ICommand SyncCommand { get; }

        public ManageBatchViewModel()
        {
            _associates = new ObservableCollection<AssociateViewModel>();

            _associates.Add(new AssociateViewModel(new Models.Associate() { firstName = "Stephen", lastName = "Pagdilao", email="sp@gmail.com", gitUsername = "spGit"}));
            _associates.Add(new AssociateViewModel(new Models.Associate() { firstName = "Marielle", lastName = "Nolasco", email = "mn@gmail.com", gitUsername = "mnGit" }));
            _associates.Add(new AssociateViewModel(new Models.Associate() { firstName = "Pablo", lastName = "Cruz", email = "pc@gmail.com", gitUsername = "pcGit" }));

            SyncCommand = new SyncBatchCommand();
        }

    }
}
