namespace Backend.DTO.Request
{
    public class InitiateNegotiationRequest
    {
        public int WorkerId { get; set; }
        public decimal ProposedPrice { get; set; }
    }

}
