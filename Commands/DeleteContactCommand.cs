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
    public class DeleteContactCommand : AsyncCommandBase
    {
        private readonly ContactsStore _contactsStore;
        private readonly ContactListingViewModel _contactListingViewModel;

        public DeleteContactCommand(ContactListingViewModel contactListingViewModel, ContactsStore contactsStore)
        {
            _contactListingViewModel = contactListingViewModel;
            _contactsStore = contactsStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Person person = _contactListingViewModel.Person;

            try
            {
                await _contactsStore.Delete(person.Id);
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
