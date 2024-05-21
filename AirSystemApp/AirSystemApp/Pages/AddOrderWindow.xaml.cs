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
    /// Логика взаимодействия для AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {

        private class AirOrderIn
        {
            public int AirOrderId { set; get; }
            public string AirName { set; get; }
            public double Price { set; get; }
        }

        private class ServiceOrderIn
        {
            public int ServiceOrderId { set; get; }
            public string ServiceName { set; get; }
            public double ServicePrice { set; get; }
        }
        private class ClientInfo
        {
            public int ClientID { set; get; }
            public string LastName { set; get; }
            public double Phone { set; get; }
        }
        Order order=null;
        int ClientID = -1;
        public AddOrderWindow()
        {
            InitializeComponent();
            LoadStatus();
            LoadAir();
            LoadClients();
            LoadServices();
            DisableComponent();
        }

        void DisableComponent()
        {
            rwOrderNum.Height = new GridLength(0);
            rwPrice.Height = new GridLength(0);
            rwAirs.Height = new GridLength(0);
            rwServices.Height = new GridLength(0);
            rwAirsName.Height = new GridLength(0);
            rwServicesName.Height = new GridLength(0);
            this.Height = 420;
            btnSave.Content = "Создать";
        }

        void EnableComponent()
        {
            rwAirs.Height = new GridLength(180);
            rwServices.Height = new GridLength(180);
            rwAirsName.Height = new GridLength(30);
            rwServicesName.Height = new GridLength(30);
            rwOrderNum.Height = new GridLength(40);
            rwPrice.Height = new GridLength(40);
            this.Height = 840;
            btnSave.Content = "Сохранить";
        }

        public AddOrderWindow(int orderId)
        {
            InitializeComponent();
            LoadStatus();
            LoadOrder(orderId);
            LoadAir();
            LoadAirOrder();
            LoadClients();
            LoadServices();
            LoadServiceOrder();

        }


        void LoadClients()
        {
            using (AirDBEntities db = new AirDBEntities())
            {
                lbClient.ItemsSource = db.Clients.ToList();
            }
        }
        void LoadStatus()
        {
            using (AirDBEntities db = new AirDBEntities())
            {
                tbStatus.ItemsSource = db.Status.ToList();
            }
        }

        void LoadServices()
        {
            using (AirDBEntities db = new AirDBEntities())
            {
                db.Services.Load();
                lbService.ItemsSource = db.Services.ToList();
            }

            }
        void LoadAir()
        {
            string imagePath = AppDomain.CurrentDomain.BaseDirectory + "Images/";
            using (AirDBEntities db = new AirDBEntities())
            {
                db.Airs.Load();
                List<Air> airs = db.Airs.ToList();
                foreach (Air a in airs)
                {
                    a.Photo = imagePath + a.Photo;
                }

                lbAirs.ItemsSource = airs;
            }
        }

        void LoadAirOrder()
        {
            using (AirDBEntities db = new AirDBEntities())
            {
                var airOrdersList = (from air in db.Airs
                                     join airorder in db.AirOrders on air.AirID equals airorder.AirID
                                     where airorder.OrderID == order.OrderID
                                     select new AirOrderIn
                                     {
                                         AirOrderId = airorder.AirOrderID,
                                         AirName = air.AirName,
                                         Price = air.Price
                                     }).ToList();
                dtAirs.ItemsSource = airOrdersList;
            }
        }

        void LoadServiceOrder()
        {
            using (AirDBEntities db = new AirDBEntities())
            {
                var serviceOrdersList = (from service in db.Services
                                         join serviceorder in db.ServiceOrders on service.ServiceID equals serviceorder.ServiceID
                                         where serviceorder.OrderID == order.OrderID
                                         select new ServiceOrderIn
                                         {
                                             ServiceOrderId = serviceorder.ServiceOrderID,
                                             ServiceName = service.ServiceName,
                                             ServicePrice = service.ServicePrice
                                         }).ToList();


                dtServices.ItemsSource = serviceOrdersList;

            }
        }
            void LoadOrder(int orderId)
        {
            using (AirDBEntities db = new AirDBEntities())
            {
                order = db.Orders.Include(order => order.Client).Include(order => order.AirOrders).Include(order => order.ServiceOrders).FirstOrDefault(d => d.OrderID == orderId);
                tbStatus.ItemsSource = db.Status.ToList();
            }
            ClientID = Convert.ToInt32(order.ClientID);
            tbOrderId.Text = order.OrderID.ToString();
            tbClient.Text =$" {order.Client.LastName}  {order.Client.FirstName}\nтелефон: {order.Client.Phone}, email:{order.Client.Email}";
            //tbClient.Text = 
            //order.Client.LastName;
            tbStartDate.SelectedDate = order.StartDate;
            tbEndDate.SelectedDate = order.EndDate;


            tbStatus.SelectedValue = order.StatusID;
            tbAddress.Text = order.Address;
            tbTotalPrice.Value = order.TotalPrice;
            
               

           
        }


        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       

      
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = false;
        }

        private void dtAirs_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() +1).ToString();
        }

        private void dtServices_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void BtnAddAir_Click(object sender, RoutedEventArgs e)
        {
            AddAirHost.IsOpen = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            AddAirHost.IsOpen = false;
        }

        private void BtnAddOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbAirs.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < lbAirs.SelectedItems.Count; i++)
                    {
                        Air xair = lbAirs.SelectedItems[i] as Air;
                        if (xair != null)
                        {
                            int id = xair.AirID;
                            using (AirDBEntities db = new AirDBEntities())
                            {
                                AirOrder airOrder = new AirOrder
                                {
                                        OrderID = order.OrderID,
                                        AirID = id
                                };
                                db.AirOrders.Add(airOrder);
                                tbTotalPrice.Value += xair.Price; 
                                db.SaveChanges();
                                db.Orders.Attach(order);
                                order.TotalPrice = tbTotalPrice.Value;
                                db.SaveChanges();
                            }
                        }
                    }
                }
                AddAirHost.IsOpen = false;
                
                //MaterialDesignThemes.Wpf.DialogHost.Show("Запись вфыафыва");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                LoadAirOrder();
            }
        }

        private void BtnDeleteAir_Click(object sender, RoutedEventArgs e)
        {

            dialogDeleteAirOrder.IsOpen = true;
        }

        private void BtnServiceOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbService.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < lbService.SelectedItems.Count; i++)
                    {
                        Service xair = lbService.SelectedItems[i] as Service;
                        if (xair != null)
                        {
                            int id = xair.ServiceID;
                            using (AirDBEntities db = new AirDBEntities())
                            {
                                ServiceOrder serviceOrder = new ServiceOrder
                                {
                                    OrderID = order.OrderID,
                                    ServiceID = id
                                };
                                tbTotalPrice.Value += xair.ServicePrice;
                                db.ServiceOrders.Add(serviceOrder);
                                db.SaveChanges();
                                db.Orders.Attach(order);
                                order.TotalPrice = tbTotalPrice.Value;
                                db.SaveChanges();
                            }
                        }
                    }
                }
                AddServiceHost.IsOpen = false;

                //MaterialDesignThemes.Wpf.DialogHost.Show("Запись вфыафыва");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                LoadServiceOrder();
            }
        }

        private void BtnServiceCancel_Click(object sender, RoutedEventArgs e)
        {
            AddServiceHost.IsOpen = false;
        }

        private void BtnAddService_Click(object sender, RoutedEventArgs e)
        {
            AddServiceHost.IsOpen = true;
        }

        private void BtnYesDeleteAirOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dtAirs.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < dtAirs.SelectedItems.Count; i++)
                    {
                        AirOrderIn xair = dtAirs.SelectedItems[i] as AirOrderIn;
                        if (xair != null)
                        {
                            int id = xair.AirOrderId;
                            using (AirDBEntities db = new AirDBEntities())
                            {
                                AirOrder x = db.AirOrders.FirstOrDefault(a => a.AirOrderID == id);
                                db.AirOrders.Attach(x);
                                db.AirOrders.Remove(x);
                                db.SaveChanges();
                                tbTotalPrice.Value -= xair.Price;
                                db.Orders.Attach(order);
                                order.TotalPrice = tbTotalPrice.Value;
                                db.SaveChanges();
                            }
                        }
                    }
                }


                //MaterialDesignThemes.Wpf.DialogHost.Show("Запись вфыафыва");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                LoadAirOrder();
                dialogDeleteAirOrder.IsOpen = false;
            }

        }

        private void BtnCancelDeleteAirOrder_Click(object sender, RoutedEventArgs e)
        {
            dialogDeleteAirOrder.IsOpen = false;
        }

        private void BtnDeleteService_Click(object sender, RoutedEventArgs e)
        {
            dialogDeleteServiceOrder.IsOpen = true;

        }

        private void BtnCancelDeleteServiceOrder_Click(object sender, RoutedEventArgs e)
        {
            dialogDeleteServiceOrder.IsOpen = false;
        }

        private void BtnYesDeleteServiceOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dtServices.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < dtServices.SelectedItems.Count; i++)
                    {
                        ServiceOrderIn xair = dtServices.SelectedItems[i] as ServiceOrderIn;
                        if (xair != null)
                        {
                            int id = xair.ServiceOrderId;
                            using (AirDBEntities db = new AirDBEntities())
                            {
                                ServiceOrder x = db.ServiceOrders.FirstOrDefault(a => a.ServiceOrderID == id);
                                db.ServiceOrders.Attach(x);
                                db.ServiceOrders.Remove(x);
                                db.SaveChanges();
                                tbTotalPrice.Value -= xair.ServicePrice;
                                db.Orders.Attach(order);
                                order.TotalPrice = tbTotalPrice.Value;
                                db.SaveChanges();
                            }
                        }
                    }
                }


                //MaterialDesignThemes.Wpf.DialogHost.Show("Запись вфыафыва");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                LoadServiceOrder();
                dialogDeleteServiceOrder.IsOpen = false;
            }
        }


        private string CheckFields()
        {
            //tbOrderId.Text = order.OrderID.ToString();
            ////tbClient.Text =$" {order.Client.LastName}  {order.Client.FirstName}\nтелефон: {order.Client.Phone}, email:{order.Client.Email}";
            //tbClient.Text =
            //order.Client.LastName;
            //tbStartDate.SelectedDate = order.StartDate;
            //tbEndDate.SelectedDate = order.EndDate;


            //tbStatus.SelectedValue = order.StatusID;
            //tbAddress.Text = order.Address;
            //tbTotalPrice.Value = order.TotalPrice;
            string s = "";
            if (ClientID == -1)
                s += "Клиент не выбран\n";
            if (tbStartDate.Text == "")
                s += "Дата не выбрана\n";
            if (tbEndDate.Text == "")
                s += "Дата не выбрана\n";
            if (tbStatus.SelectedIndex == -1)
                s += "Статус пустое\n";
            if (tbAddress.Text == "")
                s += "Адрес не указан\n";

            return s;
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string fields = CheckFields();
            if (fields != "")
            {
                tbError.Text = CheckFields();
                DialogHost.IsOpen = true;
                return;
            }
            if (order == null)
            {
                AddOrder();
            }
            else { UpdateOrder(); }

        }

        void AddOrder()
        {
            using (AirDBEntities db = new AirDBEntities())
            {
                Order new_order = new Order
                {
                    ClientID = ClientID,
                    StartDate = Convert.ToDateTime(tbStartDate.SelectedDate),
                    EndDate = Convert.ToDateTime(tbEndDate.SelectedDate),
                    StatusID = Convert.ToInt32(tbStatus.SelectedValue),
                    Address = tbAddress.Text,
                    TotalPrice = tbTotalPrice.Value
                };
                db.Orders.Add(new_order);
                db.SaveChanges();
                tbError.Text = "Запись добавлена";
                order = new_order;
                LoadOrder(order.OrderID);
                EnableComponent();
                DialogHost.IsOpen = true;
            }
        }

        void UpdateOrder()
        {
            using (AirDBEntities db = new AirDBEntities())
            {
                db.Orders.Attach(order);

                order.ClientID = ClientID;
                    order.StartDate = Convert.ToDateTime(tbStartDate.SelectedDate);
                order.EndDate = Convert.ToDateTime(tbEndDate.SelectedDate);
                order.StatusID = Convert.ToInt32(tbStatus.SelectedValue);
                order.Address = tbAddress.Text;
                order.TotalPrice = tbTotalPrice.Value;


                db.SaveChanges();
                tbError.Text = "Запись обновлена";
                DialogHost.IsOpen = true;
            }
        }

        private void btnLoadClient_Click(object sender, RoutedEventArgs e)
        {
            hostLoadClient.IsOpen = true;

        }

        private void btnClientOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbClient.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < lbClient.SelectedItems.Count; i++)
                    {
                        Client xair = lbClient.SelectedItems[i] as Client;
                        if (xair != null)
                        {
                            ClientID = xair.ClientID;

                            tbClient.Text = $" {xair.LastName}  {xair.FirstName}\nтелефон: {xair.Phone}, email:{xair.Email}";
                        }
                    }
                }


                //MaterialDesignThemes.Wpf.DialogHost.Show("Запись вфыафыва");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            hostLoadClient.IsOpen = false;
        }

        private void btnClientCancel_Click(object sender, RoutedEventArgs e)
        {
            hostLoadClient.IsOpen = false;
        }
    }
}


