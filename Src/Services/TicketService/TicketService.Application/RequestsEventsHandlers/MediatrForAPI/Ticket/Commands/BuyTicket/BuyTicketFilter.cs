using TicketService.Application.DTO.ResultType;
using FluentValidation;
using MediatR;


namespace TicketService.Application.RequestsEventsHandlers.MediatrForAPI.Ticket.Commands.BuyTicket;



public class BuyTicketFilter : IPipelineBehavior<BuyTicketRequest, Result<BuyTicketResponse>>
//where TResponse : IResult

{

    private readonly IValidator<BuyTicketRequest> _validator;

    public BuyTicketFilter(IValidator<BuyTicketRequest> validator = null)
    {
        _validator = validator;
    }

    public async Task<Result<BuyTicketResponse>> Handle(BuyTicketRequest request, RequestHandlerDelegate<Result<BuyTicketResponse>> next, CancellationToken cancellationToken)
    {
        try
        {

            return await next();

        }
        catch
        {
            throw;
        }

    }

}
