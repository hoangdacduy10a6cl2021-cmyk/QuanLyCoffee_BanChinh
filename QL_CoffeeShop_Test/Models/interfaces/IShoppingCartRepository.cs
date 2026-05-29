using QL_CoffeeShop_Test.Models;
using System.Collections.Generic;

namespace QL_CoffeeShop_Test.Models.interfaces
{
    public interface IShoppingCartRepository
    {
        void AddToShoppingCart(Product product);

        int RemoveFromCart(Product product);

        List<ShoppingCartItem> GetAllShoppingCartItems();

        void ClearCart();

        decimal GetShoppingCartTotal();

        public List<ShoppingCartItem>? ShoppingCartItems { get; set; }

        public string? ShoppingCartId { get; set; }
    }
}