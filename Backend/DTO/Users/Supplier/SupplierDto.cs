using Backend.Data.Models.Common;

namespace Backend.DTO.Users.Supplier
{
    public class SupplierDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public int AddressId { get; set; }
        public string ContactEmail { get; set; }
    }
}
