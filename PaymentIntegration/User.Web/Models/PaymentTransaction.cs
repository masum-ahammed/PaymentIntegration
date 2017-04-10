using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace User.Web.Models
{
    public class PaymentTransaction
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int PaymentMethod { get; set; }
        public int Amount { get; set; }
        public DateTime DateTime { get; set; }
    }
}