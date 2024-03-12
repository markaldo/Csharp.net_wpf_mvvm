using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Phonebook.ViewModels
{
    public class ContactDetailsFormViewModel : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        private string _surname;
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        private string _telephone;
        public string Telephone
        {
            get
            {
                return _telephone;
            }
            set
            {
                _telephone = value;

                Regex val = new Regex("^\\d+$");
                if (!val.IsMatch(Telephone))
                {
                    MessageBox.Show("Invalid phone number !, only numbers allowed");
                }
                else
                {
                    OnPropertyChanged(nameof(Telephone));
                    OnPropertyChanged(nameof(CanSubmit));
                } 
            }
        }

        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private DateTime _birthday;

        public DateTime Birthday
        {
            get
            {
                return _birthday;
            }
            set
            {
                _birthday = value;
                OnPropertyChanged(nameof(Birthday));
            }
        }

        public bool CanSubmit => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Telephone);

        public ICommand SaveCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public ContactDetailsFormViewModel(ICommand saveCommand, ICommand closeCommand)
        {
            SaveCommand = saveCommand;
            CloseCommand = closeCommand;
        }
    }
}
