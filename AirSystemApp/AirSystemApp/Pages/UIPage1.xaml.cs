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
using MaterialDesignThemes.Wpf;

namespace AirSystemApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для UIPage1.xaml
    /// </summary>
    public partial class UIPage1 : Page
    {

        public UIPage1()
        {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = true;
        }

        private void BtnMaximize_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().SingleOrDefault(x => x.IsActive);
            if (mainWindow.WindowState == WindowState.Normal)
            {
                mainWindow.WindowState = WindowState.Maximized;
                btnMaximize.Content = new PackIcon { Kind = PackIconKind.WindowRestore };
            }
            else             if (mainWindow.WindowState == WindowState.Maximized)
            {
                mainWindow.WindowState = WindowState.Normal;
                btnMaximize.Content = new PackIcon { Kind = PackIconKind.WindowMaximize };
                
            }
           // btnMaximize.SetResourceReference(Control.ForegroundProperty, "gray");
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().SingleOrDefault(x => x.IsActive);
            mainWindow.WindowState = WindowState.Minimized;
        }

        private void ItemServiceCategory_Selected(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new ServiceCategoryPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new StartPage());
        }

        private void ItemHome_Selected(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new StartPage());
        }

        private void BtnExitM_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().SingleOrDefault(x => x.IsActive);
            mainWindow.Owner.Close();
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
           
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().SingleOrDefault(x => x.IsActive);
            mainWindow.Close();
            mainWindow.Owner.Show();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = false;
        }

        private void BtnCancelLogout_Click(object sender, RoutedEventArgs e)
        {
            DialogHostLogout.IsOpen = false;
        }

        private void ItemLogout_Selected(object sender, RoutedEventArgs e)
        {
            DialogHostLogout.IsOpen = true;
        }

        private void ItemLogout_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DialogHostLogout.IsOpen = true;
        }

        private void ItemServices_Selected(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new ServicePage());
        }

        private void ItemStatus_Selected(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new StatusPage());
        }

        private void ItemClients_Selected(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new ClientPage());
        }

        private void ItemAirs_Selected(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new AirPage());
        }

        private void ItemOrders_Selected(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new OrdersPage());
        }
    }
}
