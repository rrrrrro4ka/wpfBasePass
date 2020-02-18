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
        private string authsystem;
        private string login;
        private string password;
        private string website;
        public string AuthSystem
        {
            get { return authsystem; }
            set
            {
                if (authsystem != value)
                {
                    IsSystemValid(value);
                    authsystem = value;
                }
            }
        }
        public string Login
        {
            get { return login; }
            set
            {
                if (login != value)
                {
                    IsLoginContainsAnything(value);
                    login = value;
                }
            }
        }
        public string PasswordSystem
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    IsPasswordContainsAnything(value);
                    password = value;
                }
            }
        }
        public string Website
        {
            get { return website; }
            set
            {
                website = value;
            }
        }
    }
}

