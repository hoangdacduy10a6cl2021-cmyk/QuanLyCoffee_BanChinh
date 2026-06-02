using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class QL_CoffeeShop_TestContext(DbContextOptions<QL_CoffeeShop_TestContext> options) : IdentityDbContext<QL_CoffeeShop_Test.Data.ApplicationUser>(options)
{
}
