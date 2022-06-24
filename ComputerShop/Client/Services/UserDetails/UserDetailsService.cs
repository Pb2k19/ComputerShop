using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;
using System.Net.Http.Json;

namespace ComputerShop.Client.Services.UserDetails
{
    public class UserDetailsService : IUserDetailsService
    {
        private readonly HttpClient httpClient;

        public UserDetailsService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ServiceResponse<DeliveryDetails>?> GetDeliveryDetailsAsync()
        {
            var respone = await httpClient.GetFromJsonAsync<ServiceResponse<DeliveryDetails>>("api/user/getDeliveryDetails");
            return respone;
        }
        public async Task<ServiceResponse<InvoiceDetails>?> GetInvoiceDetailsAsync()
        {
            var respone = await httpClient.GetFromJsonAsync<ServiceResponse<InvoiceDetails>>("api/user/getInvoiceDetails");
            return respone;
        }
        public async Task<SimpleServiceResponse?> UpdateDeliveryDetailsAsync(DeliveryDetails deliveryDetails)
        {
            using var response = await httpClient.PostAsJsonAsync("api/user/updateDeliveryDetails", deliveryDetails);
            return await response.Content.ReadFromJsonAsync<SimpleServiceResponse>();
        }
        public async Task<SimpleServiceResponse?> UpdateInvoiceDetailsAsync(InvoiceDetails invoiceDetails)
        {
            if (!invoiceDetails.IsBusiness)
                invoiceDetails.Nip = string.Empty;
            using var response = await httpClient.PostAsJsonAsync("api/user/updateInvoiceDetails", invoiceDetails);
            return await response.Content.ReadFromJsonAsync<SimpleServiceResponse>();
        }
    }
}
