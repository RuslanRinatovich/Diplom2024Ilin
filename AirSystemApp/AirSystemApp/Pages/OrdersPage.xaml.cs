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
using System.ComponentModel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections;


namespace AirSystemApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для AirPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        //AirDBEntities db;
        ICollectionView collectionView;
        string imagePath = AppDomain.CurrentDomain.BaseDirectory + "Images/";
        
        public OrdersPage()
        {
            InitializeComponent();
            LoadData();
        }

        void LoadData()
        {
            using (AirDBEntities db = new AirDBEntities())
            {
                db.Orders.Load();
                var orders = db.Orders.Include(order => order.Client).Include(order => order.Status).ToList();
                
                collectionView = CollectionViewSource.GetDefaultView(orders);
                dtOrders.ItemsSource = collectionView;
                
            }
        }
                       
        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dtOrders.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < dtOrders.SelectedItems.Count; i++)
                    {
                        Order order = dtOrders.SelectedItems[i] as Order;
                        if (order != null)
                        {
                            int id = order.OrderID;
                            using (AirDBEntities db = new AirDBEntities())
                            {
                                Order a = db.Orders.FirstOrDefault(x => x.OrderID == id);
                                db.Orders.Remove(a);
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
                DialogHost.IsOpen = false;
                // MessageBox.Show("Удаление прервано, есть связанные записи", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                tbError.Text = "Удаление прервано, есть связанные записи";
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
            AddOrderWindow addOrderWindow = new AddOrderWindow();

            addOrderWindow.ShowDialog();
                LoadData();
                    }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
                if (dtOrders.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < dtOrders.SelectedItems.Count; i++)
                    {

                        Order order = dtOrders.SelectedItems[i] as Order;
                        if (order != null)
                        {

                            AddOrderWindow addOrderWindow = new AddOrderWindow(order.OrderID);

                        addOrderWindow.ShowDialog();
                        LoadData();
                    }
                    }

                }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Ошибка");

            //}
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dtOrders.SelectedItems.Count > 0)
                    DialogHost.IsOpen = true;
        }

        private void BtnOkResult_Click(object sender, RoutedEventArgs e)
        {
            resultDialog.IsOpen = false;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {

            int selind = cmbSearchType.SelectedIndex;
            if (tbSearchID.Text == "")
            {
                collectionView.Filter = null;
                return;
            }
            switch (selind)
            {
                case 0:
                    FilterByID(tbSearchID.Text);
                    break;
                case 1:
                    FilterByClient(tbSearchID.Text);
                    break;
                case 2:
                    FilterByDate(tbSearchID.Text);
                    break;
                case 3:
                    FilterByAddress(tbSearchID.Text);
                    break;
                default:   collectionView.Filter = null; break;
            }
           
            
               


        }
        void FilterByID(string s)
        {
            int id = -1;
            bool b = int.TryParse(s, out id);
            if (!b)
                collectionView.Filter = null;

            collectionView.Filter = item =>
            {
                Order x = item as Order;
                return x.OrderID == id;
                
            };
            collectionView.Refresh();
        }

        void FilterByClient(string s)
        {
            collectionView.Filter = item =>
            {
                Order x = item as Order;
                //return x.OrderID == id;
                return x.Client.LastName.ToLower().Contains(s.ToLower());
            };
            collectionView.Refresh();
        }

        void FilterByAddress(string s)
        {
            collectionView.Filter = item =>
            {
                Order x = item as Order;
                //return x.OrderID == id;
                return x.Address.ToLower().Contains(s.ToLower());
            };
            collectionView.Refresh();
        }

        void FilterByDate(string s)
        {
            DateTime y = DateTime.Now;

            bool b = DateTime.TryParse(s, out y);
            if (b == false)
            {
                return;
            }
            collectionView.Filter = item =>
            {
                Order x = item as Order;
                //return x.OrderID == id;
                return x.StartDate >= y;
            };
            collectionView.Refresh();
        }

        private void BtnClearSearch_Click(object sender, RoutedEventArgs e)
        {
            
            collectionView.Filter = null;
            collectionView.Refresh();

        }

        private void BtnExcel_Click(object sender, RoutedEventArgs e)
        {
            PrintExcel();

        }

        private void PrintExcel()
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + "\\" + "Orders" + ".xltx";
            Excel.Application xlApp = new Excel.Application();
            Excel.Worksheet xlSheet = new Excel.Worksheet();
            try
            {
                //добавляем книгу
                xlApp.Workbooks.Open(fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                          Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                          Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                          Type.Missing, Type.Missing);
                //делаем временно неактивным документ
                xlApp.Interactive = false;
                xlApp.EnableEvents = false;
                Excel.Range xlSheetRange;
                //выбираем лист на котором будем работать (Лист 1)
                xlSheet = (Excel.Worksheet)xlApp.Sheets[1];
                //Название листа
                xlSheet.Name = "Список заявок";
                int row = 2;
                int i = 0;
                
              
                if (dtOrders.Items.Count > 0)
                {
                    for (i = 0; i < dtOrders.Items.Count; i++)
                    {

                        Order order = dtOrders.Items[i] as Order;
                        
                        xlSheet.Cells[row, 1] = (i + 1).ToString();
                       // DateTime y = Convert.ToDateTime(dtOrders.Rows[i].Cells[1].Value);
                        xlSheet.Cells[row, 2] = order.OrderID.ToString();
                        xlSheet.Cells[row, 3] = order.StartDate.ToShortDateString();
                        xlSheet.Cells[row, 4] = order.EndDate.ToShortDateString();
                        xlSheet.Cells[row, 5] = order.Client.LastName.ToString();
                        xlSheet.Cells[row, 6] = order.Status.StatusName.ToString();
                        xlSheet.Cells[row, 7] = order.Address.ToString();
                        xlSheet.Cells[row, 8] = order.TotalPrice.ToString();
                        row++;
                        Excel.Range r = xlSheet.get_Range("A" + row.ToString(), "H" + row.ToString());
                        r.Insert(Excel.XlInsertShiftDirection.xlShiftDown);
                    }
                }
                row--;
                xlSheetRange = xlSheet.get_Range("A2:H" + (row+1).ToString(), Type.Missing);
                xlSheetRange.Borders.LineStyle = true;
                xlSheet.Cells[row+1, 8] = "=SUM(H2:h" + row.ToString() + ")";
                
                xlSheet.Cells[row + 1, 7] = "ИТОГО:";
                row++;
               
                //выбираем всю область данных*/
                xlSheetRange = xlSheet.UsedRange;
                //выравниваем строки и колонки по их содержимому
                //xlSheetRange.Columns.AutoFit();
                //xlSheetRange.Rows.AutoFit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                //Показываем ексель
                xlApp.Visible = true;
                xlApp.Interactive = true;
                xlApp.ScreenUpdating = true;
                xlApp.UserControl = true;
            }
        }

        private void BtnOkError_Click(object sender, RoutedEventArgs e)
        {
            errorDialog.IsOpen = false;

        }
    }
}