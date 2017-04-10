using Data.Model;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService
    {
        IUserDataAccess _UserDataAccess;
        public UserService()
        {
            _UserDataAccess = new UserDataAccess();
        }
        public List<UserPaymentMethod> GetPaymentMethodsByUser(string userId)
        {
           return  _UserDataAccess.GetPaymentMethodsByUser(userId);
        }
    }
}
