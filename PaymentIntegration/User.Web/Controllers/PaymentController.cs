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
        PaymentService _PaymentService;
        public PaymentController()
        {
            _PaymentService = new PaymentService();
        }
        // GET: Paypal
        public ActionResult BuyPoint()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReceivePayment(BuyPointViewModel model)
        {
            return ProcessReceivePayment(model);

        }

        private ActionResult ProcessReceivePayment(BuyPointViewModel model)
        {
            PaypalPaymentGateway paypalGatway = new PaypalPaymentGateway();


            try
            {
                var createdPayment = paypalGatway.CreatePayment(model.Amount);

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
                model.PaymentId = createdPayment.id;
                Session.Add("PaymentInfo", model);

                return Redirect(paypalRedirectUrl);

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public ActionResult PaypalPaymentSuccess(bool cancel = false)
        {
            if (cancel)
            {
                return RedirectToAction("Player", "Home", new { id = User.Identity.GetUserId() });
            }
            PaypalPaymentGateway paypalGatway = new PaypalPaymentGateway();
            string payerId = Request.Params["PayerID"];
            BuyPointViewModel paymentInfo = Session["PaymentInfo"] as BuyPointViewModel;
            var executedPayment = paypalGatway.ExecutePayment(payerId, paymentInfo.PaymentId);

            if (executedPayment.state.ToLower() != "approved")
            {
                throw new InvalidOperationException("Paypal payment faild.");
            }

            _PaymentService.SaveUserPointAndPaymentTransaction(User.Identity.GetUserId(), paymentInfo.Points, paymentInfo.Amount);
            return RedirectToAction("Player", "Home", new { id = User.Identity.GetUserId() });
        }
    }
}