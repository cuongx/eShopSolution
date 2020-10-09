using eShop.Solution.ViewModels.Catalog.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.Solution.ViewModels.Catalog.Product.Public
{
    public class GetPublicProductPagingRequests : PagingRequestBase
    {
        public int? CategoryId { get; set; }
        public string LanguageId { get; set; }

    }
}
