using IdentityService.DAL.MainDB.Context;
using IdentityService.DAL.MainDB.Repositories.Concretes;
using IdentityService.DAL.MainDB.Repositories.Interfaces;
using IdentityService.DAL.WorkerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.DAL;
public static class DalRegistration
{
    /*
     * 
     * 
     * */
    public static IServiceCollection AddDalServices(this IServiceCollection services, IConfiguration configuration)
    {

        // ======== Add Repository Service For DB1 ========
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAdminRepository, AdminRepository>();


        // ======== Add Hosted Services ========
        services.AddHostedService<InitDatabaseWorker>();


        // ======== Init DB1(MainDB) ========
        services.AddDbContext<MDbContext>((sp, options) =>
        {
            options.UseInMemoryDatabase(configuration["ConnectionStrings:MDbContext"]);
        }, ServiceLifetime.Scoped);



        return services;
    }
}
