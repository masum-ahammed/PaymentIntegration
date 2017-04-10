using Data.Model;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using User.Web.Models;

namespace User.Web.Controllers
{
    public class HomeController : Controller
    {
        UserService _UserService;
        GameService _GameService;
        public HomeController()
        {
            _UserService = new UserService();
            _GameService = new GameService();
        }
        public ActionResult Index()
        {
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
            PlayerViewModel viewModel = new PlayerViewModel(paymentMethodViewModels, gameInfo);
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
    }
}