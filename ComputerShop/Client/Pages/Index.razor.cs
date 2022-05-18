using ComputerShop.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace ComputerShop.Client.Pages
{
    public partial class Index
    {
        [Parameter] public string? Category { get; set; }
        [Parameter] public string? Text { get; set; }
        [Parameter] public int Page { get; set; } = 1;
        int pageCount;

        protected override async Task OnParametersSetAsync()
        {
            await LoadProductsAsync();
            base.OnParametersSet();
        }

        protected async Task ChangePageAsync(int page)
        {
            Page = page;
            await LoadProductsAsync();
        }

        private async Task LoadProductsAsync()
        {
            if (!string.IsNullOrWhiteSpace(Category))
            {
                if (Page < 1)
                    Page = 1;
                var re = await ProductsService.LoadByCategoryAsync(Category, Page);
                pageCount = re.PagesCount;
                Page = re.CurrentPage;
            }
            else if (!string.IsNullOrWhiteSpace(Text))
            {
                if (Page < 1)
                    Page = 1;
                var re = await ProductsService.FindByTextAsync(Text, Page);
                pageCount = re.PagesCount;
                Page = re.CurrentPage;
            }
            else
            {
                await ProductsService.LoadAllAsync();
            }
        }
    }
}
