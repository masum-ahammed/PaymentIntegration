using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using User.PaymentIntegration;
using User.Web.Models;
using Microsoft.AspNet.Identity;

namespace User.Web.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Paypal
        public ActionResult BuyPoint()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReceivePayment(BuyPointViewModel model)
        {
            return ProcessReceivePayment();

        }

        private ActionResult ProcessReceivePayment()
        {
            PaypalPaymentGateway paypalGatway = new PaypalPaymentGateway();


            try
            {
                var createdPayment = paypalGatway.CreatePayment();

                var links = createdPayment.links.GetEnumerator();
                string paypalRedirectUrl = null;

                while (links.MoveNext())
                {
                    Links lnk = links.Current;

                    if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                    {
                        //saving the payapalredirect URL to which user will be redirected for payment
                        paypalRedirectUrl = lnk.href;
                    }
                }

                Session.Add("PaymentId", createdPayment.id);

                return Redirect(paypalRedirectUrl);

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public ActionResult PaypalPaymentSuccess(bool cancel = false)
        {
            PaypalPaymentGateway paypalGatway = new PaypalPaymentGateway();
            string payerId = Request.Params["PayerID"];
            var executedPayment = paypalGatway.ExecutePayment(payerId, Session["PaymentId"] as string);

            if (executedPayment.state.ToLower() != "approved")
            {
                throw new InvalidOperationException("Paypal payment faild.");
            }

            return RedirectToAction("Player", "Home", new { id = User.Identity.GetUserId() });
        }
    }
}