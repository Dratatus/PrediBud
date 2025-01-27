using Backend.Data.Consts;
using Backend.Data.Models.Common;
using Backend.Data.Models.Orders.Material;
using Backend.DTO.MaterialOrder;
using Backend.DTO.Users.Supplier;
using Backend.Middlewares;
using Backend.Repositories;
using Backend.Validatiors.Orders.Material;

namespace Backend.services
{
    public class MaterialOrderService : IMaterialOrderService
    {
        private readonly IMaterialOrderRepository _repository;
        private readonly IUserRepository _userRepository;

        public MaterialOrderService(IMaterialOrderRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }
        public async Task<MaterialOrderDto> CreateMaterialOrderAsync(MaterialOrderDto dto)
        {
            MaterialOrderDtoValidator.Validate(dto);

            var entity = MapToEntity(dto);

            await _repository.AddMaterialOrderAsync(entity);

            var createdEntity = await _repository.GetMaterialOrderByIdAsync(entity.ID);

            var createdDto = MapToDto(createdEntity);
            return createdDto;
        }

        public async Task<MaterialOrderDto> GetMaterialOrderByIdAsync(int orderId)
        {
            var entity = await _repository.GetMaterialOrderByIdAsync(orderId);
            if (entity == null) return null;

            return MapToDto(entity);
        }

        public async Task<IEnumerable<MaterialOrderDto>> GetAllMaterialOrdersAsync()
        {
            var entities = await _repository.GetAllMaterialOrdersAsync();
            return entities.Select(e => MapToDto(e));
        }

        public async Task<bool> UpdateMaterialOrderAsync(UpdateMaterialOrderDto dto, int userId)
        {
            UpdateMaterialOrderDtoValidator.Validate(dto);

            var existing = await _repository.GetMaterialOrderByIdAsync(dto.ID);

            if (existing == null)
            {
                throw new ApiException(ErrorMessages.MaterialOrderNotFound, StatusCodes.Status404NotFound);
            }

            if (existing.UserId != userId)
            {
                throw new ApiException(ErrorMessages.UnauthorizedAccess, StatusCodes.Status403Forbidden);
            }

            existing.UnitPriceNet = dto.UnitPriceNet;
            existing.UnitPriceGross = dto.UnitPriceGross;
            existing.Quantity = dto.Quantity;
            existing.MaterialPriceId = dto.MaterialPriceId;

            await _repository.UpdateMaterialOrderAsync(existing);
            return true;
        }

        public async Task<bool> DeleteMaterialOrderAsync(int orderId, int userId)
        {
            var existing = await _repository.GetMaterialOrderByIdAsync(orderId);

            if (existing == null)
            {
                throw new ApiException(ErrorMessages.MaterialOrderNotFound, StatusCodes.Status404NotFound);
            }

            if (existing.UserId != userId)
            {
                throw new ApiException(ErrorMessages.UnauthorizedAccess, StatusCodes.Status403Forbidden);
            }

            await _repository.DeleteMaterialOrderAsync(orderId);
            return true;
        }
        private MaterialOrderDto MapToDto(MaterialOrder entity)
        {
            if (entity == null) return null;

            return new MaterialOrderDto
            {
                ID = entity.ID,
                UnitPriceNet = entity.UnitPriceNet,
                UnitPriceGross = entity.UnitPriceGross,
                Quantity = entity.Quantity,
                TotalPriceNet = entity.TotalPriceNet,
                TotalPriceGross = entity.TotalPriceGross,
                CreatedDate = entity.CreatedDate,

                UserId = entity.UserId,
                SupplierId = entity.SupplierId,
                Supplier = new SupplierDto
                {
                    Name = entity.Supplier.Name,
                    ContactEmail = entity.Supplier.ContactEmail,
                    Address = entity.Supplier.Address
                },
                MaterialPriceId = entity.MaterialPriceId,
                MaterialPrice = entity.MaterialPrice

            };
        }

        private MaterialOrder MapToEntity(MaterialOrderDto dto)
        {
            if (dto == null) return null;

            return new MaterialOrder
            {
                ID = dto.ID,
                UnitPriceNet = dto.UnitPriceNet,
                UnitPriceGross = dto.UnitPriceGross,
                Quantity = dto.Quantity,
                CreatedDate = dto.CreatedDate,

                UserId = dto.UserId,
                SupplierId = dto.Supplier.ID,
                MaterialPriceId = dto.MaterialPriceId
            };
        }
    }
}
