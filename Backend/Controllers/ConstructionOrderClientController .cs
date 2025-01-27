using Backend.DTO.Request;
using Backend.services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConstructionOrderClientController : ControllerBase
    {
        private readonly IConstructionOrderService _constructionOrderService;

        public ConstructionOrderClientController(IConstructionOrderService constructionOrderService)
        {
            _constructionOrderService = constructionOrderService;
        }


        [HttpGet("all/{clientId}")]
        public async Task<IActionResult> GetAllOrders(int clientId)
        {
            var orders = await _constructionOrderService.GetOrdersByClientIdAsync(clientId);

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _constructionOrderService.GetOrderByIdAsync(id);

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = await _constructionOrderService.CreateOrderAsync(request);

            return CreatedAtAction(nameof(GetOrder), new { id = order.ID }, order);
        }

        [HttpDelete("{orderId}/{clientId}")]
        public async Task<IActionResult> DeleteOrder(int clientId, int orderId)
        {
            await _constructionOrderService.DeleteOrderAsync(clientId, orderId);

            return NoContent();
        }
    }
}
