using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP23_G9.ViewModels.Base
{
    [AddINotifyPropertyChangedInterface]

    class BaseViewModel : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public BaseViewModel(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? (() => true);
        }

        public event EventHandler CanExecuteChanged
        {

            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => _canExecute();

        public void Execute(object parameter) => _execute();
    }
}