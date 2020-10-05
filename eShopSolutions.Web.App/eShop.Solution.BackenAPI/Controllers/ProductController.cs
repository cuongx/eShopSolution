using eShop.Solution.ViewModels.Catalog.Product.Manage;
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
        [HttpGet]
        [Route("/api/product/get")]
        public async Task<IActionResult> Get()
        {
            var product = await publicProductService.GetAll();
            return Ok(product);
        }
        public async Task<IActionResult> Get([FromQuery] GetPublicProductPagingRequests requests)
        {
            var product = await publicProductService.GetAllByCategoryId(requests);
            return Ok(product);
        }
        public async Task<IActionResult> GetbyId(int id)
        {
            var product = await managerProductService.GetById(id);
            if(product == null)
            {
                return BadRequest("Cannot find product");
              
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateRequest request)
        {
            var result = await managerProductService.Create(request);
            if(result == 0)
            {
                return BadRequest();
            }
            return Created(nameof(GetbyId), result);
        }
    }
}
