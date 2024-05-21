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
using AirSystemApp.Pages;

namespace AirSystemApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Frame xFrame
        {
            get;
            set;
        }
        public ListView listView
        {
            get;
            set;
        }
        public MainWindow()
        {
            InitializeComponent();
            UIPage1 uIPage1 = new UIPage1();
            MainFrame.NavigationService.Navigate(uIPage1);
            xFrame = uIPage1.ContentFrame;
            listView = uIPage1.lstView;
        }
        private void Window_Closed(object sender, EventArgs e)
        {
           // Owner.Show();
        }
    }
}
