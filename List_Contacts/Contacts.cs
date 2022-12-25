using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace List_Contacts
{
    public class Conteiner_for_Contacts
    {
        private ObservableCollection<Contact> contacts;
        public Conteiner_for_Contacts()
        {
            contacts = new ObservableCollection<Contact>();

            if(File.Exists("contacts.txt"))
                load();
        }

        public ObservableCollection<Contact> GetContacts() => contacts;

        public void save()
        {
            using (FileStream fs = new FileStream("contacts.txt", FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    int i = 0;
                    while (contacts.Count != i)
                    {
                        sw.WriteLine(contacts.ElementAt(i).name);
                        sw.WriteLine(contacts.ElementAt(i).surname);
                        sw.WriteLine(contacts.ElementAt(i).phone_num);
                        i++;
                    }

                }
            }
        }
        private void load()
        {
            using (FileStream fs = new FileStream("contacts.txt", FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    
                    int i = 0;
                    while (!sr.EndOfStream)
                    {
                        Contact item = new Contact();
                        i++;

                        item.name = sr.ReadLine();
                        item.surname = sr.ReadLine();
                        item.phone_num = sr.ReadLine();

                        if (i == 1)
                        {
                           contacts.Add(item);
                           i = 0;
                        }
                    }
                }
            }
        }
    }
    public class Contact : INotifyPropertyChanged
    {
        private string Name;
        private string Surname;
        private string PhoneNumber;

        public string name
        {
            get => Name;
            set { setProperty(ref Name, value, nameof(name)); }
        }
        public string surname
        {
            get => Surname;
            set { setProperty(ref Surname, value, nameof(surname)); }
        }
        public string phone_num
        {
            get => PhoneNumber;
            set { setProperty(ref PhoneNumber, value, nameof(phone_num)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void setProperty<T>(ref T field, T value, string name)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        public override string ToString()
        {
            return $"{surname} {name} \n {phone_num}";
        }
    }
}
