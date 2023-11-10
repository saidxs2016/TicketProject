using TicketService.Application.DTO.ResultType;
using FluentValidation;
using MediatR;


namespace TicketService.Application.RequestsEventsHandlers.MediatrForAPI.AccountRH.Queries.GetAvailableTickets;



public class GetAvailableTicketsFilter : IPipelineBehavior<GetAvailableTicketsRequest, Result<GetAvailableTicketsResponse>>
//where TResponse : IResult

{

    private readonly IValidator<GetAvailableTicketsRequest> _validator;

    public GetAvailableTicketsFilter(IValidator<GetAvailableTicketsRequest> validator = null)
    {
        _validator = validator;
    }

    public async Task<Result<GetAvailableTicketsResponse>> Handle(GetAvailableTicketsRequest request, RequestHandlerDelegate<Result<GetAvailableTicketsResponse>> next, CancellationToken cancellationToken)
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
