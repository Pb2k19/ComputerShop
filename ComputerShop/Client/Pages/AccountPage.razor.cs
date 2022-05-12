using ComputerShop.Client.Helpers;
using ComputerShop.Shared.Models.User;
using Microsoft.AspNetCore.Components;

namespace ComputerShop.Client.Pages
{
    public partial class AccountPage
    {
        [Parameter] public string? Page { get; set; }
        ChangePassword changePassword = new();
        IUserHelper? userHelper = null;
        DeliveryDetails delivery = new();
        bool isInvoiceForBusiness = true;
        InvoiceDetailsForBusiness invoiceDetailsForBusiness = new();
        InvoiceDetails invoiceDetails = new();
        WishListModel wishList = new();

        protected async override Task OnParametersSetAsync()
        {
            wishList = (await WishListService.GetWishListAsync()).Data;
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

        protected void OnValidSubmit()
        {
            if (isInvoiceForBusiness)
                return;
            return;
        }
    }
}
