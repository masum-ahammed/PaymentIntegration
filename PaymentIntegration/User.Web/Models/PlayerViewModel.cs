using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.Model;

namespace User.Web.Models
{
    public class PlayerViewModel
    {
       

        public PlayerViewModel(List<PaymentMethodViewModel> paymentMethods, UserGameInfo gameInfo)
        {
            PaymentMethods = paymentMethods;
            Points = gameInfo.Point;
            IsEnrolledInGame = gameInfo.IsEnrolledInGame;
        }

        public List<PaymentMethodViewModel> PaymentMethods { get; set; }
        public int Points { get; set; }
        bool IsEnrolledInGame { get; set; }
    }
}