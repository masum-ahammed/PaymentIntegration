using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.Model;

namespace User.Web.Models
{
    public class PlayerViewModel
    {
       

        public PlayerViewModel(List<PaymentMethodViewModel> paymentMethods, UserGameInfo gameInfo, decimal totalSpent)
        {
            PaymentMethods = paymentMethods;
            Points = gameInfo.Point;
            IsEnrolledInGame = gameInfo.IsEnrolledInGame;
            UserId = gameInfo.UserId;
            TotalSpent = totalSpent.ToString("0.00");
        }

        public List<PaymentMethodViewModel> PaymentMethods { get; set; }
        public int Points { get; set; }
        public bool IsEnrolledInGame { get; set; }
        public string UserId { get; set; }
        public string TotalSpent { get; set; }
    }
}