using eShop.Solution.ViewModels.Catalog.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.Solution.ViewModels.Catalog.Product.Manage
{
    public class GetManageProductPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        public List<int> CategoryId { get; set; }
    }
}
