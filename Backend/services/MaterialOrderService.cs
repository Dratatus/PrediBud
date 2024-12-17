using Backend.Data.Models.Orders.Material;
using Backend.Repositories;

namespace Backend.services
{
    public class MaterialOrderService : IMaterialOrderService
    {
        private readonly IMaterialOrderRepository _repository;

        public MaterialOrderService(IMaterialOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<MaterialOrder> CreateMaterialOrderAsync(MaterialOrder order)
        {
            await _repository.AddMaterialOrderAsync(order);
            return order;
        }

        public async Task<MaterialOrder> GetMaterialOrderByIdAsync(int orderId)
        {
            return await _repository.GetMaterialOrderByIdAsync(orderId);
        }

        public async Task<IEnumerable<MaterialOrder>> GetAllMaterialOrdersAsync()
        {
            return await _repository.GetAllMaterialOrdersAsync();
        }

        public async Task<bool> UpdateMaterialOrderAsync(MaterialOrder order)
        {
            var existingOrder = await _repository.GetMaterialOrderByIdAsync(order.ID);
            if (existingOrder == null)
                return false;

            existingOrder.MaterialCategory = order.MaterialCategory;
            existingOrder.MaterialType = order.MaterialType;
            existingOrder.Quantity = order.Quantity;
            existingOrder.Taxes = order.Taxes;
            existingOrder.SupplierId = order.SupplierId;
            existingOrder.UnitPrice = order.UnitPrice;

            await _repository.UpdateMaterialOrderAsync(existingOrder);
            return true;
        }

        public async Task<bool> DeleteMaterialOrderAsync(int orderId)
        {
            var order = await _repository.GetMaterialOrderByIdAsync(orderId);
            if (order == null)
                return false;

            await _repository.DeleteMaterialOrderAsync(orderId);
            return true;
        }
    }
}
