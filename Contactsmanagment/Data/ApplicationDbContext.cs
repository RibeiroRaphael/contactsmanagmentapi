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
                entity.HasOne(c => c.Region).WithMany(r => r.Contacts).HasForeignKey(c => c.RegionId).OnDelete(DeleteBehavior.Restrict);
            });
            
        }
    }
}
