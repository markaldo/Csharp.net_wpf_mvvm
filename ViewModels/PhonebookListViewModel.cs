using Phonebook.Commands;
using Phonebook.Domain.Models;
using Phonebook.Stores;
using PhoneBook.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Phonebook.ViewModels
{
    public class PhonebookListViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ContactListingViewModel> _contacts;
        public IEnumerable<ContactListingViewModel> ContactListingViewModels => _contacts;

        private readonly ObservableCollection<Person> _upcomingBirthdays;
        public IEnumerable<Person> UpComingBirthdays => _upcomingBirthdays;

        private readonly ContactsStore _contactsStore;
        private readonly SelectedContactStore selectedContactStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        public ICommand SearchCommand { get; }
        public ICommand BirthdayLoadCommand { get; }

        private ContactListingViewModel _selectedContactPhonebookVM;
        public ContactListingViewModel SelectedContactPhonebookVM
        {
            get
            {
                return _selectedContactPhonebookVM;
            }
            set
            {
                _selectedContactPhonebookVM = value;
                OnPropertyChanged(nameof(SelectedContactPhonebookVM));
                if (_selectedContactPhonebookVM != null)
                {
                    selectedContactStore.SelectedContact = _selectedContactPhonebookVM.Person;
                }
                else
                {
                    Guid g = Guid.NewGuid();
                    selectedContactStore.SelectedContact = new Person(g, "", "", "", "", DateTime.Now);
                }
            }
        }

        private string query;
        public string Query
        {
            get
            {
                return query;
            }
            set
            {
                query = value;
                OnPropertyChanged(nameof(Query));
            }
        }

        public ICommand LoadContactsCommand { get; }

        public PhonebookListViewModel(ContactsStore contactsStore, SelectedContactStore _selectedContactStore, ModalNavigationStore modalNavigationStore)
        {
            _contactsStore = contactsStore;
            selectedContactStore = _selectedContactStore;
            _modalNavigationStore = modalNavigationStore;
            _contacts = new ObservableCollection<ContactListingViewModel>();
            _upcomingBirthdays = new ObservableCollection<Person>();

            LoadContactsCommand = new LoadContactsCommand(contactsStore);

            _contactsStore.ContactsLoaded += _contactsStore_ContactsLoaded;
            _contactsStore.PersonAdded += ContactsStore_PersonAdded;
            _contactsStore.PersonUpdated += ContactsStore_PersonUpdated;
            _contactsStore.PersonDeleted += ContactsStore_PersonDeleted;

            SearchCommand = new NeutralCommand(SearchQuery);
            BirthdayLoadCommand = new NeutralCommand(Reloading);
        }

        public static PhonebookListViewModel LoadViewModel(ContactsStore contactsStore, SelectedContactStore _selectedContactStore, ModalNavigationStore modalNavigationStore)
        {
            PhonebookListViewModel viewModel = new PhonebookListViewModel(contactsStore, _selectedContactStore, modalNavigationStore);

            viewModel.LoadContactsCommand.Execute(null);

            return viewModel;
        }

        protected override void Dispose()
        {
            _contactsStore.ContactsLoaded -= _contactsStore_ContactsLoaded;
            _contactsStore.PersonAdded -= ContactsStore_PersonAdded;
            _contactsStore.PersonUpdated -= ContactsStore_PersonUpdated;
            _contactsStore.PersonDeleted -= ContactsStore_PersonDeleted;

            base.Dispose();
        }

        private void _contactsStore_ContactsLoaded()
        {
            _contacts.Clear();

            foreach (Person person in _contactsStore.People)
            {
                AddContact(person);
            }
        }

        private void ContactsStore_PersonAdded(Person person)
        {
            AddContact(person);
        }

        private void ContactsStore_PersonUpdated(Person person)
        {
            ContactListingViewModel phonebookViewModel = _contacts.FirstOrDefault(x => x.Person.Id == person.Id);

            if(phonebookViewModel != null)
            {
                phonebookViewModel.UpdateContact(person);
            }
            
        }

        private void AddContact(Person person)
        {
            ContactListingViewModel contactListingViewModels = new ContactListingViewModel(person, _contactsStore, _modalNavigationStore);
            _contacts.Add(contactListingViewModels);
        }

        private void ContactsStore_PersonDeleted(Guid id)
        {
            ContactListingViewModel itemViewModel = _contacts.FirstOrDefault(x => x.Person?.Id == id);

            if(itemViewModel != null)
            {
                _contacts.Remove(itemViewModel);
            }
        }

        private void SearchQuery()
        {
            if (query != null) 
            {
                Person itemViewModel = _contactsStore.People.FirstOrDefault(x => x.Name.ToLower() == query.ToLower()
                                                                            || x.Surname.ToLower() == query.ToLower()
                                                                            || x.Telephone.ToLower() == query.ToLower()
                                                                            || x.Email.ToLower() == query.ToLower());

                if (itemViewModel != null)
                {
                    _contacts.Clear();
                    ContactListingViewModel contactListingViewModels = new ContactListingViewModel(itemViewModel);
                    _contacts.Add(contactListingViewModels);
                }
            }
            else
            {
                MessageBox.Show("Query can't be empty !");
            }
            
        }

        private void Reloading()
        {
           {
                _upcomingBirthdays.Clear();

               var now = DateTime.Now;
               var birthdays = from dt in _contactsStore.People.ToList()
                               orderby IsBeforeNow(now, dt.Birthday), dt.Birthday.Month, dt.Birthday.Day
                               select dt;
               foreach (Person d in birthdays)
               {
                   Person bir = new Person(d.Id, d.Name, d.Surname, d.Telephone, d.Email, d.Birthday);
                    _upcomingBirthdays.Add(bir);
               }

           }
        }
        private static bool IsBeforeNow(DateTime now, DateTime dateTime)
        {
            return dateTime.Month < now.Month
            || (dateTime.Month == now.Month && dateTime.Day < now.Day);
        }
    }
}
