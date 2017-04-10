using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService
    {
        public List<UserPaymentMethod> GetPaymentMethodsByUser(string userId)
        {
            using (DataDbContext dbContext = new DataDbContext())
            {
              return   dbContext.UserPaymentMethods
                    .Include("PaymentMethod")
                    .Where(x => x.UserId == userId)
                    .ToList();
            }
        }
    }
}
