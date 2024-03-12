using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Phonebook.Commands;
using Phonebook.Stores;

namespace Phonebook.ViewModels
{
    public class AddContactViewModel : ViewModelBase
    {
        public ContactDetailsFormViewModel ContactDetailsFormViewModel { get; }

        public AddContactViewModel(ContactsStore contactsStore, ModalNavigationStore modalNavigationStore)
        {
            ICommand saveCommand = new SaveContactCommand(this, contactsStore, modalNavigationStore);
            ICommand closeCommand = new CloseModalCommand(modalNavigationStore);
            ContactDetailsFormViewModel = new ContactDetailsFormViewModel(saveCommand, closeCommand);
        }
    }
}
