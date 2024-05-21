﻿using System;
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
using AirSystemApp.Models;
using System.Text.RegularExpressions;
using System.Data.Entity;

namespace AirSystemApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        AirDBEntities db;

        public ClientPage()
        {
            InitializeComponent();
            LoadData();
        }

        void LoadData()
        {
            db = new AirDBEntities();
            db.Clients.Load();
           
            dtClients.ItemsSource = db.Clients.Local.ToBindingList();
        }

        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dtClients.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < dtClients.SelectedItems.Count; i++)
                    {
                        Client client = dtClients.SelectedItems[i] as Client;
                        if (client != null)
                        {
                            db.Clients.Remove(client);
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

        

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow clientAddPage = new AddClientWindow();
            
            if (clientAddPage.ShowDialog() == true)
            {
                db.Dispose();
                LoadData();
                tbMessage.Text = "Запись добавлена";
                resultDialog.IsOpen = true;
                
            }

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dtClients.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < dtClients.SelectedItems.Count; i++)
                    {

                        Client client = dtClients.SelectedItems[i] as Client;
                        if (client != null)
                        {

                            AddClientWindow clientAddPage = new AddClientWindow(client.ClientID);

                            if (clientAddPage.ShowDialog() == true)
                            {
                                db.Dispose();
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