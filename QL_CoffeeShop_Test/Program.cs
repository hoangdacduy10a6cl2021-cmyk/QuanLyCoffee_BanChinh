using coffeeshop.Models.Services;
using Microsoft.EntityFrameworkCore;
using QL_CoffeeShop_Test.Data;
using QL_CoffeeShop_Test.Models.interfaces;
using QL_CoffeeShop_Test.Models.Service;

var builder = WebApplication.CreateBuilder(args);

// ===== SERVICES =====
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CoffeeshopDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("CoffeeShopDbContextConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>(ShoppingCartRepository.GetCart);
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ===== BUILD =====
var app = builder.Build();

// ===== MIDDLEWARE =====
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();