using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace User.PaymentIntegration
{
    public class PaypalPaymentGateway
    {
        public Payment CreatePayment(decimal paymentAmount)
        {
            
           var apiContext =  PaypalConfiguration.GetAPIContext();
            //similar to credit card create itemlist and add item objects to it
            var itemList = new ItemList() { items = new List<Item>() };

            itemList.items.Add(new Item()
            {
                name = "Game Point",
                currency = "USD",
                price = paymentAmount.ToString(),
                quantity = "1",
                sku = "sku"
            });

            var payer = new Payer() { payment_method = "paypal" };

            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = PaypalConfiguration.CancelUrl,
                return_url = PaypalConfiguration.ReturnUrl
            };

            // similar as we did for credit card, do here and create details object
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = paymentAmount.ToString()
            };

            // similar as we did for credit card, do here and create amount object
            var amount = new Amount()
            {
                currency = "USD",
                total = paymentAmount.ToString(), // Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };

            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Purchasing GamePoint",
                invoice_number = Guid.NewGuid().ToString(),
                amount = amount,
                item_list = itemList
            });

            var payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a APIContext
            return payment.Create(apiContext);
        }

        public Payment ExecutePayment( string payerId, string paymentId)
        {
            var apiContext = PaypalConfiguration.GetAPIContext();
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            var payment = new Payment() { id = paymentId };
            return payment.Execute(apiContext, paymentExecution);
        }

        public PayoutBatchHeader SendPayment(string reciverEmail, decimal amount, string note)
        {
            var apiContext = PaypalConfiguration.GetAPIContext();
            var payout = new Payout
            {
                sender_batch_header = new PayoutSenderBatchHeader
                {
                    sender_batch_id = "batch_" + System.Guid.NewGuid().ToString().Substring(0, 8),
                    email_subject = "You have redeem points payment"
                },
                
                items = new List<PayoutItem>
                {
                    new PayoutItem
                    {
                        recipient_type = PayoutRecipientType.EMAIL,
                        amount = new Currency
                        {
                            value = decimal.Round(amount,2).ToString(),
                            currency = "USD"
                        },
                        receiver = reciverEmail,
                        note = note,
                        sender_item_id = "item_1"
                    }
                 
                    
                }
            };

            PayoutBatch createdPayout = payout.Create(apiContext, false);
            return createdPayout.batch_header;

        }
    }
}
