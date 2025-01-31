using Backend.Data.Consts;
using Backend.Data.Models.Orders;
using Backend.Data.Models.Orders.Material;
using Backend.DTO.Orders;
using Backend.DTO.Orders.Material;
using Backend.DTO.Price;
using Backend.DTO.Users.Supplier;
using Backend.Middlewares;
using Backend.Repositories;
using Backend.Validatiors.Orders.Material;

namespace Backend.services.Material
{
    public class MaterialOrderService : IMaterialOrderService
    {
        private readonly IMaterialOrderRepository _repository;
        private readonly IMaterialPriceRepository _materialPriceRepository;
        private readonly IUserRepository _userRepository;

        public MaterialOrderService(IMaterialOrderRepository repository, IUserRepository userRepository, IMaterialPriceRepository materialPriceRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _materialPriceRepository = materialPriceRepository;
        }
        public async Task<MaterialOrderDetailsDto> CreateMaterialOrderAsync(MaterialOrderDto dto)
        {
            MaterialOrderDtoValidator.Validate(dto);

            var materialPrice = await _materialPriceRepository.GetMaterialPriceByIdAsync(dto.MaterialPriceId.Value);

            if (materialPrice == null)
            {
                throw new ApiException(ErrorMessages.MaterialNotFound, StatusCodes.Status404NotFound);
            }

            if (materialPrice.SupplierId != dto.SupplierId)
            {
                throw new ApiException(ErrorMessages.MaterialDoesNotBelongToSupplier, StatusCodes.Status400BadRequest);
            }

            var entity = MapToEntity(dto);

            await _repository.AddMaterialOrderAsync(entity);
            var createdEntity = await _repository.GetMaterialOrderByIdAsync(entity.ID);

            var createdDto = MapToDto(createdEntity);
            return createdDto;
        }

        public async Task<MaterialOrderDetailsDto> GetMaterialOrderByIdAsync(int orderId)
        {
            var entity = await _repository.GetMaterialOrderByIdAsync(orderId);
            if (entity == null) return null;

            return MapToDto(entity);
        }

        public async Task<IEnumerable<MaterialOrderDetailsDto>> GetAllMaterialOrdersAsync()
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
            existing.Address.City = dto.Address.City;
            existing.Address.PostCode = dto.Address.PostCode;
            existing.Address.StreetName = dto.Address.StreetName;

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
        private MaterialOrderDetailsDto MapToDto(MaterialOrder entity)
        {
            if (entity == null) return null;

            return new MaterialOrderDetailsDto
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
                Supplier = entity.Supplier != null ? new SupplierDto
                {
                    Name = entity.Supplier.Name,
                    ContactEmail = entity.Supplier.ContactEmail,
                    Address = entity.Supplier.Address
                } : null,
                MaterialPriceId = entity.MaterialPriceId,
                MaterialPrice = entity.MaterialPrice != null ? new MaterialPriceDto
                {
                    MaterialType = entity.MaterialPrice.MaterialType,
                    MaterialCategory = entity.MaterialPrice.MaterialCategory,
                    PriceWithoutTax = entity.MaterialPrice.PriceWithoutTax
                } : null,
                Address = entity.Address != null ? new OrderAddressDto
                {
                    City = entity.Address.City,
                    PostCode = entity.Address.PostCode,
                    StreetName = entity.Address.StreetName
                } : null
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
                SupplierId = dto.SupplierId,
                MaterialPriceId = dto.MaterialPriceId,
                Address = new OrderAddress
                {
                    City = dto.Address.City,
                    PostCode = dto.Address.PostCode,
                    StreetName = dto.Address.StreetName
                }
            };
        }
    }
}
