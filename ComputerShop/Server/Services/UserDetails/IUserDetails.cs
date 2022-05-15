using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;

namespace ComputerShop.Server.Services.UserDetails
{
    public interface IUserDetails
    {
        Task<ServiceResponse<DeliveryDetails>> GetDeliveryDetailsAsync(string? userId);
        Task<ServiceResponse<DeliveryDetails>> GetDeliveryDetailsAsync();
        Task<SimpleServiceResponse> UpdateDeliveryDetailsAsync(DeliveryDetails deliveryDetails);
        Task<ServiceResponse<InvoiceDetails>> GetInvoiceDetailsAsync(string? userId);
        Task<ServiceResponse<InvoiceDetails>> GetInvoiceDetailsAsync();
        Task<SimpleServiceResponse> UpdateInvoiceDetailsAsync(InvoiceDetails invoiceDetails);
        Task<SimpleServiceResponse> UpdateInvoiceDetailsForBusinessAsync(InvoiceDetailsForBusiness invoiceDetails);
    }
}
