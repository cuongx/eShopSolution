using eShop.Solution.ViewModels.Catalog.Common;
using eShop.Solution.ViewModels.Catalog.Product;
using eShop.Solution.ViewModels.Catalog.Product.Manage;
using eShop.Solution.ViewModels.Catalog.Product.ProductImages;
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
        Task<ProductViewsModel> GetById(int productId,string languageId);
        Task<bool> UpdatePrice(int productId, decimal newPrice);
        Task  AddViewsCount(int productId);
        Task<bool> UpdateStock(int productId, int addQuantity);
        Task<List<ProductViewsModel>> GetAll();
      Task<PagedResult<ProductViewsModel>> GetAllPaging(GetPublicProductPagingRequests request);
        Task<int> AddImages(int productId,ProductImageCreateRequest request);
        Task<int> RemoveImages(int imageId);
        Task<int> UpdateImages(int imageId,ProductImageUpdateRequest request);
        Task<List<ProductImageViewModel>> GetListImages(int id);
        Task<ProductImageViewModel> GetImageById(int imageId);
       
    }
}
