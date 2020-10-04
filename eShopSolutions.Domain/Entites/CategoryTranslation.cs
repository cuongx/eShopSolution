using eShopSolutions.Domain.Entites.Categorys;
using eShopSolutions.Domain.Entites.Languages;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolutions.Domain.Entites
{
   public class CategoryTranslation
    {
        public int Id { set; get; }

        public string Name { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public string LanguageId { set; get; }
        public string SeoAlias { set; get; }
        public int CategoryId { set; get; }
        public Category Category { get; set; }

        public Language Language { get; set; }
    }
}
