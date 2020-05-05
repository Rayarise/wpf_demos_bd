using System;
using System.Collections.ObjectModel;
using System.Windows;
using wpf_demo_phonebook.ViewModels.Commands;


namespace wpf_demo_phonebook.ViewModels
{
    class MainViewModel : BaseViewModel
    {

        

        readonly ContactDataService contactDataService = new ContactDataService();
        private ObservableCollection<ContactModel> contacts;

        private ContactModel selectedContact;


        public ObservableCollection<ContactModel> Contacts
        {
            get => contacts;
            private set
            {
               contacts = value;
                OnPropertyChanged();
            }
        }
        public ContactModel SelectedContact
        {
            get => selectedContact;
            set { 
                selectedContact = value;
                OnPropertyChanged();
            }
        }

        private string criteria;

        public string Criteria
        {
            get { return criteria; }
            set { 
                criteria = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SearchContactCommand { get; set; }
        public RelayCommand DeleteContactCommand { get; private set; }
        public RelayCommand ModContactCommand { get; private set; }

        public RelayCommand AddContactCommand { get; private set; }
        //----------------------------------------const------------------------------------
        public MainViewModel()
        {
            SearchContactCommand = new RelayCommand(SearchContact);
            SelectedContact = PhoneBookBusiness.GetContactByID(1);
            Contacts = new ObservableCollection<ContactModel>(contactDataService.GetAll());

            DeleteContactCommand = new RelayCommand(DeleteContact, CanDeleteContact);
            ModContactCommand = new RelayCommand(ModContact, CanModContact);
            AddContactCommand = new RelayCommand(AddContact, CanAddContact);
        }
//----------------------------------------filter------------------------------------
        private void SearchContact(object parameter)
        {
            string input = parameter as string;
            int output;
            string searchMethod;
            if (!Int32.TryParse(input, out output))
            {
                searchMethod = "name";
            } else
            {
                searchMethod = "id";
            }

            switch (searchMethod)
            {
                case "id":
                    SelectedContact = PhoneBookBusiness.GetContactByID(output);
                    break;
                case "name":
                    SelectedContact = PhoneBookBusiness.GetContactByName(input);
                  
                    break;
                default:
                    MessageBox.Show("Unkonwn search method");
                    break;
            }
        }
        //----------------------------------------commands------------------------------------
        private void DeleteContact(Object c)
        {
          

            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                ContactModel customer = c as ContactModel;

                var currentIndex = Contacts.IndexOf(customer);

                if (currentIndex > 0) currentIndex--;

                SelectedContact = Contacts[currentIndex];

                Contacts.Remove(customer);
            }
            else
            {

            }
        }

        private bool CanDeleteContact(Object c)
        {
          
                return true;
            
               
        }

        private void ModContact(Object c)
        {
            ContactModel customer = c as ContactModel;
            var currentIndex = Contacts.IndexOf(customer);

            if (currentIndex > 0)
            {
                SelectedContact = Contacts[currentIndex];
            }


            ContactModel temp = new ContactModel() { LastName = customer.LastName, FirstName = customer.FirstName, Email = customer.Email, Mobile = customer.Mobile, ContactID = customer.ContactID, Phone = customer.Phone   };
        
           Contacts.Remove(customer);


            if (currentIndex > 0)
            {
                Contacts.Insert(currentIndex, temp);
                SelectedContact = temp;
            }
        }

        private bool CanModContact(Object c)
        {

            return true;


        }


        private void AddContact(Object c)
        {
            ContactModel temp = new ContactModel() { LastName = "Undefined", FirstName = "Undefined" };
            Contacts.Add(temp);
            SelectedContact = temp;
        }

        private bool CanAddContact(Object c)
        {

            return true;


        }
    }
}
