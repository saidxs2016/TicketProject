using IdentityService.DAL.MainDB.Context;
using IdentityService.DAL.MainDB.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace IdentityService.DAL.MainDB.Repositories.Concretes;

// Base Repository İçinde Bir Değişiklik Yapmak Yasaktır. (SAİD'e Sorunuz.)
public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly MDbContext _db;
    public BaseRepository(MDbContext db)
    {
        _db = db;
    }


    // ================ Command ================
    public virtual T Add(T entity)
    {
        EntityEntry entry = _db.Set<T>().Add(entity);
        return entry.State == EntityState.Added ? entity : null;
    }
    public virtual async ValueTask<T> AddAsync(T entity, CancellationToken token = default)
    {
        EntityEntry entry = await _db.Set<T>().AddAsync(entity, cancellationToken: token);
        return entry.State == EntityState.Added ? entity : null;
    }
    public virtual void AddRange(List<T> entities) => _db.Set<T>().AddRange(entities);
    public virtual async Task AddRangeAsync(List<T> entities, CancellationToken token = default) =>
        await _db.Set<T>().AddRangeAsync(entities, cancellationToken: token);
    public virtual T Update(T entity)
    {
        EntityEntry entry = _db.Set<T>().Update(entity);
        return entry.State == EntityState.Modified ? entity : null;
    }

    public virtual void UpdateRange(List<T> entities) => _db.Set<T>().UpdateRange(entities);
    public virtual T Delete(T entity)
    {
        EntityEntry entry = _db.Set<T>().Remove(entity);

        return entry.State == EntityState.Deleted ? entity : null;
    }
    public virtual void DeleteRange(List<T> entities) => _db.Set<T>().RemoveRange(entities);


    /// <summary>
    /// .NET7 ile gelen yeni update metodu.
    ///  DİKKAT::: bu metot savechange gerektirmeden işlemi veritabana yansıtıyor. 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="props"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public virtual async ValueTask<int> ExecuteUpdateAsync(Expression<Func<T, bool>> predicate, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> props, CancellationToken token = default) =>
        await _db.Set<T>().Where(predicate).ExecuteUpdateAsync(props, cancellationToken: token);

    /// <summary>
    /// .NET7 ile gelen yeni update metodu.
    ///  DİKKAT::: bu metot savechange gerektirmeden işlemi veritabana yansıtıyor. 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public virtual async ValueTask<int> ExecuteDeleteAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default) => await _db.Set<T>().Where(predicate).ExecuteDeleteAsync(cancellationToken: token);



    // ================ Query ================
    public Guid GetInstanceId() => _db.ContextId.InstanceId;
    public virtual DbSet<T> Entity() => _db.Set<T>();
    public virtual IQueryable<T> AsQueryable() => _db.Set<T>().AsQueryable();
    public virtual IQueryable<T> AsQueryable(Expression<Func<T, bool>> predicate) => _db.Set<T>().Where(predicate);

    public virtual List<T> GetAll() => _db.Set<T>().ToList();
    public virtual async Task<List<T>> GetAllAsync(CancellationToken token = default) => await _db.Set<T>().ToListAsync(cancellationToken: token);
    public virtual List<T> GetAsWhere(Expression<Func<T, bool>> predicate) => _db.Set<T>().Where(predicate).ToList();
    public virtual async Task<List<T>> GetAsWhereAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default) => await _db.Set<T>().Where(predicate).ToListAsync(cancellationToken: token);
    public virtual T GetAsFirstOrDefault(Expression<Func<T, bool>> predicate) => _db.Set<T>().FirstOrDefault(predicate);
    public virtual async Task<T> GetAsFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default) => await _db.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken: token);
    public virtual int GetCount(Expression<Func<T, bool>> predicate) => _db.Set<T>().Count(predicate);
    public virtual async Task<int> GetCountAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default) => await _db.Set<T>().CountAsync(predicate, cancellationToken: token);
    public virtual bool Exist(Expression<Func<T, bool>> predicate) => _db.Set<T>().Any(predicate);
    public virtual async Task<bool> ExistAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default) => await _db.Set<T>().AnyAsync(predicate, cancellationToken: token);


}
