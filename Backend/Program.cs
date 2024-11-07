
using Backend.Data.Context;
using Backend.Factories;
using Backend.Repositories;
using Backend.services;
using Backend.Validation;
using Microsoft.EntityFrameworkCore;

namespace Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Configure Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure Database Context
            builder.Services.AddDbContext<PrediBudDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("PrediBudConnection")));

            // Add repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IConstructionOrderRepository, ConstructionOrderRepository>();

            // Add services
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IConstructionOrderService, ConstructionOrderService>();

            // Add additional utilities
            builder.Services.AddScoped<IPasswordValidation, PasswordValidation>();
            builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            // Add factory
            builder.Services.AddScoped<IConstructionSpecificationFactory, ConstructionSpecificationFactory>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
