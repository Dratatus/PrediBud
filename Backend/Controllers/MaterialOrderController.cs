using Backend.Data.Models.Orders.Material;
using Backend.services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialOrderController : ControllerBase
    {
        private readonly IMaterialOrderService _service;

        public MaterialOrderController(IMaterialOrderService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(MaterialOrder order)
        {
            var createdOrder = await _service.CreateMaterialOrderAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { orderId = createdOrder.ID }, createdOrder);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await _service.GetMaterialOrderByIdAsync(orderId);
            if (order == null)
                return NotFound($"Order with ID {orderId} not found.");

            return Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _service.GetAllMaterialOrdersAsync();
            return Ok(orders);
        }

        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(int orderId, MaterialOrder updatedOrder)
        {
            if (orderId != updatedOrder.ID)
                return BadRequest("Order ID mismatch.");

            var success = await _service.UpdateMaterialOrderAsync(updatedOrder);
            if (!success)
                return NotFound($"Order with ID {orderId} not found.");

            return NoContent();
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var success = await _service.DeleteMaterialOrderAsync(orderId);
            if (!success)
                return NotFound($"Order with ID {orderId} not found.");

            return NoContent();
        }
    }
}
