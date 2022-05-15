using ComputerShop.Client.Helpers;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;
using Microsoft.AspNetCore.Components;

namespace ComputerShop.Client.Pages
{
    public partial class AccountPage
    {
        [Parameter] public string? Page { get; set; }
        readonly DropdownNavigationItems navigationItems = new(new List<DropdownNavigationItem>
        {
            new DropdownNavigationItem { Name = "Zamówienia", Path="orders"},
            new DropdownNavigationItem { Name = "Lista życzeń", Path="wish-list"},
            new DropdownNavigationItem { Name = "Dane do dostawy", Path="delivery-details"},
            new DropdownNavigationItem { Name = "Dane do faktury", Path="invoice-details"},
            new DropdownNavigationItem { Name = "Bezpieczeństwo", Path="security"},
        });
        ChangePassword changePassword = new();
        IUserHelper? userHelper = null;
        DeliveryDetails delivery = new();       
        InvoiceDetailsForBusiness invoiceDetailsForBusiness = new();
        InvoiceDetails invoiceDetails = new();
        WishListModel wishList = new();
        List<OrderModel> orderList = new();
        bool isInvoiceForBusiness = true;


        protected async override Task OnParametersSetAsync()
        {
            await ChangeViewAsync(Page);
            base.OnParametersSet();
        }
        protected void InvoiceChanged(bool isForBussiness)
        {
            isInvoiceForBusiness = isForBussiness;
        }
        protected async Task OnPasswordChangeAsync()
        {
            if (userHelper == null)
                userHelper = new UserHelper(StateProvider, UserService, LocalStorageService, NavigationManager, ToastService);
            var response = await UserService.ChangePassword(changePassword);
            if (response.Success)
                ToastService.ShowSuccess("Hasło zostało zmienione", "Udało się!");
            else
            {
                if (response.Data == System.Net.HttpStatusCode.Unauthorized)
                    await userHelper.LogoutOnUnauthorizedAsync();
                else
                    ToastService.ShowInfo(response?.Message, "Nie udało się ☹");
            }
        }
        public async Task ChangeViewAsync(string? name)
        {
            Page = name;
            switch(name)
            {
                case "wish-list":
                    wishList = (await WishListService.GetWishListAsync()).Data;
                    break;
                case "delivery-details":
                    break;
                case "invoice-details":
                    break;
                default:
                    orderList = (await OrderService.GetAllOrdersForUserAsync()).Data;
                    break;
            }
        }

        protected void OnValidSubmit()
        {
            if (isInvoiceForBusiness)
                return;
            return;
        }
    }
}
