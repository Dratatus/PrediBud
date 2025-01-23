using Backend.Data.Models.Users;
using Backend.services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConstructionOrderWorkerController : ControllerBase
    {
        private readonly IConstructionOrderService _constructionOrderService;

        public ConstructionOrderWorkerController(IConstructionOrderService constructionOrderService)
        {
            _constructionOrderService = constructionOrderService;
        }

        [HttpGet("available/{workerId}")]
        public async Task<IActionResult> GetAvailableOrders(int workerId)
        {
            var orders = await _constructionOrderService.GetAvailableOrdersAsync(workerId);
            if (orders == null || !orders.Any())
            {
                return NotFound("No available orders found.");
            }

            return Ok(orders);
        }

        [HttpGet("my-orders/{workerId}")]
        public async Task<IActionResult> GetMyOrders(int workerId)
        {
            var orders = await _constructionOrderService.GetOrdersByWorkerIdAsync(workerId);
            if (orders == null || !orders.Any())
            {
                return NotFound($"No orders found for worker with ID {workerId}");
            }

            return Ok(orders);
        }
    }
}
