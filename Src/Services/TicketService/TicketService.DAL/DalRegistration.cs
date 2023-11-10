using TicketService.DAL.MainDB.Context;
using TicketService.DAL.MainDB.Repositories.Concretes;
using TicketService.DAL.MainDB.Repositories.Interfaces;
using TicketService.DAL.WorkerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TicketService.DAL;
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
        services.AddScoped<ITicketRepository, TicketRepository>();


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
