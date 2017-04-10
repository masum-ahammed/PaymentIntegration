using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.PaymentIntegration
{
    public class PaymentService
    {
        public PaymentService()
        {

        }
       
        public int SavePaymentTransaction(string userId, PaymentMethodType paymentMethod, string paymentId, decimal amount)
        {
            using (DataDbContext dbContext = new DataDbContext())
            {
                PaymentTransaction paymentTransaction = new PaymentTransaction();
                paymentTransaction.PaymentMethod = paymentMethod;
                paymentTransaction.PaymentId = paymentId;
                paymentTransaction.UserId = userId;
                paymentTransaction.Amount = amount;
                paymentTransaction.DateTime = DateTime.Now;

                dbContext.PaymentTransactions.Add(paymentTransaction);
                
               return dbContext.SaveChanges();
            }
        }

        
    }
}
