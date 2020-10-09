//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using eShop.Solution.BackenAPI.Models;
//using eShopSolutions.Application.Catalog.Products;

//namespace eShop.Solution.BackenAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class HomeController : Controller
//    {
//        private readonly IPublicProductService publicProductService;
//        public HomeController(IPublicProductService publicProductService)
//        {
//            this.publicProductService = publicProductService;
//        }

//        [HttpGet]
//        [Route("/api/Get")]
//        public async Task<IActionResult> Get()
//        {
//            var product = await publicProductService.GetAll();
//            return Ok(product);
//        }
//        public IActionResult Index()
//        {
//            return View();
//        }

//        public IActionResult Privacy()
//        {
//            return View();
//        }

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}
