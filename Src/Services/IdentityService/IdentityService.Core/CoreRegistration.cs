// ================== <Start> Global Varaibles ==================
global using IdentityService.Core.DTO.Enums;
global using IdentityService.Core.DTO.Helpers;
global using IdentityService.Core.DTO.Options;
global using IdentityService.Core.DTO.Models;
global using IdentityService.Core.DTO.ResultType;
global using IdentityService.Core.Functions_Extensions;
// ================== </End> ==================

using IdentityService.Core.Security.Jwt;
using IdentityService.Core.Services.CacheService.MicrosoftInMemory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using IdentityService.Core.Services.CacheService.Redis;

namespace IdentityService.Core;
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
