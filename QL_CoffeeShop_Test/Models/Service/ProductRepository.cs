using Microsoft.EntityFrameworkCore;
using QL_CoffeeShop_Test.Data;
using QL_CoffeeShop_Test.Models.interfaces;

namespace QL_CoffeeShop_Test.Models.Service
{
    public class ProductRepository : IProductRepository
    {
        private CoffeeshopDbContext dbContext;
        public ProductRepository(CoffeeshopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return dbContext.Products;
        }
        public Product? GetProductDetail(int id)
        {
            return dbContext.Products.FirstOrDefault(p => p.Id == id);
        }
        public IEnumerable<Product> GetTrendingProducts()
        {
            return dbContext.Products  
                           .Where(p => p.IsTrendingProduct == true)
                           .ToList();
        }

    }
}
