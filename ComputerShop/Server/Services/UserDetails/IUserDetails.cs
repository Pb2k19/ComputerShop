using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;

namespace ComputerShop.Server.Services.UserDetails
{
    public interface IUserDetailsService
    {
        Task<ServiceResponse<DeliveryDetails>> GetDeliveryDetailsAsync(string? userId);
        Task<ServiceResponse<DeliveryDetails>> GetDeliveryDetailsAsync();
        Task<ServiceResponse<InvoiceDetails>> GetInvoiceDetailsAsync(string? userId);
        Task<ServiceResponse<InvoiceDetails>> GetInvoiceDetailsAsync();
        Task<SimpleServiceResponse> UpdateDeliveryDetailsAsync(DeliveryDetails deliveryDetails);
        Task<SimpleServiceResponse> UpdateInvoiceDetailsAsync(InvoiceDetails invoiceDetails);
    }
}
