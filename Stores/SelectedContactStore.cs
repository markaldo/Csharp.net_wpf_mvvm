using Phonebook.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Stores
{
    public class SelectedContactStore
    {
        private readonly ContactsStore _contactsStore;
        private Person _selectedcontact;
        public Person SelectedContact
        {
            get
            {
                return _selectedcontact;
            }
            set
            {
                _selectedcontact = value;
                SelectedContactChanged?.Invoke();
            }
        }

        public event Action SelectedContactChanged;

        public SelectedContactStore(ContactsStore contactsStore)
        {
            _contactsStore = contactsStore;

            _contactsStore.PersonUpdated += ContactsStore_PersonUpdated;
        }

        private void ContactsStore_PersonUpdated(Person person)
        {
            if (person.Id == SelectedContact?.Id)
            {
                SelectedContact = person;
            }
        }
    }
}
