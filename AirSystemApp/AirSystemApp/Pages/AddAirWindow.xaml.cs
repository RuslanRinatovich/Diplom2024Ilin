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
using System.IO;
using Microsoft.Win32;

namespace AirSystemApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddAirWindow.xaml
    /// </summary>
    public partial class AddAirWindow : Window
    {
        string filePath = null;
        Air air = null;
        public AddAirWindow()
        {
            InitializeComponent();

        }

        public AddAirWindow(int airId)
        {
            InitializeComponent();
            using (AirDBEntities db = new AirDBEntities())
            {
                air = db.Airs.FirstOrDefault(d => d.AirID == airId);
            }
            LoadAir();

        }

        void LoadAir()
        {
            
            tbAirName.Text = air.AirName;
            tbDescription.Text = air.AirDescription;
            tbPrice.Value = air.Price;
            string imagePath = AppDomain.CurrentDomain.BaseDirectory + "Images/"+air.Photo;
            tbPhotoName.Text = air.Photo;
            imgPhoto.Source = new BitmapImage(new Uri(imagePath));
        }


        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private string CheckFields()
        {
            string s = "";
            if (tbAirName.Text == "")
                s += "Поле название пустое\n";
            if (tbDescription.Text == "")
                s += "Поле Характеристики пустое\n";
            if (tbPrice.Text == "")
                s += "Поле цена пустое\n";
            if (tbPhotoName.Text == "")
                s += "фото не выбрано пустое\n";
        
            return s;
        }


        void AddAir()
        {
            using (AirDBEntities db = new AirDBEntities())
            {
                string photo = ChangePhotoName();
                Air a = new Air
                {
                    AirName = tbAirName.Text,
                    AirDescription = tbDescription.Text,
                    Price = Convert.ToDouble(tbPrice.Value),
                    Photo = photo,
                };
                db.Airs.Add(a);
                
                
                    string dest = AppDomain.CurrentDomain.BaseDirectory + "Images/" + photo;
                    File.Copy(filePath, dest);
               
                db.SaveChanges();
                tbError.Text = "Запись добавлена";
                air = a;

                DialogHost.IsOpen = true;
                this.DialogResult = true;
            }
        }

        void UpdateClient()
        {
            using (AirDBEntities db = new AirDBEntities())
            {
                db.Airs.Attach(air);

                air.AirName = tbAirName.Text;
                air.AirDescription = tbDescription.Text;
                air.Price = Convert.ToDouble(tbPrice.Value);
                air.Photo = tbPhotoName.Text;

                if (filePath != null)
                {
                    air.Photo = ChangePhotoName();
                    string dest = AppDomain.CurrentDomain.BaseDirectory + "Images/" + air.Photo;
                    MessageBox.Show(air.Photo);
                    File.Copy(filePath, dest);
                }
                db.SaveChanges();
                tbError.Text = "Запись обновлена";
                DialogHost.IsOpen = true;
                this.DialogResult = true;


            }
        }

        string ChangePhotoName()
        {
            string x = AppDomain.CurrentDomain.BaseDirectory + "Images/" + tbPhotoName.Text;
            string photoname = tbPhotoName.Text;
            int i = 0;
            if (System.IO.File.Exists(x))
            {
                while (System.IO.File.Exists(x))
                {
                    i++;
                    x = AppDomain.CurrentDomain.BaseDirectory + "Images/" + i.ToString() + photoname;
                }
                photoname = i.ToString() + photoname;
            }
            return photoname;

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFields() != "")
            {
                tbError.Text = CheckFields();
                DialogHost.IsOpen = true;
                return;

            }
            
            if (air == null)
                AddAir();
            else
                UpdateClient();



        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = false;
        }


private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            if (op.ShowDialog() == true)
            {
                imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
                tbPhotoName.Text = op.SafeFileName;
                filePath = op.FileName;
            }
        }
    }
}
