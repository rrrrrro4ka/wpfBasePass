using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDataStorage.Logic;
using UserDataStorage.Utils;
using System.Windows;

namespace UserDataStorage.ViewModels
{
    class MainWindowViewModel : BaseUserViewModel
    {
        private string userpassword;
        private DelegateCommand into_user_account;
        public string UserPassword
        {
            get { return userpassword; }
            set
            {
                userpassword = value;
                OnPropertyChanged(nameof(UserPassword));
            }
        }
        /// <summary>
        /// Команда на кнопку входа в личный кабинет.
        /// </summary>
        public DelegateCommand IntoUserAccount
        {
            get
            {
                return into_user_account = new DelegateCommand(param => LoginUserAccount());
            }
        }
        private bool CheckUserPassword()
        {
            XmlService xmlPass = new XmlService();
            return xmlPass.CompareUserPassword(userpassword);
        }
        /// <summary>
        /// Метод выполняемый при нажатии на кнопку авторизации. Проверяются пароли на совпадение, 
        /// если всё ок заходим в ЛК. 
        /// </summary>
        private void LoginUserAccount()
        {
            if (CheckUserPassword())
            {
                UserAccountConfiguration userAccount = new UserAccountConfiguration();
                foreach (Window window in App.Current.Windows)
                {
                    if (window is MainWindow)
                    {
                        window.Close();
                    }
                }
                userAccount.Show();
            }
            else
            {
                MessageBox.Show("Пароль неверный!");
            }
        }

    }
}
