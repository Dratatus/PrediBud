using Backend.Data.Models.Orders.Material;
using Backend.Data.Models.Suppliers;
using Backend.Data.Models.Users;
using Backend.DTO.MaterialOrder;
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
        public async Task<IActionResult> CreateOrder([FromBody] MaterialOrderDto dto)
        {
            var createdDto = await _service.CreateMaterialOrderAsync(dto);
            return CreatedAtAction(nameof(GetOrderById), new { orderId = createdDto.ID }, createdDto);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var dto = await _service.GetMaterialOrderByIdAsync(orderId);
            if (dto == null)
                return NotFound($"Order with ID {orderId} not found.");

            return Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var dtos = await _service.GetAllMaterialOrdersAsync();
            return Ok(dtos);
        }

        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(int orderId, [FromBody] MaterialOrderDto updatedDto)
        {
            if (orderId != updatedDto.ID)
                return BadRequest("Order ID mismatch.");

            var success = await _service.UpdateMaterialOrderAsync(updatedDto);
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
