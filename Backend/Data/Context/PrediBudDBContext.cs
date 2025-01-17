using Backend.Data.Models.Notifications;
using Backend.Data.Models.Suppliers;
using Backend.Data.Models;
using Microsoft.EntityFrameworkCore;
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
using Backend.Data.Models.Orders.Construction;
using Backend.Data.Models.Orders.Material;
using Backend.Data.Models.MaterialPrices.Balcony;
using Backend.Data.Models.MaterialPrices.Celling;
using Backend.Data.Models.MaterialPrices.Doors;
using Backend.Data.Models.MaterialPrices.Facade;
using Backend.Data.Models.MaterialPrices.Floor;
using Backend.Data.Models.MaterialPrices.Insulation;
using Backend.Data.Models.MaterialPrices.Painting;
using Backend.Data.Models.MaterialPrices.Plastering;
using Backend.Data.Models.MaterialPrices.ShellOpen;
using Backend.Data.Models.MaterialPrices.Stairs;
using Backend.Data.Models.Constructions.Specyfication.Walls;
using Backend.Data.Models.Constructions.Specyfication.Chimney;
using Backend.Data.Models.Constructions.Specyfication.Ventilation;
using Backend.Data.Models.Constructions.Specyfication.Foundation;
using Backend.Data.Models.Constructions.Specyfication.Roof;
using Backend.Data.Models.Constructions.Specyfication.Windows;
using Backend.Data.Models.MaterialPrices.Windows;

namespace Backend.Data.Context
{
    public class PrediBudDBContext : DbContext
    {
        public PrediBudDBContext(DbContextOptions<PrediBudDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<MaterialNotification> MaterialNotifications { get; set; }
        public DbSet<ConstructionOrderNotification> ConstructionOrderNotifications { get; set; }
        public DbSet<ConstructionOrder> ConstructionOrders { get; set; }
        public DbSet<MaterialOrder> MaterialOrders { get; set; } // Dodano MaterialOrder
        public DbSet<MaterialPrice> MaterialPrices { get; set; } // Dodano MaterialPrice

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

            modelBuilder.Entity<MaterialOrder>()
                .HasOne(mo => mo.User)
                .WithMany(u => u.MaterialOrders)
                .HasForeignKey(mo => mo.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MaterialOrder>()
                .HasOne<Supplier>() 
                .WithMany(s => s.MaterialOrders) 
                .HasForeignKey(mo => mo.SupplierId) 
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<MaterialOrder>()
                 .HasOne(mo => mo.MaterialPrice)
                 .WithMany() 
                 .HasForeignKey(mo => mo.MaterialPriceId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MaterialPrice>()
                .HasOne(mp => mp.Supplier)
                .WithMany(s => s.MaterialPrices)
                .HasForeignKey(mp => mp.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);

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
                .HasValue<WindowsSpecification>(ConstructionType.Windows)
                .HasValue<ChimneySpecification>(ConstructionType.Chimney) 
                .HasValue<CeilingSpecification>(ConstructionType.Ceiling)
                .HasValue<PartitionWallSpecification>(ConstructionType.PartitionWall) 
                .HasValue<FoundationSpecification>(ConstructionType.Foundation) 
                .HasValue<LoadBearingWallSpecification>(ConstructionType.LoadBearingWall) 
                .HasValue<VentilationSystemSpecification>(ConstructionType.VentilationSystem)
                .HasValue<RoofSpecification>(ConstructionType.Roof);

            modelBuilder.Entity<MaterialPrice>()
                .ToTable("MaterialPrices")
                .HasDiscriminator<string>("MaterialPriceType")
                .HasValue<BalconyMaterialPrice>("Balcony")
                .HasValue<SuspendedCeilingMaterialPrice>("SuspendedCeiling")
                .HasValue<DoorsMaterialPrice>("Doors")
                .HasValue<FacadeMaterialPrice>("Facade")
                .HasValue<FlooringMaterialPrice>("Flooring")
                .HasValue<InsulationOfAtticMaterialPrice>("InsulationOfAttic")
                .HasValue<PaintingMaterialPrice>("Painting")
                .HasValue<PlasteringMaterialPrice>("Plastering")
                .HasValue<StaircaseMaterialPrice>("Staircase")
                .HasValue<RoofMaterialPrice>("Roof")
                .HasValue<CeilingMaterialPrice>("Ceiling")
                .HasValue<ChimneyMaterialPrice>("Chimney")
                .HasValue<PartitionWallMaterialPrice>("PartitionWall")
                .HasValue<LoadBearingWallMaterialPrice>("LoadBearingWall")
                .HasValue<FoundationMaterialPrice>("Foundation")
                .HasValue<VentilationSystemMaterialPrice>("VentilationSystem")
                .HasValue<StaircaseMaterialPrice>("Staircase")
                .HasValue<WindowsMaterialPrice>("Windows");

            base.OnModelCreating(modelBuilder);
        }
    }

}