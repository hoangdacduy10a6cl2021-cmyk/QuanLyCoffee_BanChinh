using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QL_CoffeeShop_Test.Models;
using QL_CoffeeShop_Test.Models.interfaces;

namespace QL_CoffeeShop_Test.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private IOrderRepository orderRepository;
        private IShoppingCartRepository shoppingCartRepository;

        public OrdersController(IOrderRepository orderRepository,
                                IShoppingCartRepository shoppingCartRepository)
        {
            this.orderRepository = orderRepository;
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            // checkout logic
            return View();
        }
    }
}