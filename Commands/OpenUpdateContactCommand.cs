using Phonebook.Domain.Models;
using Phonebook.Stores;
using Phonebook.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Commands
{
    public class OpenUpdateContactCommand : CommandBase
    {
        private readonly ContactsStore _contactsStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ContactListingViewModel _contactListingViewModel;

        public OpenUpdateContactCommand(ContactListingViewModel contactListingViewModel, ContactsStore contactsStore, ModalNavigationStore modalNavigationStore)
        {
            _contactListingViewModel = contactListingViewModel;
            _contactsStore = contactsStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            Person person = _contactListingViewModel.Person;

            UpdateContactViewModel updateContactViewModel = new UpdateContactViewModel(person, _contactsStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = updateContactViewModel;
        }
    }
}
