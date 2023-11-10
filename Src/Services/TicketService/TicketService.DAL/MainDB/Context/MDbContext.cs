using TicketService.DAL.MainDB.Entities;
using Microsoft.EntityFrameworkCore;

namespace TicketService.DAL.MainDB.Context;

public partial class MDbContext : DbContext
{
    public MDbContext()
    {
    }

    public MDbContext(DbContextOptions<MDbContext> options)
        : base(options)
    {
    }


    public virtual DbSet<Ticket> Tickets { get; set; }


    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseNpgsql("Host=localhost;Database=main_db;Username=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("ticket_pkey");

            entity.ToTable("admin", tb => tb.HasComment("biletler tablosu"));

            entity.Property(e => e.Uid).ValueGeneratedNever();
        });       

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
