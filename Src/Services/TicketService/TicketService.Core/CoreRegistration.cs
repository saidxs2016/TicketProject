// ================== <Start> Global Varaibles ==================
global using TicketService.Core.DTO.Enums;
global using TicketService.Core.DTO.Helpers;
global using TicketService.Core.DTO.Options;
global using TicketService.Core.DTO.Models;
global using TicketService.Core.DTO.ResultType;
global using TicketService.Core.Functions_Extensions;
// ================== </End> ==================

using TicketService.Core.Security.Jwt;
using TicketService.Core.Services.CacheService.MicrosoftInMemory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using TicketService.Core.Services.CacheService.Redis;

namespace TicketService.Core;
public static class CoreRegistration
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
    { 
        // ======== Cache Microsoft Service ========
        services.AddMemoryCache();
        services.AddSingleton<IMemoryCacheService, MemoryCacheService>();

        // ======== Cache Redis Service ========
        //var multiplexer = ConnectionMultiplexer.Connect(configuration["ConnectionStrings:Redis"]);
        //services.AddSingleton<IConnectionMultiplexer>(multiplexer);
        //services.AddSingleton<IRedisCacheService, RedisCacheService>();

        // ======== JWT Helper Service ========
        services.Configure<JwtSetting>(configuration.GetSection("Jwt"));
        services.AddScoped<IJwtHelper, JwtHelper>();



        return services;
    }

    
}
