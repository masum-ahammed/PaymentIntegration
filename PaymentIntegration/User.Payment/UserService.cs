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

        public async Task AddDefaultPaymentMethodsToUser(string userId)
        {
            var allPaymentMethods = _UserDataAccess.GetAllPaymentMethods();
            List<UserPaymentMethod> userPaymentMethods = new List<UserPaymentMethod>();
            foreach (var paymentMethod in allPaymentMethods)
            {
                userPaymentMethods.Add(new UserPaymentMethod() { PaymentMethodId = paymentMethod.Id, UserId = userId, IsPrimary = paymentMethod.Name == PaymentMethodType.PayPal.ToString() , Isverified = paymentMethod.Name == PaymentMethodType.PayPal.ToString() });

            }
           await  _UserDataAccess.SaveUserPaymentMethods(userPaymentMethods);
        }
    }
}
