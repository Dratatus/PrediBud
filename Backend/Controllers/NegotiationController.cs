using Backend.DTO.Request;
using Backend.services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NegotiationController : ControllerBase
    {
        private readonly INegotiationService _negotiationService;

        public NegotiationController(INegotiationService negotiationService)
        {
            _negotiationService = negotiationService;
        }

        [HttpPost("{orderId}/initiate")]
        public async Task<IActionResult> InitiateNegotiation(int orderId, [FromBody] InitiateNegotiationRequest request)
        {
             await _negotiationService.InitiateNegotiation(orderId, request.WorkerId, request.ProposedPrice);

            return Ok("Negotiation has been initiated.");
        }

        [HttpPost("{orderId}/accept")]
        public async Task<IActionResult> AcceptNegotiation(int orderId, [FromBody] AcceptNegotiationRequest request)
        {
             await _negotiationService.AcceptNegotiation(orderId, request.ClientId);

            return Ok("Negotiation has been accepted.");
        }

        [HttpPost("{orderId}/reject")]
        public async Task<IActionResult> RejectNegotiation(int orderId, [FromBody] RejectNegotiationRequest request)
        {
             await _negotiationService.RejectNegotiation(orderId, request.UserId);

            return Ok("Negotiation has been rejected.");
        }

        [HttpPost("{orderId}/continue")]
        public async Task<IActionResult> ContinueNegotiation(int orderId, [FromBody] ContinueNegotiationRequest request)
        {
            await _negotiationService.ContinueNegotiation(orderId, request.UserId, request.ProposedPrice);

            return Ok("Negotiation has been continued.");
        }

        [HttpPost("{orderId}/complete")]
        public async Task<IActionResult> CompleteOrder(int orderId, [FromBody] CompleteOrderRequest request)
        {
            await _negotiationService.CompleteOrder(orderId, request.UserId);

            return Ok("Negotiation has been continued.");
        }
    }
}
