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
        private CreateNewPassword newPassword = new CreateNewPassword();
        private string firstPassword;
        private string secondPassword;
        private string secretWord;
        private DelegateCommand addPasswordCommand;

        public string FirstPassword
        {
            get { return firstPassword; }
            set
            {
                firstPassword = value;
                OnPropertyChanged(nameof(FirstPassword));
                addPasswordCommand.RaiseCanExecuteChanged();
            }
        }
        public string SecondPassword
        {
            get { return secondPassword; }
            set
            {
                secondPassword = value;
                OnPropertyChanged(nameof(SecondPassword));
                addPasswordCommand.RaiseCanExecuteChanged();
            }
        }
        public string SecretWord
        {
            get { return secretWord; }
            set
            {
                secretWord = value;
                OnPropertyChanged(nameof(SecretWord));
                addPasswordCommand.RaiseCanExecuteChanged();
            }
        }
        public DelegateCommand AddPasswordCommand
        {
            get
            {
                return addPasswordCommand = new DelegateCommand(SaveNewPassword, CanSaveNewPassword);
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
                newPassword.AddPassword = FirstPassword;
                newPassword.AddSecretWord = SecretWord;
                XmlService xmlPass = new XmlService();
                xmlPass.SaveUserPasswordToXml(newPassword);
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
