using Microsoft.EntityFrameworkCore;
using WarehouseTracker.Infrastructure;
using System.Text.Json.Serialization;
using WarehouseTracker.Application.Repositories;
using WarehouseTracker.Application.Services; // Add this using directive

namespace WarehouseTracker.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddControllers();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddScoped<IColleagueRepository, ColleagueRepository>();
            builder.Services.AddScoped<IDepartmenRepository, DepartmentRepository>();
            builder.Services.AddScoped<IShiftAssignmentRepository, ShiftAssignmentRepository>();
            builder.Services.AddScoped<IBreakRuleRepository, BreakRuleRepository>();
            builder.Services.AddScoped<IEventRepository, EventRepository>();

            builder.Services.AddScoped<IColleagueService, ColleagueService>();

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
