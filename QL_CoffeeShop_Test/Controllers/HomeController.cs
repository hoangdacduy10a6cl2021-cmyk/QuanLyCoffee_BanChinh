using Microsoft.AspNetCore.Mvc;
using QL_CoffeeShop_Test.Models;
using QL_CoffeeShop_Test.Models.interfaces;
using QL_CoffeeShop_Test.Models.Service;
using System.Diagnostics;

namespace QL_CoffeeShop_Test.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository productRepository;

        public HomeController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var trendingProducts = productRepository.GetTrendingProducts(); 
            return View(trendingProducts);
        }
    }
}
