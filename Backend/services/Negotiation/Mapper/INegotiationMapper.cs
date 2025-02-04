using Backend.Data.Models.Orders.Construction;
using Backend.DTO.ConstructionOrderDto;

namespace Backend.services.Negotiation.Mapper
{
    public interface INegotiationMapper
    {
        ConstructionOrderDto MapToDto(ConstructionOrder order);
        List<ConstructionOrderDto> MapToDtoList(IEnumerable<ConstructionOrder> orders);
    }
}
