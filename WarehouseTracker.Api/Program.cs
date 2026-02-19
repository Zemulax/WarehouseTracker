using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WarehouseTracker.Application.ActivitySessions;
using WarehouseTracker.Application.Repositories;
using WarehouseTracker.Application.Services; // Add this using directive
using WarehouseTracker.Infrastructure;

namespace WarehouseTracker.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddScoped<IColleagueRepository, ColleagueRepository>();
            builder.Services.AddScoped<IDepartmenRepository, DepartmentRepository>();
            builder.Services.AddScoped<IShiftAssignmentRepository, ShiftAssignmentRepository>();
            builder.Services.AddScoped<IBreakRuleRepository, BreakRuleRepository>();
            builder.Services.AddScoped<IEventRepository, EventRepository>();
            builder.Services.AddScoped<IActivitySessionRepository, ActivitySessionRepository>();
            builder.Services.AddScoped<IBreakRuleRepository, BreakRuleRepository>();

            builder.Services.AddScoped<IColleagueService, ColleagueService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEventService, EventService>();
            builder.Services.AddScoped<IActivitySessionService, ActivitySessionService>();
            builder.Services.AddScoped<IShiftAssignmentService, ShiftAssignmentService>();
            builder.Services.AddScoped<IShiftAssignmentService, ShiftAssignmentService>();
            builder.Services.AddScoped<IBreakRuleService, BreakRuleService>();

            builder.Services.AddScoped<IActivitySessionBuilder, ActivitySessionBuilder>();
            builder.Services.AddScoped<IActivitySessionRebuilder, ActivitySessionRebuilder>();

            builder.Services.AddHostedService<BreakSchedulerService>();

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
