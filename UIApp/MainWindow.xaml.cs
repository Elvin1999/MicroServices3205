using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UIApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private Contact contact;

        public Contact Contact
        {
            get { return contact; }
            set { contact = value; OnPropertyChanged(); }
        }


        private ObservableCollection<Contact> allContacts;

        public ObservableCollection<Contact> AllContacts
        {
            get { return allContacts; }
            set { allContacts = value; OnPropertyChanged(); }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            response = httpClient.GetAsync($@"https://localhost:22950/c").Result;
            var str = response.Content.ReadAsStringAsync().Result;
            var items = JsonConvert.DeserializeObject<List<Contact>>(str);
            AllContacts=new ObservableCollection<Contact>(items);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            response = httpClient.DeleteAsync($@"https://localhost:22950/c/{Contact.Id}").Result;
            var str = response.Content.ReadAsStringAsync().Result;


            response = httpClient.GetAsync($@"https://localhost:22950/c").Result;
            str = response.Content.ReadAsStringAsync().Result;
            var items = JsonConvert.DeserializeObject<List<Contact>>(str);
            AllContacts = new ObservableCollection<Contact>(items);


            MessageBox.Show("Deleted Successfully");

        }
    }
}
