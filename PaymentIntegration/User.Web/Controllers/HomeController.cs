using Data.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using User.PaymentIntegration;
using User.Web.Models;

namespace User.Web.Controllers
{
    public class HomeController : Controller
    {
        UserService _UserService;
        GameService _GameService;
        PaymentService _PaymentService;
        private ApplicationUserManager _userManager;
        public HomeController()
        {
            _UserService = new UserService();
            _GameService = new GameService();
            _PaymentService = new PaymentService();
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (UserManager.IsInRole(User.Identity.GetUserId(),"Admin"))
                    return RedirectToAction("Admin", "Home", new { id = User.Identity.GetUserId() });
                if (UserManager.IsInRole(User.Identity.GetUserId(),"Player"))
                    return RedirectToAction("Player", "Home", new { id = User.Identity.GetUserId() });
            }

            return View();
        }

        public ActionResult Admin()
        {
            return View();
        }
        public ActionResult Player(string id)
        {
            string userId = id;
            List<UserPaymentMethod> userPaymentMethods = _UserService.GetPaymentMethodsByUser(userId);
            UserGameInfo gameInfo = _GameService.GetGameInfoByUser(userId);
            List<PaymentMethodViewModel> paymentMethodViewModels = userPaymentMethods.ConvertAll(x => new PaymentMethodViewModel(x));
            decimal totalSpent = _PaymentService.GetTotalSpentByUser(userId);
            PlayerViewModel viewModel = new PlayerViewModel(paymentMethodViewModels, gameInfo, totalSpent);
            return View(viewModel);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult EnrollIntoGame(string id)
        {
            _GameService.EntollIntoGame(id);

            return RedirectToAction("Player", "Home", new { id = id });
        }
    }
}