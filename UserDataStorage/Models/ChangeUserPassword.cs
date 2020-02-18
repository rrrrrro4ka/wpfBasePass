using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDataStorage.Models
{
    public class ChangeUserPassword
    {
        private string nePpassword;
        private string newSecretWord;
        public string Password
        {
            set { nePpassword = value; }
            get { return nePpassword; }
        }
        public string NewSecretWord
        {
            set { newSecretWord = value; }
            get { return newSecretWord; }
        }
    }
}
