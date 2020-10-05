using eShop.Solution.ViewModels.Catalog.Common;
using eShop.Solution.ViewModels.Catalog.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using eShop.Solution.ViewModels.Catalog.Product.Public;
namespace eShopSolutions.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        public Task<PagedResult<ProductViewsModel>> GetAllByCategoryId(GetProductPagingRequests request );
    }
}
