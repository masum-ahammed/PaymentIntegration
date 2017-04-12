namespace User.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Data.Model;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.Model.IdentityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "User.Web.Models.ApplicationDbContext";
        }

        ApplicationUser AddAdminUserAndRole(Data.Model.IdentityDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("Admin"));
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var adminUser = new ApplicationUser()
            {
                UserName = "admin@xyz.com",
            };
            ir = um.Create(adminUser, "Admin@123");
           
            ir = um.AddToRole(adminUser.Id, "Admin");
            return adminUser;
        }
        ApplicationUser AddPlayerUserAndRole(Data.Model.IdentityDbContext context, string player)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("Player"));
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var playerUser = new ApplicationUser()
            {
                UserName = string.Format("{0}.game@gmail.com", player),
                Email = string.Format("{0}.game@gmail.com",player)
            };
            ir = um.Create(playerUser, "Player@123");
           
            ir = um.AddToRole(playerUser.Id, "Player");
            return playerUser;
        }
        protected override void Seed(Data.Model.IdentityDbContext context)
        {
            var paypalPaymentMethod = new PaymentMethod() { Name = "PayPal" };
            var localBankPaymentMethod = new PaymentMethod() { Name = "Local Bank" };
            var usBankPaymentMethod = new PaymentMethod() { Name = "US Bank" };
            context.PaymentMethods.AddOrUpdate(paypalPaymentMethod);
            context.PaymentMethods.AddOrUpdate(localBankPaymentMethod);
            context.PaymentMethods.AddOrUpdate(usBankPaymentMethod);
            var adminUser = AddAdminUserAndRole(context);
           var player1User = AddPlayerUserAndRole(context, "player1");
           var player2User = AddPlayerUserAndRole(context, "player2");
            context.UserPaymentMethods.AddOrUpdate(new UserPaymentMethod() { PaymentMethodId = paypalPaymentMethod.Id, UserId = player1User.Id, IsPrimary = true, Isverified = true });
            context.UserPaymentMethods.AddOrUpdate(new UserPaymentMethod() { PaymentMethodId = localBankPaymentMethod.Id, UserId = player1User.Id, IsPrimary = false });
            context.UserPaymentMethods.AddOrUpdate(new UserPaymentMethod() { PaymentMethodId = paypalPaymentMethod.Id, UserId = player2User.Id, IsPrimary = true });
            context.UserPaymentMethods.AddOrUpdate(new UserPaymentMethod() { PaymentMethodId = localBankPaymentMethod.Id, UserId = player2User.Id, IsPrimary = false });

        }
    }
}
