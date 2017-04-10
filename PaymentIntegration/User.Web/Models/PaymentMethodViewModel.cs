using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.Model;

namespace User.Web.Models
{
    public class PaymentMethodViewModel
    {
       

        public PaymentMethodViewModel(UserPaymentMethod userPaymentMethod)
        {
            this.IsPrimary = userPaymentMethod.IsPrimary;
            this.IsVerified = userPaymentMethod.Isverified;
            this.PaymentMethod = userPaymentMethod.PaymentMethod.Name;
        }

        public bool IsPrimary { get; set; }
        public bool IsVerified { get; set; }
        public string PaymentMethod { get; set; }
    }
}