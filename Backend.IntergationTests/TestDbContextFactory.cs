using Backend.Data.Context;
using Backend.Data.Models.Common;
using Backend.Data.Models.Constructions;
using Backend.Data.Models.Constructions.Specyfication;
using Backend.Data.Models.Constructions.Specyfication.Windows;
using Backend.Data.Models.Credidentials;
using Backend.Data.Models.Orders;
using Backend.Data.Models.Orders.Construction;
using Backend.Data.Models.Orders.Material;
using Backend.Data.Models.Suppliers;
using Backend.Data.Models.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Backend.IntergationTests
{
    public class TestDbContextFactory : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<PrediBudDBContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<PrediBudDBContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");
                });
            });

            var host = base.CreateHost(builder);

            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<PrediBudDBContext>();
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                SeedTestData(db);
            }

            return host;
        }

        private void SeedTestData(PrediBudDBContext db)
        {
            var clientAddress = new Address
            {
                ID = 1,
                City = "Test City",
                StreetName = "Test Street 1",
                PostCode = "12345"
            };

            var workerAddress = new Address
            {
                ID = 2,
                City = "Worker City",
                StreetName = "Worker Street 1",
                PostCode = "67890"
            };

            var supplier1Address = new Address
            {
                ID = 3,
                City = "Supplier City 1",
                StreetName = "Supplier Street 1",
                PostCode = "55555"
            };

            var supplier2Address = new Address
            {
                ID = 4,
                City = "Supplier City 2",
                StreetName = "Supplier Street 2",
                PostCode = "66666"
            };

            db.Set<Address>().Add(clientAddress);
            db.Set<Address>().Add(workerAddress);
            db.Set<Address>().Add(supplier1Address);
            db.Set<Address>().Add(supplier2Address);

            var client = new Client
            {
                ID = 21,
                ContactDetails = new ContactDetails { Name = "Client 1", Phone = "111-222-333" },
                AddressId = clientAddress.ID,
                Address = clientAddress,
                Credentials = new Credentials { Email = "client1@example.com", PasswordHash = "hash1" }
            };

            var worker1 = new Worker
            {
                ID = 20,
                Position = "Worker 1",
                ContactDetails = new ContactDetails { Name = "Worker 1", Phone = "444-555-666" },
                AddressId = workerAddress.ID,
                Address = workerAddress,
                Credentials = new Credentials { Email = "worker1@example.com", PasswordHash = "hash2" }
            };

            db.Set<Client>().Add(client);
            db.Set<Worker>().Add(worker1);


            var client1 = new Client
            {
                ID = 1,
            };
            db.Set<Client>().Add(client1);

            var client2 = new Client
            {
                ID = 2,
            };
            db.Set<Client>().Add(client2);

            var supplier1 = new Supplier
            {
                ID = 1,
                Name = "Supplier 1",
                ContactEmail = "supplier1@example.com",
                AddressId = supplier1Address.ID,
                Address = supplier1Address
            };

            var supplier2 = new Supplier
            {
                ID = 2,
                Name = "Supplier 2",
                ContactEmail = "supplier2@example.com",
                AddressId = supplier2Address.ID,
                Address = supplier2Address
            };

            db.Set<Supplier>().Add(supplier1);
            db.Set<Supplier>().Add(supplier2);

            var windowsSpec = new WindowsSpecification
            {
                ID = 1,
                Type = ConstructionType.Windows
            };
            db.Set<ConstructionSpecification>().Add(windowsSpec);

            var order9 = new ConstructionOrder
            {
                ID = 9,
                ClientId = 1,
                Description = "Instalacja testowa #9",
                ConstructionSpecificationId = windowsSpec.ID
            };
            db.Set<ConstructionOrder>().Add(order9);

            var order10 = new ConstructionOrder
            {
                ID = 10,
                ClientId = 1,
                Description = "Instalacja testowa #10",
                ConstructionSpecificationId = windowsSpec.ID
            };
            db.Set<ConstructionOrder>().Add(order10);

            var order11 = new ConstructionOrder
            {
                ID = 26,
                ClientId = client.ID,
                Client = client,
                WorkerId = worker1.ID,
                Worker = worker1,
                Description = "Instalacja testowa #9",
                Status = OrderStatus.New,
                ConstructionType = ConstructionType.Windows,
                ConstructionSpecificationId = windowsSpec.ID,
                ConstructionSpecification = windowsSpec
            };

            var order12 = new ConstructionOrder
            {
                ID = 25,
                ClientId = client.ID,
                Client = client,
                WorkerId = worker1.ID,
                Worker = worker1,
                Description = "Instalacja testowa #10",
                Status = OrderStatus.Accepted,
                ConstructionType = ConstructionType.Windows,
                ConstructionSpecificationId = windowsSpec.ID,
                ConstructionSpecification = windowsSpec
            };

            db.Set<ConstructionOrder>().Add(order11);
            db.Set<ConstructionOrder>().Add(order12);

            var order27 = new ConstructionOrder
            {
                ID = 27,
                ClientId = 21,
                Client = client,
                WorkerId = worker1.ID, 
                Worker = worker1,
                Description = "Negotiation test #27 (New)",
                Status = OrderStatus.New,
                ConstructionType = ConstructionType.Windows,
                ConstructionSpecificationId = windowsSpec.ID,
                ConstructionSpecification = windowsSpec
            };
            db.Set<ConstructionOrder>().Add(order27);

            var order28 = new ConstructionOrder
            {
                ID = 28,
                ClientId = 21,
                Client = client,
                WorkerId = worker1.ID,
                Worker = worker1,
                Description = "Negotiation test #28 (NegotiationInProgress)",
                Status = OrderStatus.NegotiationInProgress,
                ConstructionType = ConstructionType.Windows,
                ConstructionSpecificationId = windowsSpec.ID,
                ConstructionSpecification = windowsSpec,
                WorkerProposedPrice = 1500m
            };
            db.Set<ConstructionOrder>().Add(order28);

            var order29 = new ConstructionOrder
            {
                ID = 29,
                ClientId = 21,
                Client = client,
                WorkerId = worker1.ID,
                Worker = worker1,
                Description = "Negotiation test #29 (Accepted)",
                Status = OrderStatus.Accepted,
                ConstructionType = ConstructionType.Windows,
                ConstructionSpecificationId = windowsSpec.ID,
                ConstructionSpecification = windowsSpec,
                AgreedPrice = 2000m
            };
            db.Set<ConstructionOrder>().Add(order29);

            var order71 = new ConstructionOrder
            {
                ID = 71,
                ClientId = 21,   
                WorkerId = 20,    
                Description = "Negotiation test #71 (New)",
                Status = OrderStatus.New,
                ConstructionType = ConstructionType.Windows,
                ConstructionSpecificationId = windowsSpec.ID,
                ConstructionSpecification = windowsSpec
            };
            db.Set<ConstructionOrder>().Add(order71);

            var order72 = new ConstructionOrder
            {
                ID = 72,
                ClientId = 21,
                WorkerId = 20,
                Description = "Negotiation test #72 (NegotiationInProgress)",
                Status = OrderStatus.NegotiationInProgress,
                ConstructionType = ConstructionType.Windows,
                ConstructionSpecificationId = windowsSpec.ID,
                LastActionBy = LastActionBy.Worker,
                ConstructionSpecification = windowsSpec,
                WorkerProposedPrice = 1500m
            };
            db.Set<ConstructionOrder>().Add(order72);

            var order73 = new ConstructionOrder
            {
                ID = 73,
                ClientId = 21,
                WorkerId = 20,
                Description = "Negotiation test #73 (Accepted)",
                Status = OrderStatus.Accepted,
                ConstructionType = ConstructionType.Windows,
                ConstructionSpecificationId = windowsSpec.ID,
                ConstructionSpecification = windowsSpec,
                AgreedPrice = 2000m
            };
            db.Set<ConstructionOrder>().Add(order73);

            var order74 = new ConstructionOrder
            {
                ID = 74,
                ClientId = 21,
                WorkerId = 20,
                Description = "Negotiation test #74 (Already in Negotiation)",
                Status = OrderStatus.NegotiationInProgress, 
                ConstructionType = ConstructionType.Windows,
                ConstructionSpecificationId = windowsSpec.ID,
                ConstructionSpecification = windowsSpec,
                WorkerProposedPrice = 999m
            };
            db.Set<ConstructionOrder>().Add(order74);

            var order75 = new ConstructionOrder
            {
                ID = 75,
                ClientId = 21,
                WorkerId = 20,
                Description = "Negotiation test #75 (Not Accepted, e.g. New)",
                Status = OrderStatus.New,
                ConstructionType = ConstructionType.Windows,
                ConstructionSpecificationId = windowsSpec.ID,
                ConstructionSpecification = windowsSpec
            };
            db.Set<ConstructionOrder>().Add(order75);

            var order76 = new ConstructionOrder
            {
                ID = 76,
                ClientId = 21,
                WorkerId = 20,
                Description = "Negotiation test #72 (NegotiationInProgress)",
                Status = OrderStatus.NegotiationInProgress,
                ConstructionType = ConstructionType.Windows,
                ConstructionSpecificationId = windowsSpec.ID,
                LastActionBy = LastActionBy.Worker,
                ConstructionSpecification = windowsSpec,
                WorkerProposedPrice = 1500m
            };
            db.Set<ConstructionOrder>().Add(order76);      
            
            var order77 = new ConstructionOrder
            {
                ID = 77,
                ClientId = 21,
                WorkerId = 20,
                Description = "Negotiation test #72 (NegotiationInProgress)",
                Status = OrderStatus.NegotiationInProgress,
                ConstructionType = ConstructionType.Windows,
                ConstructionSpecificationId = windowsSpec.ID,
                LastActionBy = LastActionBy.Client,
                ConstructionSpecification = windowsSpec,
                WorkerProposedPrice = 1500m
            };
            db.Set<ConstructionOrder>().Add(order77);

            var materialPrice1 = new MaterialPrice
            {
                ID = 1,
                MaterialType = MaterialType.Wood,
                MaterialCategory = ConstructionType.PartitionWall,
                PriceWithoutTax = 50.0m,
                SupplierId = supplier1.ID,
                Supplier = supplier1
            };

            var materialPrice2 = new MaterialPrice
            {
                ID = 2,
                MaterialType = MaterialType.Glass,
                MaterialCategory = ConstructionType.Windows,
                PriceWithoutTax = 120.0m,
                SupplierId = supplier2.ID,
                Supplier = supplier2
            };

            db.Set<MaterialPrice>().Add(materialPrice1);
            db.Set<MaterialPrice>().Add(materialPrice2);

            var materialOrder1 = new MaterialOrder
            {
                ID = 101,
                UnitPriceNet = 50.0m,
                UnitPriceGross = 61.5m,
                Quantity = 10,
                UserId = 21,
                SupplierId = supplier1.ID,
                Supplier = supplier1,
                MaterialPriceId = materialPrice1.ID,
                MaterialPrice = materialPrice1,
                CreatedDate = DateTime.UtcNow.AddDays(-5)
            };

            var materialOrder2 = new MaterialOrder
            {
                ID = 102,
                UnitPriceNet = 120.0m,
                UnitPriceGross = 147.6m,
                Quantity = 5,
                UserId = 20, 
                SupplierId = supplier2.ID,
                Supplier = supplier2,
                MaterialPriceId = materialPrice2.ID,
                MaterialPrice = materialPrice2,
                CreatedDate = DateTime.UtcNow.AddDays(-3)
            };

            var materialOrder3 = new MaterialOrder
            {
                ID = 103,
                UnitPriceNet = 75.0m,
                UnitPriceGross = 92.25m,
                Quantity = 8,
                UserId = 21,
                SupplierId = supplier1.ID,
                Supplier = supplier1,
                MaterialPriceId = materialPrice1.ID,
                MaterialPrice = materialPrice1,
                CreatedDate = DateTime.UtcNow.AddDays(-1)
            };

            var materialOrder4 = new MaterialOrder
            {
                ID = 104,
                UnitPriceNet = 50.0m,
                UnitPriceGross = 61.5m,
                Quantity = 10,
                UserId = 21,
                SupplierId = supplier1.ID,
                Supplier = supplier1,
                MaterialPriceId = materialPrice1.ID,
                MaterialPrice = materialPrice1,
                CreatedDate = DateTime.UtcNow.AddDays(-5)
            };

            db.Set<MaterialOrder>().Add(materialOrder1);
            db.Set<MaterialOrder>().Add(materialOrder2);
            db.Set<MaterialOrder>().Add(materialOrder3);
            db.Set<MaterialOrder>().Add(materialOrder4);

            db.SaveChanges();
        }
    }
}
