using ComputerShop.Server.Services.User;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;

namespace ComputerShop.Server.Services.UserDetails
{
    public class UserDetails : IUserDetails
    {
        private readonly IUserService userService;

        public UserDetails(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<ServiceResponse<DeliveryDetails>> GetDeliveryDetailsAsync(string? userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return new ServiceResponse<DeliveryDetails> { Message = "Coś poszło nie tak", Success = false };
            var user = await userService.GetUserByIdAsync(userId);
            if(user == null || user.)
                return new ServiceResponse<DeliveryDetails> { Message = "Nie można znaleźć użytkownika z podanym id", Success = false };
            return new ServiceResponse<DeliveryDetails>
        }

        public Task<ServiceResponse<DeliveryDetails>> GetDeliveryDetailsAsync()
        {
            return GetDeliveryDetailsAsync(userService.GetUserId());
        }

        public Task<ServiceResponse<InvoiceDetails>> GetInvoiceDetailsAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<InvoiceDetails>> GetInvoiceDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SimpleServiceResponse> UpdateDeliveryDetailsAsync(DeliveryDetails deliveryDetails)
        {
            throw new NotImplementedException();
        }

        public Task<SimpleServiceResponse> UpdateInvoiceDetailsAsync(InvoiceDetails invoiceDetails)
        {
            throw new NotImplementedException();
        }

        public Task<SimpleServiceResponse> UpdateInvoiceDetailsForBusinessAsync(InvoiceDetailsForBusiness invoiceDetails)
        {
            throw new NotImplementedException();
        }
    }
}
