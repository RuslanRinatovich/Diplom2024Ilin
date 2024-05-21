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
    /// Логика взаимодействия для StatusPage.xaml
    /// </summary>
    public partial class StatusPage : Page
    {
        AirDBEntities db;
        public StatusPage()
        {
            InitializeComponent();
            LoadData();
        }


        void LoadData()
        {
            db = new AirDBEntities();
            db.Status.Load();
            dtStatus.ItemsSource = db.Status.Local.ToBindingList();
        }


        void SaveData()
        {
            try
            {
                
               db.SaveChanges();
                dialogHostAdd.IsOpen = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка");

            }
            finally
            {
                db.Dispose();
                LoadData();
            }
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveData();

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {


            DialogHost.IsOpen = true;

        }

        private void dtStatus_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            //SaveData();
        }

        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dtStatus.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < dtStatus.SelectedItems.Count; i++)
                    {
                        Status status = dtStatus.SelectedItems[i] as Status;
                        if (status != null)
                        {
                            db.Status.Remove(status);
                        }
                    }
                }
                db.SaveChanges();
                DialogHost.IsOpen = false;
                //MaterialDesignThemes.Wpf.DialogHost.Show("Запись вфыафыва");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка");
                db.Dispose();
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
    }
}