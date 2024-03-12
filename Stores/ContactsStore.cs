using Contacts.Domain.Commands;
using Contacts.Domain.Queries;
using Phonebook.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Stores
{
    public class ContactsStore
    {
        private readonly IGetAllContactsQuery _getAllContactsQuery;
        private readonly ICreateContactCommand _createContactCommand;
        private readonly IUpdateContactCommand _updateContactCommand;
        private readonly IDeleteContactCommand _deleteContactCommand;

        private readonly List<Person> _people;
        public IEnumerable<Person> People => _people;

        public event Action ContactsLoaded;
        public event Action<Person> PersonAdded;
        public event Action<Person> PersonUpdated;
        public event Action<Guid> PersonDeleted;

        public ContactsStore(IGetAllContactsQuery getAllContactsQuery, 
            ICreateContactCommand createContactCommand, 
            IUpdateContactCommand updateContactCommand, 
            IDeleteContactCommand deleteContactCommand)
        {
            _getAllContactsQuery = getAllContactsQuery;
            _createContactCommand = createContactCommand;
            _updateContactCommand = updateContactCommand;
            _deleteContactCommand = deleteContactCommand;

            _people = new List<Person>();
        }

        public async Task Load()
        {
            IEnumerable<Person> people = await _getAllContactsQuery.Execute();

            _people.Clear();
            _people.AddRange(people);

            ContactsLoaded?.Invoke();
        }

        public async Task Add(Person person)
        {
            await _createContactCommand.Execute(person);
            _people.Add(person);
            PersonAdded?.Invoke(person);
        }
        public async Task Update(Person person)
        {
            await _updateContactCommand.Execute(person);
            int currentIndex = _people.FindIndex(x => x.Id == person.Id);

            if(currentIndex != -1)
            {
                _people[currentIndex] = person;
            }
            else
            {
                _people.Add(person);
            }
            PersonUpdated?.Invoke(person);
        }

        public async Task Delete(Guid id)
        {
            await _deleteContactCommand.Execute(id);

            _people.RemoveAll(x => x.Id == id);

            PersonDeleted?.Invoke(id);
        }
    }
}
