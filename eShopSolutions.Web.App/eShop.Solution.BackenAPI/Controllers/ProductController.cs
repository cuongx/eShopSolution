using eShop.Solution.ViewModels.Catalog.Product.Manage;
using eShop.Solution.ViewModels.Catalog.Product.ProductImages;
using eShop.Solution.ViewModels.Catalog.Product.Public;
using eShopSolutions.Application.Catalog.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Solution.BackenAPI.Controllers
{
           [Route("api/[controller]")]
           [ApiController]
       public class ProductController:ControllerBase
       {
        readonly IPublicProductService publicProductService;
        private readonly IManagerProductService managerProductService;

        public ProductController(IPublicProductService publicProductService,IManagerProductService managerProductService)
        {
            this.publicProductService = publicProductService;
            this.managerProductService = managerProductService;
        }
       [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await managerProductService.Create(request);
            if (productId == 0)
                return BadRequest();
      
            var product = await managerProductService.GetById(productId, request.LanguageId);

            return CreatedAtAction(nameof(GetbyId), new { id = productId }, product);
        }
        //[NonAction]
   
        [HttpGet("public-paging/{languageId}")]
        public async Task<IActionResult> GetAll(string languageId)
        {
            var product = await publicProductService.GetAll(languageId);
            return Ok(product);
        }
       
        [HttpGet("{LanguageId}")]
        public async Task<IActionResult> GetAllPaging(string LanguageId, [FromQuery] GetPublicProductPagingRequests requests)
        {
            var product = await publicProductService.GetAllByCategoryId(LanguageId,requests);
            return Ok(product);
        }
        [HttpGet("{id}/{languageId}")]
        public async Task<IActionResult> GetbyId(int id , string languageId)
        {
            var product = await managerProductService.GetById(id,languageId);
            if (product == null)
            {
                return BadRequest("Cannot find product");

            }
            return Ok(product);
        }
        [HttpPost("{productId}/images")]
        //[Route("api/product/create")]
        public async Task<IActionResult> CreateImage(int productId, [FromForm] ProductImageCreateRequest request)
        {
            if (!!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                var ImageId = await managerProductService.AddImages(productId,request);
                if (ImageId == 0)
                {
                    return BadRequest();
                }
                var product = await managerProductService.GetImageById(ImageId);
                return CreatedAtAction(nameof(GetbyId), new { id = ImageId }, product);                
        }
        [HttpPut("{productId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImages(int imageId, [FromForm] ProductImageUpdateRequest request)
        {
            if (!!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await managerProductService.UpdateImages(imageId, request);
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpDelete("{productId}/images/{imageId}")]
        public async Task<IActionResult> RemoveImage(int imageId)
        {
            if (!!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await managerProductService.RemoveImages(imageId);
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await managerProductService.Delete(id);
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("price/{id}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int id, decimal newPrice)
        {
            var isSuccessful = await managerProductService.UpdatePrice(id, newPrice);
            if (isSuccessful)
                return Ok();

            return BadRequest();
        }
        [HttpGet("{id}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int id, int imageId)
        {
            var image = await managerProductService.GetImageById(imageId);
            if (image == null)
            {
                return BadRequest("Cannot find product");

            }
            return Ok(image);
        }
    }
}
