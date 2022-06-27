using ComputerShop.Client.Helpers;
using ComputerShop.Shared.Models.Interfaces;
using ComputerShop.Shared.Models.Products;
using Microsoft.AspNetCore.Components;

namespace ComputerShop.Client.Pages
{
    public partial class ProductDetailsPage
    {
        [Parameter] public string? Categoryname { get; set; }
        [Parameter] public string? Id { get; set; }

        Comment comment = new();

        private IProduct? currentProdcut;
        protected override async Task OnInitializedAsync()
        {
            if(!string.IsNullOrWhiteSpace(Id))
            {                
                if (!string.IsNullOrWhiteSpace(Categoryname))
                {
                    currentProdcut = await ProductHelper.GetProductByCategoryAndIdAsync(ProductsService, Categoryname, Id);
                }
                else
                {
                    currentProdcut = await ProductsService.GetProductByIdAsync(Id);
                }
            }
            base.OnInitialized();
        }

        protected async Task AddCommentAsync()
        {
            if (comment != null && currentProdcut != null)
            {
                await ProductsService.AddCommentAsync(comment, currentProdcut.Id);
                currentProdcut.Comments.Add(comment);
            }                
            comment = new();
        }

        protected async Task OnListItemClick(int num)
        {
            switch (num)
            {
                case 0:
                    await JS.ScrollToElementAsync("product-name");
                    break;
                case 1:
                    await JS.ScrollToElementAsync("product-description");
                    break;
                case 2:
                    await JS.ScrollToElementAsync("product-details");
                    break;
                case 3:
                    await JS.ScrollToElementAsync("product-comments");
                    break;
                default:
                    break;
            }
        }
    }
}
