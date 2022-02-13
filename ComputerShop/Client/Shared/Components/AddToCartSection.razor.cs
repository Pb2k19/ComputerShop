﻿using ComputerShop.Shared.Models.Interfaces;
using Microsoft.AspNetCore.Components;

namespace ComputerShop.Client.Shared.Components
{
    public partial class AddToCartSection
    {
        [Parameter] public IProduct? Product { get; set; }
        [Parameter] public int HourToSendTheseDay { get; set; } = 15;
        private int quantity = 1;
        private string? time;
        private DateTime today = DateTime.Today;

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

        public void OnButtonClick()
        {
            Console.WriteLine($"{Quantity} {Product?.Id}");
        }
    }
}
