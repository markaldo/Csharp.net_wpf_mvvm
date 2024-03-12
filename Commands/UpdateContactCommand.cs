using Phonebook.Domain.Models;
using Phonebook.Stores;
using Phonebook.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Phonebook.Commands
{
    public class UpdateContactCommand : AsyncCommandBase
    {
        private readonly UpdateContactViewModel _updateContactViewModel;
        private readonly ContactsStore _contactsStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public UpdateContactCommand(UpdateContactViewModel updateContactViewModel, ContactsStore contactsStore, ModalNavigationStore modalNavigationStore)
        {
            _updateContactViewModel = updateContactViewModel;
            _contactsStore = contactsStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public UpdateContactViewModel UpdateContactViewModel { get; }

        public override async Task ExecuteAsync(object parameter)
        {
            ContactDetailsFormViewModel formViewModel = _updateContactViewModel.ContactDetailsFormViewModel;
            Person person = new Person(_updateContactViewModel.PersonId, formViewModel.Name, formViewModel.Surname, formViewModel.Telephone, formViewModel.Email, formViewModel.Birthday);

            try
            {
                await _contactsStore.Update(person);
                _modalNavigationStore.Close();
                MessageBox.Show("Contact Successfully Saved !");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
