using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using AirSystemApp.Models;
using System.Data.Entity;
using System.Text.RegularExpressions;

namespace AirSystemApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        Client mainclient = null;
        public AddClientWindow()
        {
            InitializeComponent();
            mainclient = null;

        }

        public AddClientWindow(int clientId)
        {
            InitializeComponent();
            using (AirDBEntities db = new AirDBEntities())
            {
                mainclient = db.Clients.FirstOrDefault(d => d.ClientID == clientId) ;
            }
            LoadClient();
            
        }

        void LoadClient()
        {
            tbFirstName.Text = mainclient.FirstName;
            tbLastName.Text = mainclient.LastName;
            tbMiddleName.Text = mainclient.MiddleName;
            tbEmail.Text = mainclient.Email;
            tbPhone.Text = mainclient.Phone;
        }


        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        //void CheckData()
        //{
        //    if ((string.IsNullOrEmpty(tbFirstName.Text)) ||
        //        (string.IsNullOrEmpty(tbLastName.Text)) ||
        //        (string.IsNullOrEmpty(tbMiddleName.Text)) ||
        //        (string.IsNullOrEmpty(tbPhone.Text)) ||
        //        (string.IsNullOrEmpty(tbEmail.Text))
        //        )
        //    {

        //        tbError.Text = "Поля пустые ";
                

        //        DialogHost.IsOpen = true;
        //        return;
        //    }


        //}

        private bool CheckEmail(string email)
        {
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            if (Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string CheckFields()
        {
            string s = "";
            if (tbFirstName.Text == "")
                s += "Поле Имя пустое\n";
            if (tbLastName.Text == "")
                s += "Поле Фамилия пустое\n";
            if (tbMiddleName.Text == "")
                s += "Поле Отчество пустое\n";
            if (tbEmail.Text == "")
                s += "Поле email пустое\n";
            if (tbPhone.Text == "")
                s += "Поле телефон пустое\n";
            return s;
        }


        void AddClient()
        {
            using (AirDBEntities db = new AirDBEntities())
            {
                Client client = new Client
                {
                    FirstName = tbFirstName.Text,
                    LastName = tbLastName.Text,
                    MiddleName = tbMiddleName.Text,
                    Phone = tbPhone.Text,
                    Email = tbEmail.Text
                };
                db.Clients.Add(client);
                db.SaveChanges();
                tbError.Text = "Запись добавлена";
                mainclient = client;
                
                DialogHost.IsOpen = true;
                this.DialogResult = true;
            }
        }

        void UpdateClient()
        {
            using (AirDBEntities db = new AirDBEntities())
            {
                db.Clients.Attach(mainclient);

                mainclient.FirstName = tbFirstName.Text;
                mainclient.LastName = tbLastName.Text;
                mainclient.MiddleName = tbMiddleName.Text;
                mainclient.Phone = tbPhone.Text;
                mainclient.Email = tbEmail.Text;

                
                db.SaveChanges();
                tbError.Text = "Запись обновлена";
                DialogHost.IsOpen = true;
                this.DialogResult = true;
                

            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFields() != "")
            {
                tbError.Text = CheckFields();
                 DialogHost.IsOpen = true;
                return;

            }

            if (!CheckEmail(tbEmail.Text))
            {
                tbError.Text = "Некорректный email";
                DialogHost.IsOpen = true;
                return;
            }

            if (mainclient == null)
                AddClient();
            else
                UpdateClient();

            

        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = false;
        }
    }
}
