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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using AirSystemApp.Models;


namespace AirSystemApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для AirPage.xaml
    /// </summary>
    public partial class AirPage : Page
    {
        //AirDBEntities db;
        string imagePath = AppDomain.CurrentDomain.BaseDirectory + "Images/";
        public AirPage()
        {
            InitializeComponent();
            LoadData();
        }

        void LoadData()
        {
            using (AirDBEntities db = new  AirDBEntities())
            {
                db.Airs.Load();
                List<Air> airs = db.Airs.ToList();
                foreach (Air a in airs)
                {
                    a.Photo = imagePath + a.Photo;
                }

                dtAir.ItemsSource = airs;
            }
        }

        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dtAir.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < dtAir.SelectedItems.Count; i++)
                    {
                        Air xair
                            = dtAir.SelectedItems[i] as Air;
                        if (xair != null)
                        {
                            int id = xair.AirID;
                            using (AirDBEntities db = new AirDBEntities())
                            {
                                Air a = db.Airs.FirstOrDefault(x => x.AirID == id);
                                db.Airs.Remove(xair);
                                db.SaveChanges();
                            }
                        }
                    }
                }
                DialogHost.IsOpen = false;
                //MaterialDesignThemes.Wpf.DialogHost.Show("Запись вфыафыва");
            }
            catch (Exception ex)
            {
                tbError.Text = "Ошибка удаления, есть связанные записи";
                DialogHost.IsOpen = false;
                errorDialog.IsOpen = true;
               
            }
            finally
            {
                LoadData();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = false;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            dialogHostAdd.IsOpen = false;
        }



        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddAirWindow addAirWindow = new AddAirWindow();

            if (addAirWindow.ShowDialog() == true)
            {
                LoadData();
                tbMessage.Text = "Запись добавлена";
                resultDialog.IsOpen = true;

            }

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dtAir.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < dtAir.SelectedItems.Count; i++)
                    {

                        Air air = dtAir.SelectedItems[i] as Air;
                        if (air != null)
                        {

                            AddAirWindow addAirWindow = new AddAirWindow(air.AirID); 

                            if (addAirWindow.ShowDialog() == true)
                            {
                                LoadData();
                                tbMessage.Text = "Запись изменена";
                                resultDialog.IsOpen = true;
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                tbError.Text = "Ошибка добавления";
                errorDialog.IsOpen = true;

            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = true;
        }

        private void BtnOkResult_Click(object sender, RoutedEventArgs e)
        {
            resultDialog.IsOpen = false;
        }
        private void BtnOkError_Click(object sender, RoutedEventArgs e)
        {
            errorDialog.IsOpen = false;
        }
    }
}