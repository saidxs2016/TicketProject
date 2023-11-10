using IdentityService.DAL.MainDB.Entities;
using IdentityService.DAL.MainDB.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IdentityService.DAL.WorkerServices;

public class InitDatabaseWorker : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<InitDatabaseWorker> _logger;
    public InitDatabaseWorker(IServiceScopeFactory serviceScopeFactory, ILogger<InitDatabaseWorker> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken token)
    {
        try
        {
            // bu worker ile eğer code first yaklaşımı kullanılmakta ise update edilmeyen yada update yapmadan
            // migrationlari host üzerindeki yada belirtilen connection stringe göre tabloalrı oluşturmakta. 
            token.ThrowIfCancellationRequested();
            Task.Run(async () => await DoWork(token), token);
        }
        catch (Exception) { }

        return Task.CompletedTask;

    }

    private async Task DoWork(CancellationToken token)
    {
        try
        {
            await using var scope = _serviceScopeFactory.CreateAsyncScope();
            var _uow = scope.ServiceProvider.GetService<IUnitOfWork>();
            var db = _uow.DbContext;
            //var arr = await db.Database.GetPendingMigrationsAsync(token);
            //if (arr.Any())
            //    await db.Database.MigrateAsync(token);


            if (!db.Admins.Any())
            {
                await db.Admins.AddAsync(new Admin
                {
                    Uid = Guid.NewGuid(),
                    Name = "Said",
                    Surname = "Yunus",
                    Email = "saidxs2016@gmail.com",
                    Phone = "00000000000",
                    Username = "admin",
                    Password = "123"
                }, cancellationToken: token);
                await _uow.SaveChangesAsync(token);
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }


        //return Task.CompletedTask;

    }
}
