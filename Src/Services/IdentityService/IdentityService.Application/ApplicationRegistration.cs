// ================== <Start> Global Varaibles ==================
global using IdentityService.Core.DTO.Enums;
global using IdentityService.Core.DTO.Helpers;
using IdentityService.Application.RequestsEventsHandlers.MediatrBehaviors;
using IdentityService.Application.RequestsEventsHandlers.MessageEventsHandlers;
// ================== </End> ==================

using IdentityService.Core;
using IdentityService.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Application;

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
            conf.AddOpenBehavior(typeof(MedPipelineFilter<,>));
            //conf.AddOpenRequestPreProcessor(typeof(RequestFilter<>));
            //conf.AddOpenRequestPostProcessor(typeof(ResponseFilter<,>));
        });

        // ======== MassTransit ========
        services.InjectMassTransit(configuration);




        services.AddDalServices(configuration);
        services.AddCoreServices(configuration);
        return services;
    }
    
}
