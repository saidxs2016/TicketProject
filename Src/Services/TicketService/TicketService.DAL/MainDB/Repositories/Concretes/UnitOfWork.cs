using TicketService.DAL.MainDB.Context;
using TicketService.DAL.MainDB.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace TicketService.DAL.MainDB.Repositories.Concretes;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly MDbContext _db;
    private IDbContextTransaction transaction = null;

    public UnitOfWork(MDbContext db, ILogger<UnitOfWork> logger)
    {
        _db = db;
    }

    public Guid GetInstanceId() => _db.ContextId.InstanceId;
    public virtual MDbContext DbContext => _db;

    public int SaveChange() => _db.SaveChanges();
    public async Task<int> SaveChangesAsync(CancellationToken token = default) => await _db.SaveChangesAsync(cancellationToken: token);

    // ================ Transaction ================    
    public virtual void BeginTransaction()
    {
        transaction = _db.Database.BeginTransaction();
    }
    public virtual async Task BeginTransactionAsync(CancellationToken token = default)
    {
        transaction = await _db.Database.BeginTransactionAsync(cancellationToken: token);
    }
    public virtual void CommitTransaction() => transaction.Commit();
    public virtual async Task CommitTransactionAsync(CancellationToken token = default) => await transaction.CommitAsync(cancellationToken: token);
    public virtual void RollbackTransaction() => transaction.Rollback();
    public virtual async Task RollbackTransactionAsync(CancellationToken token = default) => await transaction.RollbackAsync(cancellationToken: token);
    public virtual void DisposeTransaction() => transaction.Dispose();
    public virtual async Task DisposeTransactionAsync(CancellationToken token = default) => await transaction.DisposeAsync();




    // ================ Disposable ================
    public void Dispose() => transaction?.Dispose();


}
