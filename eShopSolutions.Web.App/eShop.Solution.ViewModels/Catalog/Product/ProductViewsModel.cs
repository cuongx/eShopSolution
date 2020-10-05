
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.Solution.ViewModels.Catalog.Product
{
    public class ProductViewsModel
    {

        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; }
        public int Stock { get; set; }
        public int ViewCount { get; set; }
        public DateTime DateCreated { get; set; }
        public string Name { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public string LanguageId { set; get; }
        public string SeoAlias { set; get; }
        public int CategoryId { set; get; }
        public string Details { get; set; }
        public string Description { get; set; }
    }
}
