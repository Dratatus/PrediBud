using Backend.Data.Models.Suppliers;

namespace Backend.Providers
{
    public interface ISupplierDataProvider
    {
        Task<List<Supplier>> GetSuppliersAsync();
    }
}
