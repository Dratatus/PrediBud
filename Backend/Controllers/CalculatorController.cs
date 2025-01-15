using Backend.Data.Models.Constructions.Specyfication;
using Backend.services.Calculator;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculatorService _calculatorService;

        public CalculatorController(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> CalculatePrice([FromBody] ConstructionSpecification specification)
        {
            if (specification == null)
            {
                return BadRequest("Specification cannot be null.");
            }

            try
            {
                var calculatedPrice = await _calculatorService.CalculatePriceAsync(specification);
                return Ok(calculatedPrice);
            }
            catch (NotSupportedException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
