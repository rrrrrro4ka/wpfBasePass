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
    class ChangeUserPasswordView : BaseUserViewModel
    {
        private ChangeUserPassword new_password = new ChangeUserPassword();
        private string first_password;
        private string second_password;
        private string new_secret_word;
        private string secret_word;
        private DelegateCommand change_password_command;

        public string FirstPassword
        {
            get { return first_password; }
            set
            {
                first_password = value;
                OnPropertyChanged(nameof(FirstPassword));
                change_password_command.RaiseCanExecuteChanged();
            }
        }
        public string SecondPassword
        {
            get { return second_password; }
            set
            {
                second_password = value;
                OnPropertyChanged(nameof(SecondPassword));
                change_password_command.RaiseCanExecuteChanged();
            }
        }
        public string NewSecretWord
        {
            get { return new_secret_word; }
            set
            {
                new_secret_word = value;
                OnPropertyChanged(nameof(NewSecretWord));
                change_password_command.RaiseCanExecuteChanged();
            }
        }
        public string SecretWord
        {
            get { return secret_word; }
            set
            {
                secret_word = value;
                OnPropertyChanged(nameof(SecretWord));
                change_password_command.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Команда для кнопки сохранения измененного пароля.
        /// </summary>
        public DelegateCommand ChangePasswordCommand
        {
            get
            {
                return change_password_command = new DelegateCommand(ChangePassword, CanChangePassword);
            }
        }

        /// <summary>
        /// Метод проверяет одинаковые ли пароли, совпадают ли секретные слова из файла и "Старое секретное слово"
        /// и сохраняет измененный пароль в xml.
        /// Закрывает текущее окно и создаёт новый mainwindow.
        /// </summary>
        /// <param name="args"></param>
        private void ChangePassword(object args)
        {
            XmlService xmlForSecret = new XmlService();
            bool compareSecret = xmlForSecret.CompareSecretWords(SecretWord);
            if (compareSecret)
            {
                if (FirstPassword == SecondPassword)
                {
                    new_password.Password = FirstPassword;
                    new_password.NewSecretWord = NewSecretWord;
                    XmlService changePass = new XmlService();
                    changePass.SaveUserPasswordToXml(new_password);
                    MainWindow Main = new MainWindow();
                    foreach (Window window in App.Current.Windows)
                    {
                        if (window is ChangePasswordWindow)
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
            else
            {
                MessageBox.Show("Секретное слово неверно.");
            }
        }

        /// <summary>
        /// Метод проверяет заполнены ли все необходимые поля на форме. Если всё ок, даёт возможнось активировать кнопку сохранения 
        /// через delegatecommand.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private bool CanChangePassword(object args)
        {
            if (String.IsNullOrEmpty(FirstPassword) || String.IsNullOrEmpty(SecondPassword)
                || String.IsNullOrEmpty(SecretWord) || String.IsNullOrEmpty(NewSecretWord))
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
