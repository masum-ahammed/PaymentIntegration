
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IUserDataAccess
    {
        List<UserPaymentMethod> GetPaymentMethodsByUser(string userId);
        List<PaymentMethod> GetAllPaymentMethods();
        Task SaveUserPaymentMethods(List<UserPaymentMethod> userPaymentMethods);
       
    }
}
