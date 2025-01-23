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

        [HttpPost("{orderId}/reject")]
        public async Task<IActionResult> RejectNegotiation(int orderId, [FromBody] RejectNegotiationRequest request)
        {
            var success = await _negotiationService.RejectNegotiation(orderId, request.UserId);
            if (!success)
                return BadRequest("Unable to reject negotiation.");

            return Ok("Negotiation has been rejected.");
        }

        [HttpPost("{orderId}/continue")]
        public async Task<IActionResult> ContinueNegotiation(int orderId, [FromBody] ContinueNegotiationRequest request)
        {
            var success = await _negotiationService.ContinueNegotiation(orderId, request.UserId, request.ProposedPrice);

            if (!success)
                return BadRequest("Unable to continue negotiation.");

            return Ok("Negotiation has been continued.");
        }

        [HttpPost("{orderId}/complete")]
        public async Task<IActionResult> CompleteOrder(int orderId, [FromBody] CompleteOrderRequest request)
        {
            var success = await _negotiationService.CompleteOrder(orderId, request.UserId);

            if (!success)
                return BadRequest("Unable to continue negotiation.");

            return Ok("Negotiation has been continued.");
        }
    }
}
