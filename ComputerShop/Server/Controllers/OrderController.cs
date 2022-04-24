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

        [HttpPost("addOrderaddOrder")]
        public async Task<ActionResult<ServiceResponse<OrderModel>>> AddOrder(OrderInfo orderInfo)
        {
            return Ok(await orderService.AddOrderAsync(orderInfo.CartItems, orderInfo.DeliveryDetails));
        }
    }
}
