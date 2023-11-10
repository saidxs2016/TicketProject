using TicketService.Application.DTO.Models;

namespace TicketService.Application.RequestsEventsHandlers.MediatrForAPI.Ticket.Commands.BuyTicket;

public class BuyTicketResponse : ResponseModel
{
    public bool IsSold { get; set; }
}

