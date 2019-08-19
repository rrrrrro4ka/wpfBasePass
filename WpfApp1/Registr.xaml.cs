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
using WpfApp1.Logic;
using WpfApp1.Models;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for WindowWork.xaml
    /// </summary>
    public partial class Registr : Page
    {
        public MainWindow mainWindow;
        public DBcontext _db = new DBcontext();
        public Registr(MainWindow _mainWindow)
        {
            InitializeComponent();

            mainWindow = _mainWindow;
        }
        /// <summary>
        /// Кнопка открыия фрейма с авторизацией при нажатии "Отмена"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bad_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPages(MainWindow.pages.login);
        }

        /// <summary>
        /// кнопка регистрации пользователя со всем валидациями
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void registr_Click(object sender, RoutedEventArgs e)
        {
            //  Проверяем есть ли что-то в поле с паролем
            
            if(password.Text.Length > 0)
            {
                if(password_copy.Text.Length > 0)
                {

                }
                else
                {
                    MessageBox.Show("Повторите пароль.");
                }
            }
            else
            {
                MessageBox.Show("Введите пароль.");
            }
            // Проверяем длину пароля, содержит ли цифры и написан ли на английской расскладке
            if(password.Text.Length >= 6)
            {
                bool english = true;
                bool number = false;
                
                for(int i=0; i < password.Text.Length; i++)
                {
                    if(password.Text[i] >= 'А' && password.Text[i] <= 'Я')
                    {
                        english = false; // Если русская раскладка
                    }
                    if(password.Text[i] >= '0' && password.Text[i] <= '9')
                    {
                        number = true; // Если есть цифры
                    }
                }
                if(!english)
                {
                    MessageBox.Show("Доступна только английская раскладка");
                } else if(!number)
                {
                    MessageBox.Show("Добавьте хотя бы одну цифру");
                }
                if (english && number)
                {
                }
            }
            

            else
            {
                MessageBox.Show("Пароль слишком короткий, минимум 6 символов");
            }
            //  Проверяем совпадение паролей и уже существующих пользователей.
            //  Если всё в порядке, то регистрируем пользователя
            if(password.Text == password_copy.Text)
            {               
                User user = new User();
                user.pass = HashCryptoService.EncryptHashPassword(password.Text);
                try
                {
                    UserService userServ = new UserService(_db);
                    List<User> users = userServ.GetUsers();
                    foreach(User us in users)
                    {
                        if (HashCryptoService.VerifyHashPassword(us.pass, password.Text))
                        {
                            MessageBox.Show("Такой пользователь уже есть в системе!");
                            return;
                        }
                     
                    }
                    userServ.CreateUser(user);
                    MessageBox.Show("Пользователь зарегистрирован");
                    mainWindow.OpenPages(MainWindow.pages.login);

                }
                catch(Exception excep)
                {
                    MessageBox.Show($"Произошла ошибка при попытке создать пользователя {excep.Message}");
                }
                
            } else
            {
                MessageBox.Show("Пароли не совпадают");
            }
        }
    }
}
