namespace Backend.DTO.Request
{
    public class RejectNegotiationRequest
    {
        public int UserId { get; set; }
        public bool IsClient { get; set; }
    }
}
