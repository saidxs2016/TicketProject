using IdentityService.DAL.MainDB.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DAL.MainDB.Context;

public partial class MDbContext : DbContext
{
    public MDbContext()
    {
    }

    public MDbContext(DbContextOptions<MDbContext> options)
        : base(options)
    {
    }


    public virtual DbSet<Admin> Admins { get; set; }


    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseNpgsql("Host=localhost;Database=main_db;Username=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("admin_pkey");

            entity.ToTable("admin", tb => tb.HasComment("adminler tablosu"));

            entity.Property(e => e.Uid).ValueGeneratedNever();
        });       

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
