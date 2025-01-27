using Backend.Data.Models.Suppliers;

namespace Backend.Repositories
{
    public interface IMaterialPriceRepository
    {
        Task<MaterialPrice> GetMaterialPriceByIdAsync(int id);
        Task<IEnumerable<MaterialPrice>> GetAllMaterialPricesAsync();
        Task AddMaterialPriceAsync(MaterialPrice materialPrice);
        Task UpdateMaterialPriceAsync(MaterialPrice materialPrice);
        Task DeleteMaterialPriceAsync(int id);
    }
}
