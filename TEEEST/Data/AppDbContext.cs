using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TEEEST.Models;

namespace TEEEST.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Purchase> Purchases { get; set; } = null!;
        public DbSet<Cashreg> CashRegisters { get; set; } = null!;
        public DbSet<EndOrEdit> EndOrEdits { get; set; }
        public DbSet<PurchaseRecord> PurchaseRecords { get; set; } // New PurchaseRecord DbSet
        public DbSet<Active> Actives { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Booking configuration
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.StartTimeUtc)
                    .HasColumnName("StartTime")
                    .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(b => b.BookingType)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(b => b.Price)
                    .HasColumnType("decimal(18,2)");
                entity.Property(b => b.Duration)
                    .HasConversion(
                        v => v.ToString(@"hh\:mm\:ss"),
                        v => TimeSpan.Parse(v));
            });

            // Product configuration
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Name);
                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(p => p.Price)
                    .HasColumnType("decimal(18,2)");

                // Seed data
                entity.HasData(
                    new Product { Name = "Standard", Price = 50.00m },
                    new Product { Name = "Premium", Price = 80.00m }
                );
            });

            // Purchase configuration
            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.ProductName)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(p => p.Price)
                    .HasColumnType("decimal(18,2)");
                entity.Property(p => p.FormattedDate)
                    .IsRequired()
                    .HasMaxLength(10); // Add max length for "dd.MM.yyyy"
                entity.Property(p => p.FormattedTime)
                    .IsRequired()
                    .HasMaxLength(8);  // Add max length for "HH:mm:ss"
            });

            // Cash register configuration
            modelBuilder.Entity<Cashreg>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Cash)
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0m);
                entity.Property(c => c.Card)
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0m);
                entity.Property(c => c.Total)
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0m);

                // Seed data
                entity.HasData(
                    new Cashreg { Id = 1, Cash = 0, Card = 0, Total = 0 }
                );
            });

            // PurchaseRecord configuration
            modelBuilder.Entity<PurchaseRecord>(entity =>
            {
                entity.HasKey(pr => pr.Id);  // Assuming you have an Id property in PurchaseRecord
                entity.Property(pr => pr.Date)
                    .IsRequired();
                entity.Property(pr => pr.Type)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(pr => pr.Duration)
                    .IsRequired()
                    .HasConversion(
                        v => v.ToString(@"hh\:mm\:ss"),
                        v => TimeSpan.Parse(v));  // Assuming Duration is of type TimeSpan
                entity.Property(pr => pr.Price)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)"); // Price as decimal
                entity.Property(pr => pr.ItemsPurchased)
                    .IsRequired();  // Assuming ItemsPurchased is an integer

                // Optional: Add seed data if needed
                // entity.HasData(new PurchaseRecord { Id = 1, Date = DateTime.Now, Type = "Online", Duration = new TimeSpan(1, 30, 0), Price = 99.99m, ItemsPurchased = 5 });
            });
           

        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var now = DateTime.UtcNow;
            var georgianNow = Booking.ToGeorgianTime(now);
            var dateStr = georgianNow.ToString("dd.MM.yyyy");
            var timeStr = georgianNow.ToString("HH:mm:ss");

            foreach (var entry in ChangeTracker.Entries<Purchase>()
                .Where(e => e.State == EntityState.Added))
            {
                entry.Entity.FormattedDate = dateStr;
                entry.Entity.FormattedTime = timeStr;
            }
        }
    }
}
