using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TicketService.Application.DTO.DataObjects;
using TicketService.Application.DTO.Exceptions;
using TicketService.Application.DTO.ResultType;
using TicketService.DAL.MainDB.Repositories.Interfaces;

namespace TicketService.Application.RequestsEventsHandlers.MediatrForAPI.AccountRH.Queries.GetAvailableTickets;

public class GetAvailableTicketsHandler : IRequestHandler<GetAvailableTicketsRequest, Result<GetAvailableTicketsResponse>>
{
    private readonly ILogger<GetAvailableTicketsHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ITicketRepository _ticketRepository;
    private readonly IUnitOfWork _uow;

    public GetAvailableTicketsHandler(ILogger<GetAvailableTicketsHandler> logger, IMapper mapper, ITicketRepository ticketRepository, IUnitOfWork uow)
    {
        _logger = logger;
        _mapper = mapper;
        _ticketRepository = ticketRepository;
        _uow = uow;
    }

    public async Task<Result<GetAvailableTicketsResponse>> Handle(GetAvailableTicketsRequest request, CancellationToken cancellationToken)
    {
        var result = new Result<GetAvailableTicketsResponse>(IsSuccess: true, Message: "", ResponseLogging: true);
        var model = new GetAvailableTicketsResponse();
        try
        {
            var tickets_entites = await _ticketRepository.GetAsWhereAsync(i => i.IsSold != true);
            model.Tickets = _mapper.Map<List<TicketDO>>(tickets_entites);
            result.Data = model;
            result.IsSuccess = true;
        }
        catch (Exception ex)
        {
            throw new ModelException(new Result<object> { IsSuccess = false, ResultType = ResultTypeEnum.Warning, Data = request, Message = ex.Message });
        }
        return result;
    }

    


}

