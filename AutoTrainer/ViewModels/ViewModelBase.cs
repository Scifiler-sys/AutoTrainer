using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrainer.ViewModels
{
    //INotify... is an interface that will view will automatically hook into and can raise an event 
    public class ViewModelBase : INotifyPropertyChanged
    {
        //When we raise this event, we can tell the UI what bindings to update
        public event PropertyChangedEventHandler PropertyChanged;


        //We can tell our UI that whenever a property value has change, we can tell the UI to re-render or update
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
