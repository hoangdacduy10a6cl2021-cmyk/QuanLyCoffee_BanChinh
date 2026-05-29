using QL_CoffeeShop_Test.Models;
using QL_CoffeeShop_Test.Models.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace QL_CoffeeShop_Test.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IShoppingCartRepository shoppingCartRepository;

        public ShoppingCartController(IProductRepository productRepository, IShoppingCartRepository shoppingCartRepository)
        {
            this.productRepository = productRepository;
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public IActionResult Index()
        {
            var items = shoppingCartRepository.GetAllShoppingCartItems();
            shoppingCartRepository.ShoppingCartItems = items;

           
            HttpContext.Session.SetInt32("CartCount", items.Count);

            ViewBag.TotalCart = shoppingCartRepository.GetShoppingCartTotal();
            return View(items);
        }

        public RedirectToActionResult AddToShoppingCart(int pId)
        {
            var product = productRepository.GetAllProducts().FirstOrDefault(p => p.Id == pId);
            if (product != null)
            {
                shoppingCartRepository.AddToShoppingCart(product);

                
                int cartCount = shoppingCartRepository.GetAllShoppingCartItems().Count();
                HttpContext.Session.SetInt32("CartCount", cartCount);
            }
            return RedirectToAction("Index");
        }

        
        public RedirectToActionResult RemoveFromShoppingCart(int pId)
        {
            var product = productRepository.GetAllProducts().FirstOrDefault(p => p.Id == pId);
            if (product != null)
            {
                shoppingCartRepository.RemoveFromCart(product);

                
                int cartCount = shoppingCartRepository.GetAllShoppingCartItems().Count();
                HttpContext.Session.SetInt32("CartCount", cartCount);
            }
            return RedirectToAction("Index");
        }
    }
}