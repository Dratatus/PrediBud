using Backend.Conventer;
using Backend.Data.Context;
using Backend.Factories;
using Backend.Middlewares;
using Backend.Repositories;
using Backend.services;
using Backend.services.Calculator;
using Backend.Services;
using Backend.Validation;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.Converters.Add(new ConstructionSpecificationJsonConverter());
            });

            // Configure Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure Database Context
            builder.Services.AddDbContext<PrediBudDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("PrediBudConnection")));

            // Add repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IConstructionOrderRepository, ConstructionOrderRepository>();
            builder.Services.AddScoped<IConstructionOrderNotificationRepository, ConstructionOrderNotificationRepository>();
            builder.Services.AddScoped<IMaterialOrderRepository, MaterialOrderRepository>();
            builder.Services.AddScoped<IMaterialPriceRepository, MaterialPriceRepository>();


            // Add services
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IConstructionOrderService, ConstructionOrderService>();
            builder.Services.AddScoped<INegotiationService, NegotiationService>(); 
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<ISupplierService, SupplierService>();
            builder.Services.AddScoped<IMaterialOrderService, MaterialOrderService>();
            builder.Services.AddScoped<ICalculatorService, CalculatorService>();


            // Add additional utilities
            builder.Services.AddScoped<IPasswordValidation, PasswordValidation>();
            builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            // Add factory
            builder.Services.AddScoped<IConstructionSpecificationFactory, ConstructionSpecificationFactory>();

            builder.Services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(builder.Configuration.GetConnectionString("PrediBudConnection"), new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.FromSeconds(15),
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            }));
            builder.Services.AddHangfireServer();


            builder.WebHost.UseUrls("http://localhost:5142", "https://localhost:7290");

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHangfireDashboard();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();


            using (var scope = app.Services.CreateScope())
            {
                var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
                recurringJobManager.AddOrUpdate<ISupplierService>(
                    "UpdateSuppliers",
                    service => service.UpdateSuppliersAsync("Data/suppliers.json"),
                    Cron.Minutely);
            }

            app.Run();
        }
    }
}
