using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDataStorage.Utils;
using UserDataStorage.ViewModels;

namespace UserDataStorage.Models
{
    public class SystemInfo : ValidationSystemBase
    {
        private string _authsystem;
        private string _login;
        private string _password;
        private string _website;
        public string AuthSystem
        {
            get { return _authsystem; }
            set
            {
                if (_authsystem != value)
                {
                    IsSystemValid(value);
                    _authsystem = value;
                }
            }
        }
        public string Login
        {
            get { return _login; }
            set
            {
                if (_login != value)
                {
                    IsLoginContainsAnything(value);
                    _login = value;
                }
            }
        }
        public string PasswordSystem
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    IsPasswordContainsAnything(value);
                    _password = value;
                }
            }
        }
        public string Website
        {
            get { return _website; }
            set
            {
                _website = value;
            }
        }
    }
}

