using Backend.DTO.Price;

namespace Backend.services.Material
{
    public interface IMaterialPriceService
    {
        Task<IEnumerable<MaterialPriceDto>> GetAvailableMaterialsAsync();
    }
}
