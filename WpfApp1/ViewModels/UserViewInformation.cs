using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{

    class UserViewInformation : BaseUserViewModel
    {
        private int _id;
        private string _usersystem;
        private string _loginsystem;
        private string _passsystem;
        private string _website;
        private List<UserInfo> templist;

        public UserViewInformation(List<UserInfo> userInfo)
        {
            templist = userInfo;
        }
        public UserViewInformation()
        {

        }
        /// <summary>
        /// СВойство  котором формируется лист UserViewInformation из листа UserInfo :D
        /// </summary>
        public List<UserViewInformation> Listout
        {
            get
            {
                List<UserViewInformation> listout = new List<UserViewInformation>();
                foreach (var us in templist)
                {
                    listout.Add(new UserViewInformation()
                    {
                        _id = us.id,
                        _usersystem = us.authSystem,
                        _loginsystem = us.login,
                        _passsystem = us.passwordSystem,
                        _website = us.web

                    });                      
                }
                return listout;
            }
            set { }
        }
        /// <summary>
        /// Свойство для получения/записи системы пользователя
        /// </summary>
        public string usersystem
        {
            get { return _usersystem; }
            set
            {
                _usersystem = value;
                OnPropertyChanged(nameof(usersystem));
            }
        }
        /// <summary>
        /// Свойство для получения/записи логина из системы пользователя
        /// </summary>
        public string loginsystem
        {
            get { return _loginsystem; }
            set
            {
                _loginsystem = value;
                OnPropertyChanged(nameof(loginsystem));
            }
        }
        /// <summary>
        /// Свойство для получения/записи пароля системы пользователя
        /// </summary>
        public string passwordsystem
        {
            get { return _passsystem; }
            set
            {
                _passsystem = value;
                OnPropertyChanged(nameof(passwordsystem));
            }
        }
        /// <summary>
        /// Свойство для получения/записи сайта системы пользователя
        /// </summary>
        public string website
        {
            get { return _website; }
            set
            {
                _website = value;
                OnPropertyChanged(nameof(website));
            }
        }  
    }

    
}
