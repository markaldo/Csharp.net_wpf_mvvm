using Phonebook.Commands;
using Phonebook.Domain.Models;
using Phonebook.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Phonebook.ViewModels
{
    public class ContactListingViewModel : ViewModelBase
    {
        public Person Person { get; private set; }
        public string Name => Person.Name;
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        public ContactListingViewModel(Person person, ContactsStore contactsStore, ModalNavigationStore modalNavigationStore)
        {
            Person = person;

            UpdateCommand = new OpenUpdateContactCommand(this, contactsStore, modalNavigationStore);
            DeleteCommand = new DeleteContactCommand(this, contactsStore);
        }

        public ContactListingViewModel(Person person)
        {
            Person = person;
        }

        public void UpdateContact(Person person)
        {
            Person = person;
            OnPropertyChanged(nameof(Name));
        }
    }
}
