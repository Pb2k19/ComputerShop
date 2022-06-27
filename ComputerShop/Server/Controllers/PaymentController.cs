using ComputerShop.Server.Services.Order;
using ComputerShop.Server.Services.Payment;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.Products;
using ComputerShop.Shared.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerShop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService paymentService;
        private readonly IOrderService orderService;

        public PaymentController(IPaymentService paymentService, IOrderService orderService)
        {
            this.paymentService = paymentService;
            this.orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<SimpleServiceResponse>> FullfillOrder()
        {
            var re = await paymentService.FulfillOrder(Request);
            if (!re.Success)
                return BadRequest(re.Message);
            else
                return Ok(re);
        }

        [HttpPost("create-checkout/{orderId}")]
        public async Task<ActionResult<string>> CreateCheckout(List<ProductCartItem> cartItems, [FromRoute] string orderId)
        {
            var orderFromDb = (await orderService.GetOrderAsync(orderId)).Data;
            
            if (orderFromDb == null || orderFromDb.State != OrderStates.Unpaid)
                return BadRequest();
            decimal sum = 0;
            cartItems.ForEach(c => sum += c.Product.Price);
            if (orderFromDb.Total != sum)
                return BadRequest();

            string url = string.Empty;
            try
            {
                url = (paymentService.CreateCheckout(cartItems, orderFromDb.DeliveryDetails.Email)).Url;
            }
            catch (Exception)
            {
                return NotFound(string.Empty);
            }
            
            return Ok(url);
        }
    }
}
