using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDataStorage.Models
{
    public class CreateNewPassword
    {
        private string newPassword;
        private string secretWord;
        public string AddPassword
        {
            set { newPassword = value; }
            get { return newPassword; }
        }
        public string AddSecretWord
        {
            set { secretWord = value; }
            get { return secretWord; }
        }
    }
}
