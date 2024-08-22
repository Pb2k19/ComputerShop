using ComputerShop.Server.Cryptography.Hash;
using ComputerShop.Server.DataAccess;
using ComputerShop.Server.Services.Order;
using ComputerShop.Server.Services.Payment;
using ComputerShop.Server.Services.Products;
using ComputerShop.Server.Services.User;
using ComputerShop.Server.Services.UserDetails;
using ComputerShop.Server.Services.WishList;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddMemoryCache();

builder.Services.AddSingleton<IHashAlgorithm, PBKDF2HashAlgorithm>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserDetailsService, UserDetailsService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IWishListService, WishListService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddSingleton<IDbConnection, DbConnection>();
builder.Services.AddSingleton<IProductData, ProductData>();
builder.Services.AddSingleton<IUserData, UserData>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var eccPem = builder.Configuration["Settings:TokenPrivateEC"];
using var key = ECDsa.Create();
key.ImportECPrivateKey(Convert.FromBase64String(eccPem), out _);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(bearerOptions =>
{
    bearerOptions.TokenValidationParameters = new()
    {
        IssuerSigningKey = new ECDsaSecurityKey(key),
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
    };
});
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
