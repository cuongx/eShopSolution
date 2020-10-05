using eShop.Solution.ViewModels.Catalog.Common;
using eShop.Solution.ViewModels.Catalog.Product;
using eShop.Solution.ViewModels.Catalog.Product.Manage;
using eShop.Solution.ViewModels.Catalog.Product.Public;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolutions.Application.Catalog.Products
{
    public interface IManagerProductService
    {
       Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductUpdateRequest request);
        Task<int> Delete(int id);
        Task<ProductViewsModel> GetById(int productId);
        Task<bool> UpdatePrice(int productId, decimal newPrice);
        Task  AddViewsCount(int productId);
        Task<bool> UpdateStock(int productId, int addQuantity);
        Task<List<ProductViewsModel>> GetAll();
      Task<PagedResult<ProductViewsModel>> GetAllPaging(GetPublicProductPagingRequests request);
        Task<int> AddImages(int productId, List<IFormFile> file);
        Task<int> RemoveImages(int imageId);
        Task<int> UpdateImages(int imageId,string caption, bool isDelful);
        Task<List<ProductImageViewModel>> GetListImage(int id);
    }
}
