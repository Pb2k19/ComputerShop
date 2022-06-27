using ComputerShop.Shared.Models.Products;
using System.Net.Http.Json;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;

namespace ComputerShop.Client.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient httpClient;

        public PaymentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ServiceResponse<string>> CreateCheckoutAsync(List<ProductCartItem> cartItems, string orderId)
        {
            try
            {
                using var response = await httpClient.PostAsJsonAsync($"api/payment/create-checkout/{orderId}", cartItems);
                var data = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(data))
                {
                    return new ServiceResponse<string>() { Success = false, Message = "Nie mogliśmy utworzyć przekierowania do płatności. Twoje zamówienie zostało dodane, w celu jego realizacji skontaktuj się z nami" };
                }
                return new ServiceResponse<string> { Data = data, Success = true };
            }
            catch
            {
                return new ServiceResponse<string>() { Success = false, Message = "Nie mogliśmy utworzyć przekierowania do płatności. Twoje zamówienie zostało dodane, w celu jego realizacji skontaktuj się z nami" };
            }
        }
    }
}
