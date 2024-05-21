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

namespace AirSystemApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void BtnServiceType_Click(object sender, RoutedEventArgs e)

        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().SingleOrDefault(x => x.IsActive);
            mainWindow.listView.SelectedIndex = -1;
            mainWindow.xFrame.Navigate(new ServiceCategoryPage());
            //NavigationService.Navigate(new ServiceCategoryPage());
        }

        private void BtnServices_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().SingleOrDefault(x => x.IsActive);
            mainWindow.listView.SelectedIndex = -1;
            mainWindow.xFrame.Navigate(new ServicePage());
        }

        private void BtnClients_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().SingleOrDefault(x => x.IsActive);
            mainWindow.listView.SelectedIndex = -1;
            mainWindow.xFrame.Navigate(new ClientPage());
        }

        private void BtnStatus_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().SingleOrDefault(x => x.IsActive);
            mainWindow.listView.SelectedIndex = -1;
            mainWindow.xFrame.Navigate(new StatusPage());
        }

        private void BtnAirCond_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().SingleOrDefault(x => x.IsActive);
            mainWindow.listView.SelectedIndex = -1;
            mainWindow.xFrame.Navigate(new AirPage());
        }

        private void BtnOrders_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().SingleOrDefault(x => x.IsActive);
            mainWindow.listView.SelectedIndex = -1;
            mainWindow.xFrame.Navigate(new OrdersPage());

        }
    }
}
