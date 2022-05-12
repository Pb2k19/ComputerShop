using ComputerShop.Shared.Models;
using ComputerShop.Client.Helpers;
using ComputerShop.Shared.Models.User;

namespace ComputerShop.Client.Pages
{
    public partial class SummaryPage
    {
        private List<ProductCartItem> productCartItems = new();
        private DeliveryDetails deliveryDetails = new();
        private decimal total = 0;
        private string title = "Dane dostawy";
        private int currentStep = 0;

        private bool isInvoiceForBusiness = true, showInvoiceForm = false, invoiceDetailsAdded = false;
        private InvoiceDetailsForBusiness invoiceDetailsForBusiness = new();
        private InvoiceDetails invoiceDetails = new();

        protected override async Task OnInitializedAsync()
        {
            productCartItems = await CartService.GetCartProductsAsync();
            SetTotal();
            base.OnInitialized();
        }
        protected async Task EndAsync()
        {
            InvoiceDetails? iD;
            if (!invoiceDetailsAdded)
                iD = null;
            else if (isInvoiceForBusiness)
                iD = invoiceDetailsForBusiness;
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
        protected void InvoiceChanged()
        {
            isInvoiceForBusiness = !isInvoiceForBusiness;
        }
        protected void SaveInvoiceDetails()
        {
            invoiceDetailsAdded = true;
            NextStep();
        }
        protected void GoToProductPage(string productId)
        {
            Product? product = productCartItems.FirstOrDefault(x => productId.Equals(x.Product.Id))?.Product;
            NavigationManager?.GoToProductPage(productId, product?.Category?.Name);
        }
        private void SetTotal()
        {
            total = 0;
            productCartItems.ForEach(item => total += item.Product.Price * item.Quantity);
        }
    }
}
