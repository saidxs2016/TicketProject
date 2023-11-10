using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentService.Application.RequestsEventsHandlers.MessageEventsHandlers;
// ================== </End> ==================


namespace PaymentService.Application;

public static class ApplicationRegistration
{

    // bu Service ekleme işlemi: mediatr, auto mapper ve repsitorileri servis olarak sisteme dahil etme
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // ======== Add Mapper Service ========
        services.AddAutoMapper(typeof(ApplicationRegistration));

        // ======== Add Mediatr Service ========
        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssembly(typeof(ApplicationRegistration).Assembly);
        });

        // ======== MassTransit ========
        services.InjectMassTransit(configuration);



        return services;
    }

}
