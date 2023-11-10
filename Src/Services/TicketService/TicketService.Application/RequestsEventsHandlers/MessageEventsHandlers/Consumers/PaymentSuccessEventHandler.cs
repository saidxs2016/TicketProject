using Contracts;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using TicketService.DAL.MainDB.Repositories.Interfaces;

namespace TicketService.Application.RequestsEventsHandlers.MessageEventsHandlers.Consumers;



public class PaymentSuccessEventHandler : IConsumer<PaymentSuccessEvent>
{

    private readonly IServiceScopeFactory _serviceScopeFactory;
    public PaymentSuccessEventHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Consume(ConsumeContext<PaymentSuccessEvent> context)
    {
        // Not: eğer işlem yapılırken bu servisin diğer kaynakları kullanılması gerekiyor ise
        //      yeni scope oluşturup yapılması gerekiyor yoksa 
        //      diğer kaynakları kullanan başka nesneler ile çakışma yaşanacak
        //      eğer bu service'in kaynakları kullanılması söz konusu değil ise normal dependency injection kullanılıabilir.

        try
        {
            // =============== Create New Scope ===============        
            await using var scope = _serviceScopeFactory.CreateAsyncScope();

            // =============== Get Your Services ===============
            var _ticketRepository = scope.ServiceProvider.GetService<ITicketRepository>();
            var _uow = scope.ServiceProvider.GetService<IUnitOfWork>();

            // =============== Write Your Code ===============
            var uid = context.Message.Uid;
            var ticket_entity = await _ticketRepository.GetAsFirstOrDefaultAsync(i => i.Uid == uid);
            ticket_entity.IsSold = true;

            _ticketRepository.Update(ticket_entity);
            var s = await _uow.SaveChangesAsync();

        }
        catch(Exception ex)
        {

        }
       



    }
}
