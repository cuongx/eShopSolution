using eShopSolutions.Domain.Entites.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolutions.Domain.Entites.Orders
{
   public class OrderDetail
    {

        public int OrderId { set; get; }
        public Order Order { get; set; }
        public int ProductId { set; get; }
        public Product Product { get; set; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }



       
    }
}
