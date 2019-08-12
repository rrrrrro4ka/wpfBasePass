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
    public class UserServiceCabinet : IUserService<UserInfo, int>
    {
        private DBcontext _db;

        public UserServiceCabinet(DBcontext db)
        {
            _db = db;
        }          
        /// <summary>
        /// Возвращает список юзеров и их информацию
        /// </summary>
        /// <returns></returns>
        public List<UserInfo> GetUsers()
        {
            return _db.UserInformation.ToList();
        }
        /// <summary>
        /// Удаляет информацию по юзеру.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteUser(int id)
        {
            UserInfo user = _db.UserInformation.FirstOrDefault(u => u.id == id);
            if (user != null)
            {
                _db.UserInformation.Remove(user);
                _db.SaveChanges();
            }
        }
        /// <summary>
        /// Создает систему пользователя в личном кабинете
        /// </summary>
        /// <param name="newuser"></param>

        public void CreateUser(UserInfo newuser)
        {
            _db.UserInformation.Add(newuser);
            _db.SaveChanges();
        }
        /// <summary>
        /// Обновляет данные по системе пользователя
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateUserinfo(UserInfo entity)
        {
            UserInfo _userinfoDB = _db.UserInformation.FirstOrDefault(u => u.id == entity.id);
            if (_userinfoDB != null)
            {
                _userinfoDB.authSystem = entity.authSystem;
                _userinfoDB.login = entity.login;
                _userinfoDB.passwordSystem = entity.passwordSystem;
                _userinfoDB.web = entity.web;
            }
            _db.Entry(_userinfoDB).State = EntityState.Modified;
            _db.SaveChanges();
        }
        /// <summary>
        /// Возвращает данные о системах конкретного пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<UserInfo> GetUserInfo(int id)
        {
            return _db.UserInformation.Where(u => u.User.id == id).ToList();
        }
    
            
    }
}
