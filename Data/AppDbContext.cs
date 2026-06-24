
// ============================================================
// FILE: Data/AppDbContext.cs
// ============================================================
namespace CitizenAPI.Data
{
    using CitizenAPI.Models;
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Citizen> Citizens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Citizen>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasIndex(x => x.NationalId).IsUnique();
                e.Property(x => x.NationalId).HasMaxLength(13).IsRequired();
                e.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
                e.Property(x => x.LastName).HasMaxLength(100).IsRequired();
                e.Property(x => x.Address).HasMaxLength(500).IsRequired();
            });
        }
    }
}