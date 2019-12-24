using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDataStorage.Models
{
    public class CreateNewPassword
    {
        private string new_password;
        private string secret_word;
        public string AddPassword
        {
            set { new_password = value; }
            get { return new_password; }
        }
        public string AddSecretWord
        {
            set { secret_word = value; }
            get { return secret_word; }
        }
    }
}
