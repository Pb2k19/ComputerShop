using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;
using Microsoft.AspNetCore.Components;

namespace ComputerShop.Client.Pages
{
    public partial class AdminPage
    {
        [Parameter] public string? Page { get; set; }

        readonly DropdownNavigationItems navigationItems = new(new List<DropdownNavigationItem>
        {
            new DropdownNavigationItem { Name = "Dodaj nowy produkt", Path="add-new-product"},
            new DropdownNavigationItem { Name = "Edytuj produkty", Path="edit-products"},
            new DropdownNavigationItem { Name = "Lista użytkowników", Path="user-list"},
            new DropdownNavigationItem { Name = "Lista zamówień", Path="orders-list"},
        });

        private List<UserModel> users = new List<UserModel>();

        public async Task ChangeViewAsync(string? path)
        {
            if (path != null && path.Equals(Page))
                return;
            Page = path;
            switch (path)
            {
                case "add-new-product":
                    NavigationManager.NavigateTo($"/admin/{path}");
                    
                    break;
                case "edit-products":
                    NavigationManager.NavigateTo($"/admin/{path}");
                    
                    break;
                case "user-list":
                    NavigationManager.NavigateTo($"/admin/{path}");
                    users = await UserService.GetAllUsersAsync();
                    break;
                case "orders-list":
                    NavigationManager.NavigateTo($"/admin/{path}");
                    break;
                default:
                    NavigationManager.NavigateTo("/admin");                    
                    break;
            }
        }
    }
}
