using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using User.PaymentIntegration;
using User.Web.Models;
using Microsoft.AspNet.Identity;
using Data.Model;
using Services;
using System.Threading.Tasks;
using System.Web.Security;

namespace User.Web.Controllers
{
    public class PaymentController : Controller
    {
        PaymentService _PaymentService;
        GameService _GameServices;
        public PaymentController()
        {
            _PaymentService = new PaymentService();
            _GameServices = new GameService();
        }
        // GET: Paypal
        public ActionResult BuyPoint()
        {
            return View();
        }

        public ActionResult RedeemPoint()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReceivePayment(BuyPointViewModel model)
        {
            return ProcessReceivePayment(model);
        }

        [HttpPost]
        public ActionResult SendPayment(RedeemPointViewModel model)
        {
            string userEmail = User.Identity.Name;
            _PaymentService.SendPayment(userEmail, model.Points, User.Identity.GetUserId());
            return RedirectToAction("Player", "Home", new { id = User.Identity.GetUserId() });
        }

        private ActionResult ProcessReceivePayment(BuyPointViewModel model)
        {
            try
            {
                model.Amount = decimal.Round(_PaymentService.AddProcessingFee(User.Identity.GetUserId(),model.Amount),2);

                var createdPayment = _PaymentService.MakePayment( model.Amount);

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
            string payerId = Request.Params["PayerID"];
            BuyPointViewModel paymentInfo = Session["PaymentInfo"] as BuyPointViewModel;
            _PaymentService.ExecutePayment(payerId, paymentInfo.PaymentId);
            UserGameInfo gameInfo = _GameServices.GetGameInfoByUser(User.Identity.GetUserId());

            _GameServices.SaveUserPoint(User.Identity.GetUserId(), gameInfo.Point + paymentInfo.Points);
             _PaymentService.SavePaymentTransaction(User.Identity.GetUserId(), PaymentMethodType.PayPal, payerId, paymentInfo.Amount, PaymentType.Deposit);
            return  RedirectToAction("Player", "Home", new { id = User.Identity.GetUserId() });
        }
    }
}