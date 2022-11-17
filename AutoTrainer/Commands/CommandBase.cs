using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoTrainer.Commands
{
    /*
     * ICommand
     */
    public abstract class CommandBase : ICommand
    {
        //Event has a chance of changing the "CanExecute" to actually make the button executable
        public event EventHandler CanExecuteChanged;

        //Tells what the command can execute
        //If it returns false, the ui button attached will be disabled
        public virtual bool CanExecute(object parameter)
        {
            //By default, it will always be executable
            return true;
        }

        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }


        public abstract void Execute(object parameter);
    }
}
