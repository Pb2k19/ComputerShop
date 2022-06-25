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
            await OrderService.AddOrderAsync(await CartService.GetAllCartItemsAsync(), deliveryDetails, iD);
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
