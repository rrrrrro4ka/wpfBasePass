using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class User
    {
        
        public int id { get; set; }
        public string pass { get; set; }

        public ICollection<UserInfo> UserInfoes { get; set; }
        public User()
        {
            UserInfoes = new List<UserInfo>();
        }
    }
}
