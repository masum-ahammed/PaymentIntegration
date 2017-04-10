using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace User.Web.Models
{
    public class BuyPointViewModel
    {
        public int Points { get; set; }
        public decimal Amount { get; set; }

        public string PaymentId { get; set; }
    }
}