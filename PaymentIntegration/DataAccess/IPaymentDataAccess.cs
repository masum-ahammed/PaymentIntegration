
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IPaymentDataAccess
    {
        decimal GetTotalSpentByUser(string userId);
        int SavePaymentTransaction(PaymentTransaction paymentTransaction);
    }
}
