using Phonebook.Commands;
using Phonebook.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Phonebook.ViewModels
{
    public class PhonebookViewModel : ViewModelBase
    {
        public PhonebookListViewModel PhonebookListViewModel { get; }
        public ContactDetailsViewModel ContactDetailsViewModel { get; }
        public ICommand AddContactCommand { get; }

        public PhonebookViewModel(ContactsStore contactsStore, SelectedContactStore _selectedContactStore, ModalNavigationStore modalNavigationStore)
        {
            PhonebookListViewModel = PhonebookListViewModel.LoadViewModel(contactsStore, _selectedContactStore, modalNavigationStore);
            ContactDetailsViewModel = new ContactDetailsViewModel(_selectedContactStore);

            AddContactCommand = new OpenAddContactCommand(contactsStore, modalNavigationStore);
        }
    }
}
