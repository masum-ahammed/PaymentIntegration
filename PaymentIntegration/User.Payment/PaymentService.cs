using Data.Model;
using DataAccess;
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
        IPaymentDataAccess _PaymentDataAccess;
        IGameDataAccess _GameDataAccess;
        public PaymentService()
        {
            _PaymentDataAccess = new PaymentDataAccess();
            _GameDataAccess = new GameDataAccess();
        }
       
        public Payment MakePayment(decimal amount)
        {
            PaypalPaymentGateway paypalGatway = new PaypalPaymentGateway();
            return paypalGatway.CreatePayment(amount);
        }

        public decimal AddProcessingFee(string userId,decimal amount)
        {
            UserGameInfo gameInfo = _GameDataAccess.GetGameInfoByUser(userId);
            if (gameInfo.IsEnrolledInGame)
            {
                decimal processingFee = amount * (decimal)0.0175;
                amount += processingFee;
            }
            return amount;
        }

        public Payment ExecutePayment(string payerId, string paymentId)
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
            PaymentTransaction paymentTransaction = new PaymentTransaction();
            paymentTransaction.Amount = amount;
            paymentTransaction.DateTime = DateTime.Now;
            paymentTransaction.PaymentId = paymentId;
            paymentTransaction.PaymentMethod = paymentMethod;
            paymentTransaction.UserId = userId;
            return _PaymentDataAccess.SavePaymentTransaction(paymentTransaction);
        }

        public decimal GetTotalSpentByUser(string userId)
        {
            return _PaymentDataAccess.GetTotalSpentByUser(userId);
        }
    }
}
