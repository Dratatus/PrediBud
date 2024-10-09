using Backend.Data.Models.Notifications;
using Backend.Data.Models.Suppliers;
using Backend.Data.Models.Workers;
using Backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data.Context
{
    public class PrediBudDBContext : DbContext
    {
        public PrediBudDBContext(DbContextOptions<PrediBudDBContext> options) : base(options) { }

        public DbSet<Material> Materials { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<MaterialOrder> MaterialOrders { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Calculation> Calculations { get; set; }
        public DbSet<MaterialNotification> MaterialNotifications { get; set; }
        public DbSet<ConstructionOrderNotification> ConstructionOrderNotifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Material>()
                .Property(m => m.PriceWithTaxes)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Material>()
                .Property(m => m.PriceWithoutTaxes)
                .HasPrecision(18, 2);

            modelBuilder.Entity<MaterialOrder>()
                .Property(o => o.TotalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Calculation>()
                .Property(c => c.Taxes)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Calculation>()
                .Property(c => c.UserPrice)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }

    }
}