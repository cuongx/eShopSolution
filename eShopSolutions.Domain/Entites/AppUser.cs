using eShopSolutions.Domain.Entites.Carts;
using eShopSolutions.Domain.Entites.Orders;
using eShopSolutions.Domain.Entites.Transactionss;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
namespace eShopSolutions.Domain.Entites
{
   public class AppUser:IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        public List<Cart> Carts { get; set; }

        public List<Order> Orders { get; set; }

        public List<Transactions> Transactions { get; set; }
    }
}
