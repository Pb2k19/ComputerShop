﻿using Microsoft.AspNetCore.Components;

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
        public static void GoToLoginPage(this NavigationManager navigationManager, bool returnToPage = true)
        {
            string? url = navigationManager?.ToBaseRelativePath(navigationManager.Uri);
            if (url?.Equals("login") ?? false)
                return;
            if (!returnToPage || string.IsNullOrWhiteSpace(url))
                navigationManager?.NavigateTo($"login");
            else
                navigationManager?.NavigateTo($"login?return-page={url}");
        }
        public static void GoToRegisterPage(this NavigationManager navigationManager)
        {
            navigationManager.NavigateTo($"registration");
        }
    }
}
