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
    /// Логика взаимодействия для ServiceCategoryPage.xaml
    /// </summary>
    public partial class ServiceCategoryPage : Page
    {
        AirDBEntities db;
        public ServiceCategoryPage()
        {
            InitializeComponent();
            LoadData();
        }
        void LoadData()
        {
            db = new AirDBEntities();
            db.ServiceCategories.Load();
            dtServiceCategory.ItemsSource = db.ServiceCategories.Local.ToBindingList();
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

        private void DtServiceCategory_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            //SaveData();
        }

        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dtServiceCategory.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < dtServiceCategory.SelectedItems.Count; i++)
                    {
                        ServiceCategory serviceCategory = dtServiceCategory.SelectedItems[i] as ServiceCategory;
                        if (serviceCategory != null)
                        {
                            db.ServiceCategories.Remove(serviceCategory);
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

        private void BtnOkError_Click(object sender, RoutedEventArgs e)
        {
            errorDialog.IsOpen = false;
        }
    }
}
