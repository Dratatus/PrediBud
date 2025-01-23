using Backend.Data.Models.Common;
using Backend.Data.Models.Credidentials;

namespace Backend.DTO.Users.Worker
{
    public class WorkerDto
    {
        public int ID { get; set; }
        public string Position { get; set; }
        public ContactDetails ContactDetails { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }

}
