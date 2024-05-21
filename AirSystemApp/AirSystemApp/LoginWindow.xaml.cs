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

namespace AirSystemApp
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = true;
            
        }
        private void BtnExitM_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((tbLogin.Text == "ADMIN") && (psbPassword.Password == "1"))
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Owner = this;
                this.Hide();
                mainWindow.Show();
            }
            else
            {
                tbError.Text = "Не верный логин или пароль!";
                ErrorHost.IsOpen = true;
            }
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            ErrorHost.IsOpen = false;
        }
    }
}
