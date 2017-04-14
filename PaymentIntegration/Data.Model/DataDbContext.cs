using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class DataDbContext : DbContext
    {
        public DataDbContext()
            : base("name=DefaultConnection")
        {
            Database.SetInitializer<DataDbContext>(null);
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<UserPaymentMethod> UserPaymentMethods { get; set; }

        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        public DbSet<UserGameInfo> UserGameInfos { get; set; }
      

    }
}
