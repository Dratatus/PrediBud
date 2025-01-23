﻿using Backend.Data.Models.Orders.Material;
using Backend.DTO.MaterialOrder;

namespace Backend.services
{
    public interface IMaterialOrderService
    {
        Task<MaterialOrderDto> CreateMaterialOrderAsync(MaterialOrderDto dto);
        Task<MaterialOrderDto> GetMaterialOrderByIdAsync(int orderId);
        Task<IEnumerable<MaterialOrderDto>> GetAllMaterialOrdersAsync();
        Task<bool> UpdateMaterialOrderAsync(UpdateMaterialOrderDto dto, int userId);
        Task<bool> DeleteMaterialOrderAsync(int orderId, int userId);
    }
}
