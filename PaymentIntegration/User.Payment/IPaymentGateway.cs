using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.PaymentIntegration
{
    public interface IPaymentGateway
    {
        void ReceivePayment();
        void SendPayment();
    }
}
