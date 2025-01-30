using Backend.Data.Models.Orders.Material;
using Backend.DTO.Orders.Material;

namespace Backend.services.Material
{
    public interface IMaterialOrderService
    {
        Task<MaterialOrderDetailsDto> CreateMaterialOrderAsync(MaterialOrderDto dto);
        Task<MaterialOrderDetailsDto> GetMaterialOrderByIdAsync(int orderId);
        Task<IEnumerable<MaterialOrderDetailsDto>> GetAllMaterialOrdersAsync();
        Task<bool> UpdateMaterialOrderAsync(UpdateMaterialOrderDto dto, int userId);
        Task<bool> DeleteMaterialOrderAsync(int orderId, int userId);
    }
}
