namespace QL_CoffeeShop_Test.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? detail { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public bool IsTrendingProduct { get; set; }
    }
}
