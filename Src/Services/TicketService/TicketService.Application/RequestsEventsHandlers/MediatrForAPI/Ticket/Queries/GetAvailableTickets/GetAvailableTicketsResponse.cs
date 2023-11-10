using TicketService.Application.DTO.DataObjects;
using TicketService.Application.DTO.Models;

namespace TicketService.Application.RequestsEventsHandlers.MediatrForAPI.AccountRH.Queries.GetAvailableTickets;

public class GetAvailableTicketsResponse : ResponseModel
{
    public List<TicketDO> Tickets { get; set; }

}

