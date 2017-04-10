using Data.Model;
using PayPal.Api;
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
       
        public Payment MakePaypalPayment( int amount)
        {
            PaypalPaymentGateway paypalGatway = new PaypalPaymentGateway();
            return paypalGatway.CreatePayment(amount);
        }

        public Payment ExecutePaypalPayment(string payerId, string paymentId)
        {
            try
            {
                PaypalPaymentGateway paypalGatway = new PaypalPaymentGateway();
                var executedPayment = paypalGatway.ExecutePayment(payerId, paymentId);
                if (executedPayment.state.ToLower() != "approved")
                {
                    throw new InvalidOperationException("Paypal payment faild.");
                }
                return executedPayment;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
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

        public decimal GetTotalSpentByUser(string userId)
        {
            using (DataDbContext dbContext = new DataDbContext())
            {
                return dbContext.PaymentTransactions.Where(x => x.UserId == userId).Sum(x => x.Amount);
            }
        }
    }
}
