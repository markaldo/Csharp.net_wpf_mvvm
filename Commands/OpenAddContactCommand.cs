using Phonebook.Stores;
using Phonebook.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Phonebook.Commands
{
    public class OpenAddContactCommand : CommandBase
    {
        private readonly ContactsStore _contactsStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenAddContactCommand(ContactsStore contactsStore, ModalNavigationStore modalNavigationStore)
        {
            _contactsStore = contactsStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            AddContactViewModel addYouTubeViewerViewModel = new AddContactViewModel(_contactsStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = addYouTubeViewerViewModel;
        }
    }
}
