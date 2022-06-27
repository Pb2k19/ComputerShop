using ComputerShop.Client.Helpers;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.Interfaces;
using ComputerShop.Shared.Models.Products;
using ComputerShop.Shared.Models.User;
using Microsoft.AspNetCore.Components;

namespace ComputerShop.Client.Pages
{
    public partial class AdminPage
    {
        [Parameter] public string? Page { get; set; }
        [Parameter] public string? ProductId { get; set; }
        [Parameter] public string? Category { get; set; }
        readonly NavigationItems navigationItems = new(new List<NavigationItem>
        {
            new NavigationItem { Name = "Dodaj nowy produkt", Path="add-product"},
            new NavigationItem { Name = "Edytuj produkty", Path="product-list"},
            new NavigationItem { Name = "Lista użytkowników", Path="user-list"},
            new NavigationItem { Name = "Lista zamówień", Path="order-list"},
        });
        private List<Product> products = new();
        private List<UserModel> users = new();
        private List<OrderModel> orders = new();
        bool isFirstLoad = true;
        Product? editedProduct;
        Product? newProduct;

        protected override async Task OnParametersSetAsync()
        {
            await ChangeViewAsync(Page);
        }

        public async Task ChangeViewAsync(string? path)
        {
            if (path != null && path.Equals(Page) && !isFirstLoad)
                return;
            isFirstLoad = false;
            Page = path;
            switch (path)
            {
                case "add-new-product":
                    NavigationManager.NavigateTo($"/admin/{path}");                    
                    break;
                case "product-list":
                    NavigationManager.NavigateTo($"/admin/{path}");
                    products = await ProductsService.GetAllAsync();
                    break;
                case "user-list":
                    NavigationManager.NavigateTo($"/admin/{path}");
                    users = await UserService.GetAllUsersAsync();
                    break;
                case "order-list":
                    NavigationManager.NavigateTo($"/admin/{path}");
                    orders = (await OrderService.GetAllOrdersAsync()).Data.OrderByDescending(o => o.OrderDate).ToList();
                    break;
                case "edit-product":
                    NavigationManager.NavigateTo($"/admin/edit-product/{Category}/{ProductId}");
                    if (!string.IsNullOrEmpty(ProductId) && !string.IsNullOrEmpty(Category))
                    {
                        if(editedProduct == null || !editedProduct.Id.Equals(ProductId))
                        {
                            editedProduct = await ProductHelper.GetProductByCategoryAndIdAsync(ProductsService, Category, ProductId);
                            if (editedProduct == null)
                            {
                                NavigationManager.NavigateTo("/admin");
                            }
                            else
                            {
                                editedProduct.PriceBeforeDiscount = editedProduct.Price;
                            }
                        }
                    }
                    break;
                default:
                    NavigationManager.NavigateTo("/admin"); 
                    break;
            }
        }
        public async Task EditProduct(string id, string category)
        {
            ProductId = id;
            Category = category;
            await ChangeViewAsync("edit-product");
        }
        
        public async Task SaveEditedProduct()
        {
            var re = await ProductsService.EditProductAsync(editedProduct);
            if (re.Success)
                ToastService.ShowSuccess("Zaktualizowano produkt");
            else
                ToastService.ShowError(re.Message, "Coś poszło nie tak");
        }

        public async Task SaveNewProduct()
        {
            var re = await ProductsService.AddProductAsync(newProduct);
            if (re.Success)
                ToastService.ShowSuccess("Dodano produkt");
            else
                ToastService.ShowError(re.Message, "Coś poszło nie tak");
        }

        protected void CategorySelected(int value)
        {
            var category = ProductHelper.GetCategories()[value];
            newProduct = ProductHelper.GetNewProductByCategory(category);
        }
    }
}
