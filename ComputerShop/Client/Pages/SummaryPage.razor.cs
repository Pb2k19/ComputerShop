using ComputerShop.Client.Helpers;
using ComputerShop.Shared.Models.User;
using ComputerShop.Shared.Models.Products;

namespace ComputerShop.Client.Pages
{
    public partial class SummaryPage
    {
        private List<ProductCartItem> productCartItems = new();
        private DeliveryDetails deliveryDetails = new();
        private decimal total = 0;
        private string title = "Dane dostawy";
        private int currentStep = 0;

        private bool showInvoiceForm = false, invoiceDetailsAdded = false;
        private InvoiceDetails invoiceDetails = new();

        protected override async Task OnInitializedAsync()
        {
            productCartItems = await CartService.GetCartProductsAsync();
            SetTotal();
            base.OnInitialized();
            var state = await StateProvider.GetAuthenticationStateAsync();
            deliveryDetails = new();
            invoiceDetails = new();
            if (state?.User?.Identity?.IsAuthenticated ?? false)
            {                
                var reD = await UserDetails.GetDeliveryDetailsAsync();
                if (reD?.Success ?? false)
                    deliveryDetails = reD.Data ?? new();
                else
                    deliveryDetails = new();
                var reI = await UserDetails.GetInvoiceDetailsAsync();
                if (reI?.Success ?? false)
                    invoiceDetails = reI.Data ?? new();
                else
                    invoiceDetails = new();
            }
            else
            {
                deliveryDetails = new();
                invoiceDetails = new();
            }
        }
        protected async Task EndAsync()
        {
            InvoiceDetails? iD;
            if (!invoiceDetailsAdded)
                iD = null;
            else
                iD = invoiceDetails;
            var result = await OrderService.AddOrderAsync(await CartService.GetAllCartItemsAsync(), deliveryDetails, iD);
            if (result == null || !result.Success)
            {
                return;
            }                
            if (result?.Data.State == OrderStates.InPreparation)
                NavigationManager.NavigateTo("/sucess-order");
            else
            {
                var result2 = await PaymentService.CreateCheckoutAsync(productCartItems, result?.Data?.Id ?? string.Empty);
                if (result2 == null || !result2.Success)
                {
                    NavigationManager.NavigateTo($"/sucess-order/{result?.Data.Id}/{result2?.Message}");
                    return;
                }
                else
                {
                    NavigationManager.NavigateTo(result2.Data);
                }
            }
            await CartService.ClearCartAsync();
        }
        protected void NextStep()
        {
            currentStep++;
            ChangeTitle();
        }
        protected void PreviousStep()
        {
            currentStep--;
            ChangeTitle();
        }
        protected void ChangeTitle()
        {
            switch (currentStep)
            {
                case 0:
                    title = "Dane do dostawy";
                    break;
                case 1:
                    title = "Dane do faktury";
                    break;
                default:
                    title = "Podsumowanie";
                    break;
            }
        }
        protected void SaveInvoiceDetails()
        {
            invoiceDetailsAdded = true;
            NextStep();
        }
        protected void GoToProductPage(string productId)
        {
            Product? product = productCartItems.FirstOrDefault(x => productId.Equals(x.Product.Id))?.Product;
            NavigationManager?.GoToProductPage(productId, product?.Category);
        }
        private void SetTotal()
        {
            total = 0;
            productCartItems.ForEach(item => total += item.Product.Price * item.Quantity);
        }
    }
}
