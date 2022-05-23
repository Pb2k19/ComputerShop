using ComputerShop.Server.DataAccess;
using ComputerShop.Server.Services.User;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;

namespace ComputerShop.Server.Services.UserDetails
{
    public class UserDetailsService : IUserDetailsService
    {
        private readonly IUserService userService;
        private readonly IUserData userData;

        public UserDetailsService(IUserService userService, IUserData userData)
        {
            this.userService = userService;
            this.userData = userData;
        }

        public async Task<ServiceResponse<DeliveryDetails>> GetDeliveryDetailsAsync(string? userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return new ServiceResponse<DeliveryDetails> { Message = "Coś poszło nie tak", Success = false };
            var user = await userService.GetRegisteredUserByIdAsync(userId);
            if(user == null || user.DeliveryDetails == null)
                return new ServiceResponse<DeliveryDetails> { Message = "Nie można znaleźć użytkownika z podanym id", Success = false };
            return new ServiceResponse<DeliveryDetails> { Data = user.DeliveryDetails };
        }

        public async Task<ServiceResponse<DeliveryDetails>> GetDeliveryDetailsAsync()
        {
            return await GetDeliveryDetailsAsync(userService.GetUserId());
        }

        public async Task<ServiceResponse<InvoiceDetails>> GetInvoiceDetailsAsync(string? userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return new ServiceResponse<InvoiceDetails> { Message = "Coś poszło nie tak", Success = false };
            var user = await userService.GetRegisteredUserByIdAsync(userId);
            if (user == null || user.DeliveryDetails == null)
                return new ServiceResponse<InvoiceDetails> { Message = "Nie można znaleźć użytkownika z podanym id", Success = false };
            return new ServiceResponse<InvoiceDetails> { Data = user.InvoiceDetails };
        }

        public async Task<ServiceResponse<InvoiceDetails>> GetInvoiceDetailsAsync()
        {            
            return await GetInvoiceDetailsAsync(userService.GetUserId());
        }

        public async Task<SimpleServiceResponse> UpdateDeliveryDetailsAsync(DeliveryDetails deliveryDetails)
        {
            if (deliveryDetails == null)
                return new SimpleServiceResponse { Message = "Nie można zaktualizować danych", Success = false };
            string? userId = userService.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return new SimpleServiceResponse { Message = "Coś poszło nie tak", Success = false };
            var user = await userService.GetRegisteredUserByIdAsync(userId);
            if (user == null || user.DeliveryDetails == null)
                return new SimpleServiceResponse { Message = "Nie można znaleźć użytkownika z podanym id", Success = false };
            user.DeliveryDetails = deliveryDetails;
            await userData.UpdateUserAsync(user);
            return new SimpleServiceResponse { Success = true };
        }

        public async Task<SimpleServiceResponse> UpdateInvoiceDetailsAsync(InvoiceDetails invoiceDetails)
        {
            if (invoiceDetails == null)
                return new SimpleServiceResponse { Message = "Nie można zaktualizować danych", Success = false };
            string? userId = userService.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return new SimpleServiceResponse { Message = "Coś poszło nie tak", Success = false };
            var user = await userService.GetRegisteredUserByIdAsync(userId);
            if (user == null || user.DeliveryDetails == null)
                return new SimpleServiceResponse { Message = "Nie można znaleźć użytkownika z podanym id", Success = false };
            user.InvoiceDetails = invoiceDetails;
            await userData.UpdateUserAsync(user);
            return new SimpleServiceResponse { Success = true };
        }
    }
}
