using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace List_Contacts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       private Conteiner_for_Contacts conteiner;

        public MainWindow()
        {

            conteiner = new Conteiner_for_Contacts();

            InitializeComponent();

            ObjectDataProvider provider = new ObjectDataProvider();
            provider.ObjectInstance = conteiner;
            provider.MethodName = "GetContacts";
            Binding bind = new Binding(){ Source = provider };
            List_for_Contacts.SetBinding(ListBox.ItemsSourceProperty, bind);

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            conteiner.GetContacts().Add(new Contact() { name = "", surname = "", phone_num = "" });
            List_for_Contacts.SelectedIndex = conteiner.GetContacts().Count - 1;
        }

        private void Del_Button_Click(object sender, RoutedEventArgs e)
        {
            if (List_for_Contacts.SelectedIndex == -1) return;

            conteiner.GetContacts().RemoveAt(List_for_Contacts.SelectedIndex);

            List_for_Contacts.SelectedIndex = conteiner.GetContacts().Count - 1;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            conteiner.save();
        }

    }
}

