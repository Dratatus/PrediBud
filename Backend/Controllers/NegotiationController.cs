using Backend.DTO.Request;
using Backend.services;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
            var success = await _negotiationService.InitiateNegotiation(orderId, request.WorkerId, request.ProposedPrice);
            if (!success)
                return BadRequest("Unable to initiate negotiation.");

            return Ok("Negotiation has been initiated.");
        }

        [HttpPost("{orderId}/accept")]
        public async Task<IActionResult> AcceptNegotiation(int orderId, [FromBody] AcceptNegotiationRequest request)
        {
            var success = await _negotiationService.AcceptNegotiation(orderId, request.ClientId);
            if (!success)
                return BadRequest("Unable to accept negotiation.");

            return Ok("Negotiation has been accepted.");
        }

        // Odrzucenie negocjacji przez Klienta lub Workera
        [HttpPost("{orderId}/reject")]
        public async Task<IActionResult> RejectNegotiation(int orderId, [FromBody] RejectNegotiationRequest request)
        {
            var success = await _negotiationService.RejectNegotiation(orderId, request.UserId, request.IsClient);
            if (!success)
                return BadRequest("Unable to reject negotiation.");

            return Ok("Negotiation has been rejected.");
        }
    }
}
