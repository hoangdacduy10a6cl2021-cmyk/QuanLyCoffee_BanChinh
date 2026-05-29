using QL_CoffeeShop_Test.Data;
using QL_CoffeeShop_Test.Models.interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QL_CoffeeShop_Test.Models.Service
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private CoffeeshopDbContext dbContext;

        public ShoppingCartRepository(CoffeeshopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        
        public List<ShoppingCartItem>? ShoppingCartItems { get; set; }
        public string? ShoppingCartId { get; set; }

       
        public static ShoppingCartRepository GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
            CoffeeshopDbContext context = services.GetService<CoffeeshopDbContext>() ?? throw new Exception("Error initializing coffeeshopdbcontext");
            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();
            session?.SetString("CartId", cartId);
            return new ShoppingCartRepository(context) { ShoppingCartId = cartId };
        }

        public void AddToShoppingCart(Product product)
        {
            var shoppingCartItem = dbContext.ShoppingCartItems.FirstOrDefault(s => s.Product.Id == product.Id && s.ShoppingCardID == ShoppingCartId);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCardID = ShoppingCartId,
                    Product = product,
                    Qty = 1,
                };
                dbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Qty++;
            }
            dbContext.SaveChanges();
        }

     
        public void ClearCart()
        {
            var cartItems = dbContext.ShoppingCartItems.Where(s => s.ShoppingCardID == ShoppingCartId);
            dbContext.ShoppingCartItems.RemoveRange(cartItems);
            dbContext.SaveChanges();
        }

        
        public List<ShoppingCartItem> GetAllShoppingCartItems()
        {
            return ShoppingCartItems ??= dbContext.ShoppingCartItems.Where(s => s.ShoppingCardID == ShoppingCartId).Include(p => p.Product).ToList();
        }

        
        public decimal GetShoppingCartTotal()
        {
            var totalCost = dbContext.ShoppingCartItems.Where(s => s.ShoppingCardID == ShoppingCartId)
                .Select(s => s.Product.Price * s.Qty).Sum();
            return totalCost;
        }

        
        public int RemoveFromCart(Product product)
        {
            var shoppingCartItem = dbContext.ShoppingCartItems.FirstOrDefault(s => s.Product.Id == product.Id && s.ShoppingCardID == ShoppingCartId);
            var quantity = 0;
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Qty > 1)
                {
                    shoppingCartItem.Qty--;
                    quantity = shoppingCartItem.Qty;
                }
                else
                {
                    dbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            dbContext.SaveChanges();
            return quantity;
        }
    }
}