using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.Interfaces;
using Microsoft.AspNetCore.Components;

namespace ComputerShop.Client.Shared.Components
{
    public partial class AddToCartSection
    {
        [Parameter] public IProduct? Product { get; set; }
        [Parameter] public int HourToSendTheseDay { get; set; } = 15;
        [Parameter] public int MinPriceForFreeShipping { get; set; } = 100;
        private int quantity = 1;
        private string? time;
        private DateTime today = DateTime.Today;
        public int discountPerc;

        readonly CancellationTokenSource cts = new();
        CancellationToken ct;

        public int Quantity
        {
            get => quantity;
            private set
            {
                if (value > 0 && value < 99)
                {
                    quantity = value;
                }
            }
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            ct = cts.Token;
            Task.Run(() => UpdateTime(ct));
            if(Product != null)
            {
                if (Product.Price < Product.PriceBeforeDiscount)
                {
                    discountPerc = decimal.ToInt32((Product.PriceBeforeDiscount - Product.Price) / Product.PriceBeforeDiscount * 100);
                }
            }
        }

        protected async Task UpdateTime(CancellationToken ct)
        {
            DateTime x = DateTime.Today.AddHours(HourToSendTheseDay);
            while (!ct.IsCancellationRequested)
            {
                time = new DateTime(x.Subtract(DateTime.UtcNow).Ticks).ToString("HH:mm:ss");
                await InvokeAsync(() => StateHasChanged());
                await Task.Delay(1000, ct);
            }
        }

        public async void OnButtonClick()
        {
            if(Product == null)
                return;
            await CartService.AddItemToCartAsync(new CartItem() { Price = Product.Price, ProductId = Product.Id, Quantity=Quantity });
        }

        protected void OnValueChanged(int value)
        {
            Quantity = value;
        }
    }
}
