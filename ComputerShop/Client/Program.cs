using Blazored.LocalStorage;
using Blazored.Toast;
using ComputerShop.Client;
using ComputerShop.Client.Helpers;
using ComputerShop.Client.Services.Cart;
using ComputerShop.Client.Services.Order;
using ComputerShop.Client.Services.Products;
using ComputerShop.Client.Services.User;
using ComputerShop.Client.Services.UserDetails;
using ComputerShop.Client.Services.WishList;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserDetailsService, UserDetailsService>();
builder.Services.AddScoped<IUserHelper, UserHelper>();
builder.Services.AddScoped<IWishListService, WishListService>();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, ComputerShopAuthenticationStateProvider>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredToast();

await builder.Build().RunAsync();
