using Microsoft.AspNetCore.Components;

namespace ComputerShop.Client.Helpers
{
    public static class NavigationHelper
    {
        public static void GoToProductPage(this NavigationManager navigationManager, string productId, string? categoryName)
        {
            if (string.IsNullOrWhiteSpace(productId))
                return;
            if (string.IsNullOrWhiteSpace(categoryName))
                navigationManager?.NavigateTo($"/product/{productId}", false);
            else
                navigationManager?.NavigateTo($"/product/{categoryName}/{productId}", false);
        }
        public static void GoToFindPage(this NavigationManager navigationManager, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;
            navigationManager.NavigateTo($"find/{text}");
        }
        public static void GoToCartPage(this NavigationManager navigationManager)
        {
            navigationManager.NavigateTo($"cart");
        }
    }
}
