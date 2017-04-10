using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace User.Web.Models
{
    public class UserPaymentMethod
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int PaymentMethodId { get; set; }
        public bool IsPrimary { get; set; }
        public bool Isverified { get; set; }
    }
}