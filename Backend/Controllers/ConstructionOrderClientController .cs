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
            if (orders == null || !orders.Any())
            {
                return NotFound($"No orders found for client with ID {clientId}");
            }

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _constructionOrderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var order = await _constructionOrderService.CreateOrderAsync(request);
                return CreatedAtAction(nameof(GetOrder), new { id = order.ID }, order);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{orderId}/{clientId}")]
        public async Task<IActionResult> DeleteOrder(int clientId, int orderId)
        {
            var success = await _constructionOrderService.DeleteOrderAsync(clientId, orderId);

            if (!success)
            {
                return Forbid("Order not found or access denied.");
            }

            return NoContent();
        }
    }
}
