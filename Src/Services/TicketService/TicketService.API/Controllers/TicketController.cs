using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketService.Application.RequestsEventsHandlers.MediatrForAPI.AccountRH.Queries.GetAvailableTickets;
using TicketService.Application.RequestsEventsHandlers.MediatrForAPI.Ticket.Commands.BuyTicket;

namespace TicketService.API.Controllers
{
    [ApiController]
    [Route("api/ticketservice/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly IMediator _mediator;


        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("Tickets")]
        public async Task<IActionResult> GetTickets([FromQuery] GetAvailableTicketsRequest model) => Ok(await _mediator.Send(model));


        [Authorize]
        [HttpPost("BuyTicket")]
        public async Task<IActionResult> BuyTicket([FromBody] BuyTicketRequest model) => Ok(await _mediator.Send(model));
    }
}