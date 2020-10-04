using eShopSolutions.Domain.Entites.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolutions.Domain.Entites.Categorys
{
  public class Category
    {
        public int Id { get; set; }
        public int SortOrder { get; set; }
        public bool IsShowOnHome { get; set; }
        public int? ParentId { get; set; }
        public Status Status { get; set; }
        public ICollection<ProductInCategory> ProductInCategorys { get; set; }
        public List<CategoryTranslation> CategoryTranslations { get; set; }
    }
}
