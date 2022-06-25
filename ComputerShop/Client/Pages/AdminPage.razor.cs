using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.Products;
using ComputerShop.Shared.Models.User;
using Microsoft.AspNetCore.Components;

namespace ComputerShop.Client.Pages
{
    public partial class AdminPage
    {
        [Parameter] public string? Page { get; set; }
        readonly NavigationItems navigationItems = new(new List<NavigationItem>
        {
            new NavigationItem { Name = "Dodaj nowy produkt", Path="add-new-product"},
            new NavigationItem { Name = "Edytuj produkty", Path="edit-products"},
            new NavigationItem { Name = "Lista użytkowników", Path="user-list"},
            new NavigationItem { Name = "Lista zamówień", Path="order-list"},
        });
        private List<Product> products = new();
        private List<UserModel> users = new();
        private List<OrderModel> orders = new();
        bool isFirstLoad = true;

        public async Task ChangeViewAsync(string? path)
        {
            if (path != null && path.Equals(Page) && !isFirstLoad)
                return;
            isFirstLoad = false;
            Page = path;
            switch (path)
            {
                case "add-new-product":
                    NavigationManager.NavigateTo($"/admin/{path}");                    
                    break;
                case "edit-products":
                    NavigationManager.NavigateTo($"/admin/{path}");
                    products = await ProductsService.GetAllAsync();
                    break;
                case "user-list":
                    NavigationManager.NavigateTo($"/admin/{path}");
                    users = await UserService.GetAllUsersAsync();
                    break;
                case "order-list":
                    NavigationManager.NavigateTo($"/admin/{path}");
                    orders = (await OrderService.GetAllOrdersAsync()).Data.OrderByDescending(o => o.OrderDate).ToList();
                    break;
                default:
                    NavigationManager.NavigateTo("/admin");                    
                    break;
            }
        }
    }
}
