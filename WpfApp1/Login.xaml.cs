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
using WpfApp1.Models;
using WpfApp1.Logic;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public MainWindow mainWindow;
        public DBcontext _dbcon = new DBcontext();
        
        public Login(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
        }

        private void enter_Click(object sender, RoutedEventArgs e)
        {
            if(password_box.Password.Length > 0)
            {
                List<User> users = _dbcon.Users.ToList();
                foreach(User us in users)
                {
                    if (us.pass == password_box.Password)
                    {
                        MessageBox.Show("Добро пожаловать!");
                    }
                    else
                    {
                        MessageBox.Show("Такого пользователя нет в системе.");
                    }
                }
             
            }
            else
            {
                MessageBox.Show("Введите пароль.");
            }

        }

        private void reg_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPages(MainWindow.pages.registr);
        }
    }
}
