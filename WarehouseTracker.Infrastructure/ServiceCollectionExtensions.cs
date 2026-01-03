using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTracker.Infrastructure
{
    /// <summary>
    /// helper class that plugs infrastructure services into the ASP.NET DI container
    /// adds the DbContext to the DI container using the connection string from configuration(Default Connection)
    /// Allows the Api project to call AddInfrastructureServices in Program.cs
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WarehouseTrackerDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            // Register other infrastructure services here
            return services;
        }
    }
}
