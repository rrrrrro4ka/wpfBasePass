using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.interfaceS
{
    public interface IUserService<TType, in TKey> where TType : class
    {
        List<TType> GetUsers();
        
        void DeleteUser(TKey id);
        void UpdateUserinfo(TType userinfo);
        List<TType> GetUserInfo(TKey id);
    }

}
