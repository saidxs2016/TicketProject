using FluentValidation;
using MediatR;
using TicketService.Application.DTO.ResultType;

namespace TicketService.Application.RequestsEventsHandlers.MediatrForAPI.Ticket.Commands.BuyTicket;


public class BuyTicketRequest : IRequest<Result<BuyTicketResponse>>
{
    public Guid Uid { get; set; }
    public decimal Price { get; set; }

}

public class BuyTicketValidator : AbstractValidator<BuyTicketRequest>
{
    public BuyTicketValidator()
    {
        RuleFor(p => p.Uid)
            .NotNull().WithMessage("Geçersiz İşlem")
            .NotEmpty().WithMessage("Geçersiz İşlem");

        RuleFor(p => p.Price)
            .NotNull().WithMessage("Geçersiz Fiyat")
            .NotEmpty().WithMessage("Geçersiz Fiyat")
            .Must(x => x > 0).WithMessage("Geçersiz Fiyat");

    }
}
