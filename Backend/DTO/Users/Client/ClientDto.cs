using Backend.Data.Models.Common;

namespace Backend.DTO.Users.Client
{
    public class ClientDto
    {
        public int ID { get; set; }
        public ContactDetails ContactDetails { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
