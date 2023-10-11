using LibraryWebAPI.Services.OrderService;
using Microsoft.AspNetCore.Authorization;

namespace LibraryWebAPI.Controllers
{
    [Route("/Orders")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> AddOrder(OrderDTO order)
        {
            var result = await _orderService.AddOrderAsync(order);
            return Ok(result);
        }
    }
}
