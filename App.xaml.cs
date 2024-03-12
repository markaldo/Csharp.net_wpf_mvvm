using Contacts.Domain.Commands;
using Contacts.Domain.Queries;
using Contacts.EntityFramework;
using Contacts.EntityFramework.Commands;
using Contacts.EntityFramework.Queries;
using Microsoft.EntityFrameworkCore;
using Phonebook.Stores;
using Phonebook.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ContactsDbContextFactory _contactsDbContextFactory;

        private readonly IGetAllContactsQuery _getAllContactsQuery;
        private readonly ICreateContactCommand _createContactCommand;
        private readonly IUpdateContactCommand _updateContactCommand;
        private readonly IDeleteContactCommand _deleteContactCommand;

        private readonly ModalNavigationStore _modalNavigationaStore;
        private readonly ContactsStore _contactsStore;
        private readonly SelectedContactStore _selectedContactStore;

        public App()
        {
            string connectionString = "Data Source=Phonebook.db";
            _modalNavigationaStore = new ModalNavigationStore();

            _contactsDbContextFactory = new ContactsDbContextFactory(
                new DbContextOptionsBuilder().UseSqlite(connectionString).Options);
            _getAllContactsQuery = new GetAllContactsQuery(_contactsDbContextFactory);
            _createContactCommand = new CreateContactCommand(_contactsDbContextFactory);
            _updateContactCommand = new UpdateContactCommand(_contactsDbContextFactory);
            _deleteContactCommand = new DeleteContactCommand(_contactsDbContextFactory);

            _contactsStore = new ContactsStore(_getAllContactsQuery, _createContactCommand,  _updateContactCommand, _deleteContactCommand);
            _selectedContactStore = new SelectedContactStore(_contactsStore);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            using(ContactsDbContext context = _contactsDbContextFactory.Create())
            {
                context.Database.Migrate();
            }

            PhonebookViewModel phonebookViewModel = new PhonebookViewModel(_contactsStore, _selectedContactStore, _modalNavigationaStore);
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_modalNavigationaStore, phonebookViewModel)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
