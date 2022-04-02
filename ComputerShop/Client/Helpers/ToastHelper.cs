using Blazored.Toast.Services;

namespace ComputerShop.Client.Helpers
{
    public static class ToastHelper
    {
        public static void ShowAddToCart(this IToastService toastService, string productName = "")
        {
            toastService.ShowToast(ToastLevel.Info, productName, "Dodano produkt do koszyka");
        }
        public static void ShowCartItemRemoved(this IToastService toastService, string productName = "")
        {
            toastService.ShowToast(ToastLevel.Info, productName, "Usunięto produkt z koszyka");
        }
    }
}
