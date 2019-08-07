using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class UserInfo
    {
        //  One to one, foreignKey
        [Key]
        [ForeignKey("User")]
        public int id { get; set; }
        public string authSystem { get; set; }
        public string login { get; set; }
        public string passwordSystem { get; set; }
        public string web { get; set; }

        public User User { get; set; }
    }
}
