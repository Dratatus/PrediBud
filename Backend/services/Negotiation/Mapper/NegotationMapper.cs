using Backend.Data.Models.Orders.Construction;
using Backend.DTO.ConstructionOrderDto;
using Backend.DTO.Orders;
using Backend.DTO.Users.Client;
using Backend.DTO.Users.Worker;

namespace Backend.services.Negotiation.Mapper
{
    public class NegotationMapper: INegotiationMapper
    {
        public ConstructionOrderDto MapToDto(ConstructionOrder order)
        {
            return new ConstructionOrderDto
            {
                ID = order.ID,
                Description = order.Description,
                Status = order.Status,
                ConstructionType = order.ConstructionType,
                PlacementPhotos = order.placementPhotos,
                RequestedStartTime = order.RequestedStartTime,
                StartDate = order.StartDate,
                EndDate = order.EndDate,
                ClientProposedPrice = order.ClientProposedPrice,
                WorkerProposedPrice = order.WorkerProposedPrice,
                AgreedPrice = order.AgreedPrice,
                TotalPrice = order.TotalPrice,
                Client = order.Client == null ? null : new ClientDto
                {
                    ID = order.Client.ID,
                    ContactDetails = order.Client.ContactDetails,
                    AddressId = order.Client.AddressId,
                    Address = order.Client.Address
                },
                Worker = order.Worker == null ? null : new WorkerDto
                {
                    ID = order.Worker.ID,
                    ContactDetails = order.Worker.ContactDetails,
                    AddressId = order.Worker.AddressId,
                    Address = order.Worker.Address
                },
                Address = new OrderAddressDto
                {
                    City = order.Address.City,
                    PostCode = order.Address.PostCode,
                    StreetName = order.Address.StreetName
                },
                LastActionBy = order.LastActionBy,
                ConstructionSpecification = order.ConstructionSpecification,
                ConstructionSpecificationId = order.ConstructionSpecificationId
            };
        }
        public List<ConstructionOrderDto> MapToDtoList(IEnumerable<ConstructionOrder> orders)
        {
            return orders.Select(MapToDto).ToList();
        }
    }
}
