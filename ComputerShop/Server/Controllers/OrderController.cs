using ComputerShop.Server.Services.Order;
using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace ComputerShop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost("addOrder")]
        public async Task<ActionResult<ServiceResponse<OrderModel>>> AddOrder(OrderInfo orderInfo)
        {
            if (orderInfo.DeliveryDetails == null || orderInfo.CartItems == null || orderInfo.CartItems.Count < 1)
                return BadRequest(new ServiceResponse<OrderModel> { Success = false, Message = "Dane nie mogę mieć wartości null" });
            if(orderInfo.InvoiceDetails == null)
                orderInfo.InvoiceDetails = new InvoiceDetails { City = orderInfo.DeliveryDetails.City, Name = orderInfo.DeliveryDetails.Name, 
                                                                Street = $"{orderInfo.DeliveryDetails.Street } { orderInfo.DeliveryDetails.HouseNumber }" };
            return Ok(await orderService.AddOrderAsync(orderInfo.CartItems, orderInfo.DeliveryDetails, orderInfo.InvoiceDetails));
        }

        [HttpPost("addOrderForBusiness")]
        public async Task<ActionResult<ServiceResponse<OrderModel>>> AddOrderForBusiness(OrderInfoForBussiness orderInfo)
        {
            if (orderInfo.DeliveryDetails == null || orderInfo.CartItems == null || orderInfo.InvoiceDetails == null || orderInfo.CartItems.Count < 1)
                return BadRequest(new ServiceResponse<OrderModel> { Success = false, Message = "Dane nie mogę mieć wartości null" });
            return Ok(await orderService.AddOrderAsync(orderInfo.CartItems, orderInfo.DeliveryDetails, orderInfo.InvoiceDetails));
        }
    }
}
