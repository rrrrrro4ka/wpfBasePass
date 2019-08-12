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
        /// <summary>
        /// Создан объект чтобы передавать его между страницами. Понять как правильно реализовывать???
        /// </summary>
        public static int uID { get; set; }
        public Login(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            
        }
       
        /// <summary>
        /// Кнопка войти проверяет мастер-пароль  пускает в личный кабинет
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enter_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                if (password_box.Password.Length > 0)
                {
                    List<User> users = _dbcon.Users.ToList();
                    if (users != null)
                    {


                        foreach (User us in users)
                        {
                            if (us.pass == password_box.Password && us.pass != null)
                            {
                                uID = us.id;
                                MessageBox.Show("Добро пожаловать!");
                                mainWindow.OpenPages(MainWindow.pages.cabinet);
                            }
                            else
                            {
                                MessageBox.Show("Такого пользователя нет в системе.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пользователей нет в системе. Зарегистрируйтесь.");
                    }

                }
                else
                {
                    MessageBox.Show("Введите пароль.");
                }
            } catch(Exception exc)
            {
                MessageBox.Show($"Ошибка при попытке войти в систему: {0}", exc.Message);
            }

        }
        /// <summary>
        /// Кнопка переводит на страницу регистрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainWindow.OpenPages(MainWindow.pages.registr);
            } catch(Exception exc)
            {
                MessageBox.Show($"Ошибка при попытке перейти на страницу регистрации: {0}", exc.Message);
            }
        }
    }
}
