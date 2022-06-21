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

        protected async override Task OnInitializedAsync()
        {
            users = await UserService.GetAllUsersAsync();
        }

        public async Task ChangeViewAsync(string? name)
        {
            Page = name;
            switch (name)
            {
                case "wish-list":
                    NavigationManager.NavigateTo("/account/wish-list");
                    //await LoadWishListAsync();
                    break;
                case "delivery-details":
                    NavigationManager.NavigateTo("/account/delivery-details");
                    //await LoadDeliveryDetailsAsync();
                    break;
                case "invoice-details":
                    NavigationManager.NavigateTo("/account/invoice-details");
                    //await LoadInvoiceDetailsAsync();
                    break;
                case "order-details":
                    //NavigationManager.NavigateTo($"/account/order-details/{OrderId}");
                    //await LoadCurrentOderAsync();
                    break;
                case "security":
                    NavigationManager.NavigateTo($"/account/security");
                    break;
                default:
                    NavigationManager.NavigateTo("/account");
                    //await LoadOrdersAsync();
                    break;
            }
        }

    }
}
