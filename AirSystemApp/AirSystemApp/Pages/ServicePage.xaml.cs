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
using System.Text.RegularExpressions;

namespace AirSystemApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        AirDBEntities db;
        
        public ServicePage()
        {
            InitializeComponent();
            LoadData();
        }

        void LoadData()
        {
            db = new AirDBEntities();
            db.Services.Load(); db.ServiceCategories.Load();
            dtServices.ItemsSource = db.Services.Local.ToBindingList();
            colServiceCategories.ItemsSource = db.ServiceCategories.ToList();
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
                tbError.Text = "Ошибка добавления";
                errorDialog.IsOpen = true;

            }
            finally
            {
                
                //db.Dispose();
                //LoadData();
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

        private void DtServiceCategory_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            //SaveData();
        }

        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dtServices.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < dtServices.SelectedItems.Count; i++)
                    {
                        Service service = dtServices.SelectedItems[i] as Service;
                        if (service != null)
                        {
                            db.Services.Remove(service);
                        }
                    }
                }
                db.SaveChanges();
                DialogHost.IsOpen = false;
                //MaterialDesignThemes.Wpf.DialogHost.Show("Запись вфыафыва");
            }
            catch (Exception ex)
            {
                tbError.Text = "Ошибка удаления, есть связанные записи";
                DialogHost.IsOpen = false;
                errorDialog.IsOpen = true;
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

        private void TbPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //if (e.Text.Contains("."))
            //    e.Handled = true;
            //else
                e.Handled = ! new Regex(@"^[0-9]*(?:\.[0-9]*)?$").IsMatch(e.Text);
        }

        private void TbPrice_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void BtnOkError_Click(object sender, RoutedEventArgs e)
        {
            errorDialog.IsOpen = false;
        }
    }
}