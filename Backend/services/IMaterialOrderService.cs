using Backend.Data.Models.Orders.Material;

namespace Backend.services
{
    public interface IMaterialOrderService
    {
        Task<MaterialOrder> CreateMaterialOrderAsync(MaterialOrder order);
        Task<MaterialOrder> GetMaterialOrderByIdAsync(int orderId);
        Task<IEnumerable<MaterialOrder>> GetAllMaterialOrdersAsync();
        Task<bool> UpdateMaterialOrderAsync(MaterialOrder order);
        Task<bool> DeleteMaterialOrderAsync(int orderId);
    }
}
