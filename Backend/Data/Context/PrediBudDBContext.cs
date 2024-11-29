using Backend.Data.Models.Notifications;
using Backend.Data.Models.Suppliers;
using Backend.Data.Models;
using Microsoft.EntityFrameworkCore;
using Backend.Data.Models.Orders;
using Backend.Data.Models.Users;
using Backend.Data.Models.Constructions.Dimensions.Balcony;
using Backend.Data.Models.Constructions.Dimensions.Doors;
using Backend.Data.Models.Constructions.Dimensions.Facade;
using Backend.Data.Models.Constructions.Dimensions.Floor;
using Backend.Data.Models.Constructions.Dimensions;
using Backend.Data.Models.Constructions.Specyfication.Ceiling;
using Backend.Data.Models.Constructions.Specyfication.Painting;
using Backend.Data.Models.Constructions.Specyfication.Plastering;
using Backend.Data.Models.Constructions.Specyfication.ShellOpen;
using Backend.Data.Models.Constructions.Specyfication.Stairs;
using Backend.Data.Models.Constructions.Specyfication;
using Backend.Data.Models.Constructions;

namespace Backend.Data.Context
{
    public class PrediBudDBContext : DbContext
    {
        public PrediBudDBContext(DbContextOptions<PrediBudDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<MaterialOrder> MaterialOrders { get; set; }
        public DbSet<Calculation> Calculations { get; set; }
        public DbSet<MaterialNotification> MaterialNotifications { get; set; }
        public DbSet<ConstructionOrderNotification> ConstructionOrderNotifications { get; set; }
        public DbSet<ConstructionOrder> ConstructionOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Client>("Client")
                .HasValue<Worker>("Worker");

            modelBuilder.Entity<User>()
                .HasOne(u => u.Address)
                .WithOne()
                .HasForeignKey<User>(u => u.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .OwnsOne(u => u.Credentials);

            modelBuilder.Entity<Client>()
                .OwnsOne(c => c.ContactDetails);
            modelBuilder.Entity<Client>()
                .OwnsOne(c => c.Credentials);
            modelBuilder.Entity<Worker>()
                .OwnsOne(w => w.ContactDetails);
            modelBuilder.Entity<Worker>()
                .OwnsOne(w => w.Credentials);

            modelBuilder.Entity<ConstructionOrder>()
                .HasOne(co => co.Client)
                .WithMany(c => c.ConstructionOrders)
                .HasForeignKey(co => co.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ConstructionOrder>()
                .HasOne(co => co.ConstructionSpecification)
                .WithOne()
                .HasForeignKey<ConstructionOrder>(co => co.ConstructionSpecificationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ConstructionOrder>()
               .HasOne(co => co.Worker)
               .WithMany(w => w.AssignedOrders)
               .HasForeignKey(co => co.WorkerId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConstructionOrderNotification>()
                .HasOne(n => n.Client)
                .WithMany(c => c.ConstructionOrderNotifications)
                .HasForeignKey(n => n.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConstructionOrderNotification>()
                 .HasOne(n => n.Worker)
                 .WithMany(w => w.ConstructionOrderNotifications)
                 .HasForeignKey(n => n.WorkerId)
                 .OnDelete(DeleteBehavior.Restrict);

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

            //specifications
            modelBuilder.Entity<ConstructionSpecification>()
                 .ToTable("ConstructionSpecifications")
                 .HasDiscriminator<ConstructionType>("Type")
                 .HasValue<BalconySpecification>(ConstructionType.Balcony)
                 .HasValue<SuspendedCeilingSpecification>(ConstructionType.SuspendedCeiling)
                 .HasValue<DoorsSpecification>(ConstructionType.Doors)
                 .HasValue<FacadeSpecification>(ConstructionType.Facade)
                 .HasValue<FlooringSpecification>(ConstructionType.Flooring)
                 .HasValue<InsulationOfAtticSpecification>(ConstructionType.InsulationOfAttic)
                 .HasValue<PaintingSpecification>(ConstructionType.Painting)
                 .HasValue<PlasteringSpecification>(ConstructionType.Plastering)
                 .HasValue<ShellOpenSpecification>(ConstructionType.ShellOpen)
                 .HasValue<StaircaseSpecification>(ConstructionType.Staircase)
                 .HasValue<WindowsSpecification>(ConstructionType.Windows);

            base.OnModelCreating(modelBuilder);
        }

    }
}