using Backend.services.Material;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialPriceController : ControllerBase
    {
        private readonly IMaterialPriceService _materialService;

        public MaterialPriceController(IMaterialPriceService materialService)
        {
            _materialService = materialService;
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableMaterials()
        {
            var materials = await _materialService.GetAvailableMaterialsAsync();
            return Ok(materials);
        }
    }

}
