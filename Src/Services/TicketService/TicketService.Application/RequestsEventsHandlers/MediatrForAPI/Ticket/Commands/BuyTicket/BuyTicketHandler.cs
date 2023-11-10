using AutoMapper;
using Contracts;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TicketService.Application.DTO.DataObjects;
using TicketService.Application.DTO.Exceptions;
using TicketService.Application.DTO.ResultType;
using TicketService.DAL.MainDB.Repositories.Interfaces;

namespace TicketService.Application.RequestsEventsHandlers.MediatrForAPI.Ticket.Commands.BuyTicket;

public class BuyTicketHandler : IRequestHandler<BuyTicketRequest, Result<BuyTicketResponse>>
{
    private readonly ILogger<BuyTicketHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ITicketRepository _ticketRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IUnitOfWork _uow;
    private readonly IBus _bus;

    public BuyTicketHandler(ILogger<BuyTicketHandler> logger, IMapper mapper, ITicketRepository ticketRepository, IHttpContextAccessor contextAccessor, IUnitOfWork uow, IBus bus)
    {
        _logger = logger;
        _mapper = mapper;
        _ticketRepository = ticketRepository;
        _contextAccessor = contextAccessor;
        _uow = uow;
        _bus = bus;
    }

    public async Task<Result<BuyTicketResponse>> Handle(BuyTicketRequest request, CancellationToken cancellationToken)
    {
        var result = new Result<BuyTicketResponse>(IsSuccess: true, Message: "", ResponseLogging: true);
        var model = new BuyTicketResponse();
        try
        {
            var ticket_entity = await _ticketRepository.GetAsFirstOrDefaultAsync(i => i.IsSold != true && i.Uid == request.Uid);
            if (ticket_entity == null)
            {
                result.IsSuccess = false;
                result.Message = "Bu bilet daha önce satıldı.";
                return result;
            }
            var ticket = _mapper.Map<TicketDO>(ticket_entity);
            await _bus.Publish(new PaymentRequestEvent { Uid = ticket.Uid, Title = ticket.Title, IsSold = ticket.IsSold.Value, Price = ticket.Price.Value },cancellationToken: cancellationToken);
            result.Message = "Satın işlemi kaydedildi, ödeme işlemi devam ediyor lütfen bekleyiniz.";
            result.IsSuccess = true;
        }        
        catch (Exception ex)
        {
            throw new ModelException(new Result<object> { IsSuccess = false, ResultType = ResultTypeEnum.Warning, Data = request, Message = ex.Message });
        }
        return result;
    }


}

