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
    public class UpdateContactViewModel : ViewModelBase
    {
        public Guid PersonId { get; }

        public ContactDetailsFormViewModel ContactDetailsFormViewModel { get; }

        public UpdateContactViewModel(Person person, ContactsStore contactsStore, ModalNavigationStore modalNavigationStore)
        {
            PersonId = person.Id;

            ICommand saveCommand = new UpdateContactCommand(this, contactsStore, modalNavigationStore);
            ICommand closeCommand = new CloseModalCommand(modalNavigationStore);
            ContactDetailsFormViewModel = new ContactDetailsFormViewModel(saveCommand, closeCommand)
            {
                Name = person.Name,
                Surname = person.Surname,
                Telephone = person.Telephone,
                Email = person.Email,
                Birthday = person.Birthday,
            };
        }
    }
}
