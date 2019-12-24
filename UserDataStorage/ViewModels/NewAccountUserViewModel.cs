using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UserDataStorage.Logic;
using UserDataStorage.Models;
using UserDataStorage.Utils;

namespace UserDataStorage.ViewModels
{
    class NewAccountUserViewModel : BaseUserViewModel
    {
        private CreateNewPassword new_password = new CreateNewPassword();
        private string first_password;
        private string second_password;
        private string secret_word;
        private DelegateCommand addPassword_Command;

        public string FirstPassword
        {
            get { return first_password; }
            set
            {
                first_password = value;
                OnPropertyChanged(nameof(FirstPassword));
                addPassword_Command.RaiseCanExecuteChanged();
            }
        }
        public string SecondPassword
        {
            get { return second_password; }
            set
            {
                second_password = value;
                OnPropertyChanged(nameof(SecondPassword));
                addPassword_Command.RaiseCanExecuteChanged();
            }
        }
        public string SecretWord
        {
            get { return secret_word; }
            set
            {
                secret_word = value;
                OnPropertyChanged(nameof(SecretWord));
                addPassword_Command.RaiseCanExecuteChanged();
            }
        }
        public DelegateCommand AddPasswordCommand
        {
            get
            {
                return addPassword_Command = new DelegateCommand(SaveNewPassword, CanSaveNewPassword);
            }
        }
        /// <summary>
        /// Метод проверяет одинаковые ли пароли и сохраняет новый пароль в xml. Удаляет файл с данными, если они до этого были.
        /// Закрывает текущее окно и создаёт новый mainwindow.
        /// </summary>
        /// <param name="args"></param>
        private void SaveNewPassword(object args)
        {
            if (FirstPassword == SecondPassword)
            {
                new_password.AddPassword = FirstPassword;
                new_password.AddSecretWord = SecretWord;
                XmlService xmlPass = new XmlService();
                xmlPass.SaveUserPasswordToXml(new_password);
                xmlPass.DeleteUserSystemsXml();
                MainWindow Main = new MainWindow();
                foreach (Window window in App.Current.Windows)
                {
                    if (window is NewAccountWindow)
                    {
                        window.Close();
                    }
                }
                Main.Show();

            }
            else
            {
                MessageBox.Show("Пароли не совпадают!");
            }
        }
        /// <summary>
        /// Метод проверяет заполнены ли все необходимые поля на форме. Если всё ок, даёт возможнось активировать кнопку сохранения 
        /// через delegatecommand.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private bool CanSaveNewPassword(object args)
        {
            if (String.IsNullOrEmpty(FirstPassword) || String.IsNullOrEmpty(SecondPassword)
                || String.IsNullOrEmpty(SecretWord))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
