using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.interfaceS;
using WpfApp1.Models;

namespace WpfApp1.Logic
{
    public class UserService
    {
        private DBcontext _db;

        public UserService(DBcontext db) 
        {
            _db = db;
        }                
        public void CreateUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }
        public User GetUserPass(int id)
        {
            return _db.Users.FirstOrDefault(u => u.id == id);
        }

        public List<User> GetUsers()
        {
            return _db.Users.ToList();
        }

    }
}
