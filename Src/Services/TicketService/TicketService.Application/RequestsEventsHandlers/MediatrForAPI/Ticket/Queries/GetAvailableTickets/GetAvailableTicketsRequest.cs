using TicketService.Application.DTO.Models;
using TicketService.Application.DTO.ResultType;
using FluentValidation;
using MediatR;

namespace TicketService.Application.RequestsEventsHandlers.MediatrForAPI.AccountRH.Queries.GetAvailableTickets;


public class GetAvailableTicketsRequest : RequestModel, IRequest<Result<GetAvailableTicketsResponse>>
{

}

public class GetAvailableTicketsValidator : AbstractValidator<GetAvailableTicketsRequest>
{
    public GetAvailableTicketsValidator()
    {

    }
}
