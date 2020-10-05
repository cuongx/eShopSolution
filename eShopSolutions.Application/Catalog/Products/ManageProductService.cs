using eShop.Solution.ViewModels.Catalog.Common;
using eShop.Solution.ViewModels.Catalog.Product;
using eShop.Solution.ViewModels.Catalog.Product.Manage;
using eShop.Solution.ViewModels.Catalog.Product.Public;
using eShopSolutions.Application.Catalog.Common;
using eShopSolutions.Domain.EF;
using eShopSolutions.Domain.Entites;
using eShopSolutions.Domain.Entites.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolutions.Application.Catalog.Products
{
    public class ManageProductService : IManagerProductService
    {
        private readonly EShopDbContext context;
        private readonly IStorageService storageService;

        public ManageProductService(EShopDbContext context,IStorageService storageService)
        {
            this.context = context;
            this.storageService = storageService;
        }

        public Task<int> AddImages(int productId, List<IFormFile> file)
        {
            throw new NotImplementedException();
        }

        public async Task AddViewsCount(int productId)
        {
            var product = await context.Products.FindAsync(productId);
            product.ViewCount += 1;
            await context.SaveChangesAsync();
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product
            {

                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name =  request.Name,
                        Description = request.Description,
                        Details = request.Details,
                        SeoDescription = request.SeoDescription,
                        SeoAlias = request.SeoAlias,
                        SeoTitle = request.SeoTitle,
                        LanguageId = request.LanguageId
                    }
                }
            };
            if(request.Thumbnaill != null)
            {
                product.ProductImages = new List<ProductImage>();
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnail Image",
                        DateCreated = DateTime.Now,
                        FileSize = request.Thumbnaill.Length,
                        ImagePath = await this.SaveFile(request.Thumbnaill),
                        IsDefault = true,
                        SortOrder = 1
                    };
                }
            }
            context.Products.Add(product);
            return await context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null) throw new Exception($"Can not find product: {id}");
            context.Products.Remove(product);
            var images = context.ProductImages.Where(i => i.ProductId == id);
            foreach(var image in images)
            {
                await storageService.DeleteFileAsync(image.ImagePath);
            }
            context.Remove(product);
            return await context.SaveChangesAsync();
        }

        public Task<List<ProductViewsModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<ProductViewsModel>> GetAllPaging(GetProductPagingRequest request)
        {
            var query = from p in context.Products
                        join pt in context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in context.ProductInCategorys on p.Id equals pic.ProductId
                        join c in context.Categories on pic.CategoryId equals c.Id
                        where pt.Name.Contains(request.Keyword)
                        select new { p, pt, pic };
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(a => a.pt.Name.Contains(request.Keyword));
            }

            if (request.CategoryId.Count > 0)
            {
                query = query.Where(p => request.CategoryId.Contains(p.pic.CategoryId));
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

        public Task<PagedResult<ProductViewsModel>> GetAllPaging(GetProductPagingRequests request)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductImageViewModel>> GetListImage(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveImages(int imageId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Update(ProductEditRequest request)
        {
            var product = await context.Products.FindAsync(request.Id);
            var productTranslation = await context.ProductTranslations.FirstOrDefaultAsync(a => a.Id == request.Id && a.LanguageId == request.LanguageId);
            if (product == null || productTranslation == null) throw new Exception($"Can not find product: {request.Id}");
            productTranslation.Name = request.Name;
            productTranslation.SeoAlias = request.SeoAlias;
            productTranslation.SeoDescription = request.SeoDescription;
            productTranslation.SeoTitle = request.SeoTitle;
            productTranslation.Details = request.Details;
            if (request.Thumbnaill != null)
            {
                var thumbanaill = context.ProductImages.FirstOrDefault(i => i.IsDefault == true && i.ProductId == request.Id);
                if (thumbanaill != null)
                {
                    thumbanaill.FileSize = request.Thumbnaill.Length;
                    thumbanaill.ImagePath = await this.SaveFile(request.Thumbnaill);
                    context.ProductImages.Update(thumbanaill);
                }
                                    
            }
            return await context.SaveChangesAsync();

        }

        public Task<int> UpdateImages(int imageId, string caption, bool isDelful)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new Exception($"Can not find product: {product}");
            }
            product.Price = newPrice;
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int productId, int addQuantity)
        {
            var product = await context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new Exception($"Can not find product: {productId}");
            }
            product.Stock += addQuantity;
            return await context.SaveChangesAsync() > 0;

        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
