using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PaymentDataAccess:IPaymentDataAccess
    {
        public decimal GetTotalSpentByUser(string userId)
        {
            using (DataDbContext dbContext = new DataDbContext())
            {
                return dbContext.PaymentTransactions.Where(x => x.UserId == userId).Sum(x => x.Amount);
            }
        }

        public int SavePaymentTransaction(PaymentTransaction paymentTransaction)
        {
            using (DataDbContext dbContext = new DataDbContext())
            {
                dbContext.PaymentTransactions.Add(paymentTransaction);
                return dbContext.SaveChanges();
            }
        }

      
    }
}
