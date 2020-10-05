using eShopSolutions.Domain.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using eShop.Solution.ViewModels.Catalog.Common;
using eShop.Solution.ViewModels.Catalog.Product.Public;
using eShop.Solution.ViewModels.Catalog.Product;

namespace eShopSolutions.Application.Catalog.Products
{
    public class PublicProductService:IPublicProductService
    {
        private readonly EShopDbContext context;

        public PublicProductService(EShopDbContext context)
        {
            this.context = context;
        }

        public async Task<List<ProductViewsModel>> GetAll()
        {
            var query = from p in context.Products
                        join pt in context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in context.ProductInCategorys on p.Id equals pic.ProductId
                        join c in context.Categories on pic.CategoryId equals c.Id
                        select new { p, pt, pic };
            var data = await query.Select(x => new ProductViewsModel()
               {
                   Id = x.p.Id,
                   Name = x.pt.Name,
                   DateCreated = x.p.DateCreated,
                   Description = x.pt.Description,
                   Details = x.pt.Details,
                   LanguageId = x.pt.LanguageId,
                   OriginalPrice = x.p.OriginalPrice,
                   Price = x.p.Price,
                   SeoAlias = x.pt.SeoAlias,
                   SeoDescription = x.pt.SeoDescription,
                   SeoTitle = x.pt.SeoTitle,
                   Stock = x.p.Stock,
                   ViewCount = x.p.ViewCount
               }).ToListAsync();
            return data;
        }

        public async Task<PagedResult<ProductViewsModel>> GetAllByCategoryId(GetPublicProductPagingRequests request)
        {
            var query = from p in context.Products
                        join pt in context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in context.ProductInCategorys on p.Id equals pic.ProductId
                        join c in context.Categories on pic.CategoryId equals c.Id
                        select new { p, pt, pic };
       

            if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
            {
                query = query.Where(p =>p.pic.CategoryId == request.CategoryId);
            }

            int toatal = await query.CountAsync();
            var data = await query.Skip((request.pageIndex - 1) * request.pageSize).Take(request.pageSize)
                  .Select(x => new ProductViewsModel()
                  {
                      Id = x.p.Id,
                      Name = x.pt.Name,
                      DateCreated = x.p.DateCreated,
                      Description = x.pt.Description,
                      Details = x.pt.Details,
                      LanguageId = x.pt.LanguageId,
                      OriginalPrice = x.p.OriginalPrice,
                      Price = x.p.Price,
                      SeoAlias = x.pt.SeoAlias,
                      SeoDescription = x.pt.SeoDescription,
                      SeoTitle = x.pt.SeoTitle,
                      Stock = x.p.Stock,
                      ViewCount = x.p.ViewCount
                  }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<ProductViewsModel>()
            {
                TotalRecord = toatal,
                Items = data
            };
            return pagedResult;
        }

    
    }
}
