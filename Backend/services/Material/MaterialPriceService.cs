
using Backend.DTO.Price;
using Backend.Repositories;

namespace Backend.services.Material
{
    public class MaterialPriceService : IMaterialPriceService
    {
        private readonly IMaterialPriceRepository _materialRepository;

        public MaterialPriceService(IMaterialPriceRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public async Task<IEnumerable<MaterialPriceDto>> GetAvailableMaterialsAsync()
        {
            var materials = await _materialRepository.GetAllMaterialPricesAsync();
            return materials.Select(m => new MaterialPriceDto
            {
                ID = m.ID,
                MaterialType = m.MaterialType,
                MaterialCategory = m.MaterialCategory,
                PriceWithoutTax = m.PriceWithoutTax,
                SupplierId = m.SupplierId,
                SupplierName = m.Supplier.Name
            }).ToList();
        }
    }

}
