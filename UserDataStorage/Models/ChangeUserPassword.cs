using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDataStorage.Models
{
    public class ChangeUserPassword
    {
        private string new_password;
        private string new_secret_word;
        public string Password
        {
            set { new_password = value; }
            get { return new_password; }
        }
        public string NewSecretWord
        {
            set { new_secret_word = value; }
            get { return new_secret_word; }
        }
    }
}
