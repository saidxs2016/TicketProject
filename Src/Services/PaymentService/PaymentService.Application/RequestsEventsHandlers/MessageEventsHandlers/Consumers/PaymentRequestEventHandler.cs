using Contarcts;
using Contracts;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace PaymentService.Application.RequestsEventsHandlers.MessageEventsHandlers.Consumers;



public class PaymentRequestEventHandler : IConsumer<PaymentRequestEvent>
{

    private readonly IServiceScopeFactory _serviceScopeFactory;
    public PaymentRequestEventHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Consume(ConsumeContext<PaymentRequestEvent> context)
    {
        // Not: eğer işlem yapılırken bu servisin diğer kaynakları kullanılması gerekiyor ise
        //      yeni scope oluşturup yapılması gerekiyor yoksa 
        //      diğer kaynakları kullanan başka nesneler ile çakışma yaşanacak
        //      eğer bu service'in kaynakları kullanılması söz konusu değil ise normal dependency injection kullanılıabilir.

        // =============== Create New Scope ===============        
        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        // =============== Get Your Services ===============
        var _bus = scope.ServiceProvider.GetService<IBus>();

        // =============== Write Your Code ===============

        bool paymenState = false;
        // ödeme işemleri yapıldı....
        paymenState = true; // true | false dönecek
        if (context.Message.Price < 1 || context.Message.IsSold == true)
        {
            await _bus.Publish(new PaymentFailedEvent { Uid = context.Message.Uid, Message = "Ödeme tamamlanmadı, bilet bilgileri yanlış." });
            return;
        }

        if (!paymenState)
            await _bus.Publish(new PaymentFailedEvent { Uid = context.Message.Uid, Message = "Ödeme tamamlanmadı, başka zaman deneyiniz." });
   
        
        else
            await _bus.Publish(new PaymentSuccessEvent { Uid = context.Message.Uid, Message = "Ödeme tamamlandı" });

    }
}
