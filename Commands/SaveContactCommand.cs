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
    public class SaveContactCommand : AsyncCommandBase
    {
        private readonly AddContactViewModel _addContactViewModel;
        private readonly ContactsStore _contactsStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public SaveContactCommand(AddContactViewModel addContactViewModel, ContactsStore contactsStore, ModalNavigationStore modalNavigationStore)
        {
            _addContactViewModel = addContactViewModel;
            _contactsStore = contactsStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            ContactDetailsFormViewModel formViewModel = _addContactViewModel.ContactDetailsFormViewModel;
            Person person = new Person(Guid.NewGuid(), formViewModel.Name, formViewModel.Surname, formViewModel.Telephone, formViewModel.Email, formViewModel.Birthday);

            try
            {
                await _contactsStore.Add(person);
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
