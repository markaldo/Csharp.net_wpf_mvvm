using Phonebook.Domain.Models;
using Phonebook.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.ViewModels
{
    public class ContactDetailsViewModel : ViewModelBase
    {
        private readonly SelectedContactStore selectedContactStore;

        private Person SelectedContact => selectedContactStore.SelectedContact;

        public bool HasSelectedContact => SelectedContact != null;

        public string Name => SelectedContact?.Name ?? "Select";
        public string Surname => SelectedContact?.Surname ?? "";
        public string Telephone => SelectedContact?.Telephone ?? "";
        public string Email => SelectedContact?.Email ?? "";
        public DateTime Birthday => SelectedContact?.Birthday ?? DateTime.Now;

        public ContactDetailsViewModel(SelectedContactStore _selectedContactStore)
        {
            selectedContactStore = _selectedContactStore;

            selectedContactStore.SelectedContactChanged += SelectedContactStore_SelectedContactChanged;
        }

        protected override void Dispose()
        {
            selectedContactStore.SelectedContactChanged += SelectedContactStore_SelectedContactChanged;

            base.Dispose();
        }

        private void SelectedContactStore_SelectedContactChanged()
        {
            OnPropertyChanged(nameof(HasSelectedContact));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Surname));
            OnPropertyChanged(nameof(Telephone));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(Birthday));
        }
    }
}
