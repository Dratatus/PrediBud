using Backend.Data.Models.Orders.Material;

namespace Backend.Repositories
{
    public interface IMaterialOrderRepository
    {
        Task<MaterialOrder> GetMaterialOrderByIdAsync(int orderId);
        Task<IEnumerable<MaterialOrder>> GetAllMaterialOrdersAsync();
        Task AddMaterialOrderAsync(MaterialOrder materialOrder);
        Task UpdateMaterialOrderAsync(MaterialOrder materialOrder);
        Task DeleteMaterialOrderAsync(int orderId);
    }
}
