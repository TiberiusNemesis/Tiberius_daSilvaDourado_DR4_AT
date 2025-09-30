using Microsoft.EntityFrameworkCore;
using TravelAgency.Models;

namespace TravelAgency.Data
{
    public class TravelAgencyContext : DbContext
    {
        public TravelAgencyContext(DbContextOptions<TravelAgencyContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<TourPackage> TourPackages { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Destination>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Country).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<TourPackage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                entity.Property(e => e.StartDate).IsRequired();
                entity.Property(e => e.MaxCapacity).IsRequired();
                entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(18,2)");

                entity.HasMany(p => p.Destinations)
                      .WithMany(d => d.TourPackages)
                      .UsingEntity(j => j.ToTable("TourPackageDestination"));

                entity.HasMany(p => p.Bookings)
                      .WithOne(r => r.TourPackage)
                      .HasForeignKey(r => r.TourPackageId);
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CustomerId).IsRequired();
                entity.Property(e => e.TourPackageId).IsRequired();
                entity.Property(e => e.BookingDate).IsRequired();
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);

                entity.HasOne(r => r.Customer)
                      .WithMany(c => c.Bookings)
                      .HasForeignKey(r => r.CustomerId);

                entity.HasOne(r => r.TourPackage)
                      .WithMany(p => p.Bookings)
                      .HasForeignKey(r => r.TourPackageId);

                entity.HasQueryFilter(r => !r.IsDeleted);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}