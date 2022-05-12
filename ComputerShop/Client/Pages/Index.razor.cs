using ComputerShop.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace ComputerShop.Client.Pages
{
    public partial class Index
    {
        [Parameter] public string? Category { get; set; }
        [Parameter] public string? Text { get; set; }
        [Parameter] public int Page { get; set; } = 1;
        private Category? currentCategory;
        int pageCount;

        protected override async Task OnParametersSetAsync()
        {
            if (string.IsNullOrWhiteSpace(Category))
            {
                currentCategory = null;
            }
            else
            {
                currentCategory = await CategoryService.GetCategoryByUrl(Category);
            }
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
            if (currentCategory != null)
            {
                if (Page < 1)
                    Page = 1;
                var re = await ProductsService.LoadByCategoryIdAsync(currentCategory.Id, Page);
                pageCount = re.PagesCount;
                Page = re.CurrentPage;
            }
            else if (!string.IsNullOrWhiteSpace(Text))
            {
                if (Page < 1)
                    Page = 1;
                var re = await ProductsService.FindByTextAsync(Text, Page);

            }
            else
            {
                await ProductsService.LoadAllAsync();
            }
        }
    }
}
