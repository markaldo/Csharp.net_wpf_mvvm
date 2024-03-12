using Phonebook.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Commands
{
    public class LoadContactsCommand : AsyncCommandBase
    {
        private readonly ContactsStore _contactsStore;

        public LoadContactsCommand(ContactsStore contactsStore)
        {
            _contactsStore = contactsStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _contactsStore.Load();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
