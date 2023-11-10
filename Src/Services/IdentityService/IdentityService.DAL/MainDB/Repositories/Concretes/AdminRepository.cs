using IdentityService.DAL.MainDB.Context;
using IdentityService.DAL.MainDB.Entities;
using IdentityService.DAL.MainDB.Repositories.Interfaces;

namespace IdentityService.DAL.MainDB.Repositories.Concretes;

public class AdminRepository : BaseRepository<Admin>, IAdminRepository
{
    //private readonly MDbContext _db;

    public AdminRepository(MDbContext db) : base(db)
    {
        //_db = db;
    }



}
