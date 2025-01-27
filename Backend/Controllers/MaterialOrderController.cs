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
            {
                return NotFound();
            }

            return Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var dtos = await _service.GetAllMaterialOrdersAsync();
            return Ok(dtos);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(int userId, [FromBody] UpdateMaterialOrderDto updatedDto)
        {
            await _service.UpdateMaterialOrderAsync(updatedDto, userId);

            return NoContent();
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId, int userId)
        {
            await _service.DeleteMaterialOrderAsync(orderId, userId);

            return NoContent();
        }
    }
}
