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
using WpfApp1.Models;
using WpfApp1.Logic;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AddObject.xaml
    /// </summary>
    public partial class AddObject : Page
    {
        public MainWindow mainwindow;
        private DBcontext db = new DBcontext();
        

        public AddObject(MainWindow _mainWindow)
        {
            
            InitializeComponent();
            mainwindow = _mainWindow;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainwindow.OpenPages(MainWindow.pages.cabinet);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Systema.Text.Length > 0 && Login.Text.Length > 0 && Pass.Password.Length > 0)
                {
                    UserInfo userinformation = new UserInfo();
                    UserServiceCabinet usercabinet = new UserServiceCabinet(db);
                    userinformation.id = CabinetPage.uID;
                    userinformation.authSystem = Systema.Text;
                    userinformation.login = Login.Text;
                    userinformation.passwordSystem = Pass.Password;
                    userinformation.web = website.Text;
                    usercabinet.CreateUser(userinformation);
                    MessageBox.Show("Система добавлена.");
                }
                else
                {
                    MessageBox.Show("Не все обязательные поля заполнены.");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show($"При сохранении системы произошла ошибка {except.Message}");
            }
        }
    }
}
