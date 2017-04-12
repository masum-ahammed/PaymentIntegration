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

        public bool SendPayment(string userEmail, int points, string userId)
        {
            PaypalPaymentGateway paypalGatway = new PaypalPaymentGateway();
            string paymentNote = string.Format("You have redeemed {0} points", points);
            decimal amount = (decimal)(points * 1.0);
            PayoutBatchHeader payoutBatchHeader = paypalGatway.SendPayment(userEmail, amount, paymentNote);
            SavePaymentTransaction(userId, PaymentMethodType.PayPal, payoutBatchHeader.payout_batch_id, amount, PaymentType.Redeem);
            UserGameInfo gameInfo = _GameDataAccess.GetGameInfoByUser(userId);
            int point = gameInfo.Point - points;
            _GameDataAccess.SaveUserPoint(userId, point);

            return true;
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
        public int SavePaymentTransaction(string userId, PaymentMethodType paymentMethod, string paymentId, decimal amount, PaymentType paymentType)
        {
            PaymentTransaction paymentTransaction = new PaymentTransaction();
            paymentTransaction.Amount = amount;
            paymentTransaction.DateTime = DateTime.Now;
            paymentTransaction.PaymentId = paymentId;
            paymentTransaction.PaymentMethod = paymentMethod;
            paymentTransaction.UserId = userId;
            paymentTransaction.PaymentType = paymentType;
            return _PaymentDataAccess.SavePaymentTransaction(paymentTransaction);
        }

        public decimal GetTotalSpentByUser(string userId)
        {
            return _PaymentDataAccess.GetTotalSpentByUser(userId);
        }
    }
}
