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
        InvoiceDetails invoiceDetails = new();
        WishListModel wishList = new();
        List<OrderModel> orderList = new();

        protected async override Task OnParametersSetAsync()
        {
            if (userHelper == null)
                userHelper = new UserHelper(StateProvider, UserService, LocalStorageService, NavigationManager, ToastService);
            await ChangeViewAsync(Page);            
            base.OnParametersSet();
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
                    ToastService.ShowInfo(response?.Message, "Nie udało się");
            }
        }
        public async Task ChangeViewAsync(string? name)
        {
            Page = name;
            switch(name)
            {
                case "wish-list":
                    wishList = new();
                    var reW = await WishListService.GetWishListAsync();
                    if (reW.Success)
                    {
                        wishList = reW.Data ?? new();
                    }
                    break;
                case "delivery-details":
                    delivery = new();
                    var reD = await UserDetails.GetDeliveryDetailsAsync();
                    if (reD?.Success ?? false)
                    {
                        delivery = reD.Data ?? new();
                    }
                    break;
                case "invoice-details":
                    invoiceDetails = new();
                    var reI = await UserDetails.GetInvoiceDetailsAsync();
                    if (reI?.Success ?? false)
                    {
                        invoiceDetails = reI.Data ?? new();
                    }
                    break;
                default:
                    orderList = new();
                    var reO = await OrderService.GetAllOrdersForUserAsync();
                    if(reO?.Success ?? false)
                    {
                        orderList = reO.Data ?? new();
                    }
                    break;
            }
        }
        protected async void OnValidSubmitInvoiceAsync()
        {
            var re = await UserDetails.UpdateInvoiceDetailsAsync(invoiceDetails);
            if (re?.Success ?? false)
                ToastService.ShowSuccess("Zaktualizowano dane do faktury", "Sukces");
            else
                ToastService.ShowError(re?.Message ?? "Coś poszło nie tak", "Nie udało się");
        }
        protected async void OnValidSubmitDeliveryAsync()
        {
            var re = await UserDetails.UpdateDeliveryDetailsAsync(delivery);
            if (re?.Success ?? false)
                ToastService.ShowSuccess("Zaktualizowano dane do dostawy", "Sukces");
            else
                ToastService.ShowError(re?.Message ?? "Coś poszło nie tak", "Nie udało się");
        }
    }
}
