using Contactsmanagment.Models;
using Microsoft.EntityFrameworkCore;

namespace Contactsmanagment.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts => Set<Contact>();
        public DbSet<Region> Regions => Set<Region>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Region>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Ddd).IsRequired();
                entity.Property(r => r.Name).IsRequired().HasMaxLength(100);
                entity.HasIndex(r => r.Ddd).IsUnique();
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(150);
                entity.Property(c => c.Email).IsRequired().HasMaxLength(150);
                entity.Property(c => c.Phone).IsRequired().HasMaxLength(20);

                entity.HasOne(c => c.Region)
                      .WithMany(r => r.Contacts)
                      .HasForeignKey(c => c.RegionId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // =============================
            // SEED DATA - REGIONS
            // =============================
            modelBuilder.Entity<Region>().HasData(
                new Region
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Ddd = 11,
                    Name = "São Paulo",
                    IsActive = true
                },
                new Region
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Ddd = 21,
                    Name = "Rio de Janeiro",
                    IsActive = true
                },
                new Region
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Ddd = 31,
                    Name = "Minas Gerais",
                    IsActive = true
                },
                new Region
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Ddd = 35,
                    Name = "Sul de Minas",
                    IsActive = true
                },
                new Region
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Ddd = 41,
                    Name = "Paraná",
                    IsActive = true
                }
            );
        }
    }
}