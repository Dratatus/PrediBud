namespace Backend.DTO.Request
{
    public class ContinueNegotiationRequest
    {
        public int UserId { get; set; }
        public decimal ProposedPrice { get; set; }
    }
}
