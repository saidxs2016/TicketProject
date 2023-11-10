using TicketService.DAL.MainDB.Context;
using TicketService.DAL.MainDB.Entities;
using TicketService.DAL.MainDB.Repositories.Interfaces;

namespace TicketService.DAL.MainDB.Repositories.Concretes;

public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
{
    //private readonly MDbContext _db;

    public TicketRepository(MDbContext db) : base(db)
    {
        //_db = db;
    }



}
