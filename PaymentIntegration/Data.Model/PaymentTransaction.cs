using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data.Model
{
    public class PaymentTransaction
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public PaymentMethodType PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public string PaymentId { get; set; }
        public DateTime DateTime { get; set; }
    }
}