using coffeeshop.Models.Services;
using Microsoft.EntityFrameworkCore;
using QL_CoffeeShop_Test.Data;
using QL_CoffeeShop_Test.Models.interfaces;
using QL_CoffeeShop_Test.Models.Service;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// ===== SERVICES =====
builder.Services.AddControllersWithViews();

//add code
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>(
    ShoppingCartRepository.GetCart);
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddDbContext<CoffeeshopDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("CoffeeShopDbContextConnection")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<CoffeeshopDbContext>();

//session
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ===== BUILD =====
var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production
    // scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();