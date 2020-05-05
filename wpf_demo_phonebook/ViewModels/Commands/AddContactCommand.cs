using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace wpf_demo_phonebook.ViewModels.Commands
{
    class AddContactCommand : ICommand
    {
        public AddContactCommand()
        {
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
