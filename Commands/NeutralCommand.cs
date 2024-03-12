using Phonebook.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Commands
{
    class NeutralCommand : CommandBase
    {
        Action _execute;
        public NeutralCommand(Action execute)
        {
            this._execute = execute;
        }

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            _execute();
        }
    }
}
