using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.PaymentIntegration
{
    public class PaymentDataContext:DbContext
    {
        public PaymentDataContext()
            : base("DefaultConnection")
        {
        }
        
    }
}
