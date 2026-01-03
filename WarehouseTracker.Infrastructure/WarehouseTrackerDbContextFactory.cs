using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace WarehouseTracker.Infrastructure
{
    public class WarehouseTrackerDbContextFactory : IDesignTimeDbContextFactory<WarehouseTrackerDbContext>
    {
        /// <summary>
        /// design time factory for EF Core tools(Add-Migration, Update-database)
        /// This class is used by the EF Core command line tools to create an instance of the DbContext
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public WarehouseTrackerDbContext CreateDbContext(string[] args)
        {
            //get the path to the appsettings.json file
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../WarehouseTracker.Api");

            // Build configuration
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional:false)
                .AddJsonFile($"appsettings.Development.json", optional: true)
                .Build();

            // Build DbContextOptions
            var builder = new DbContextOptionsBuilder<WarehouseTrackerDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Could not find connection string 'DefaultConnection' in appsettings.json");
            }

            builder.UseSqlServer(connectionString);

            return new WarehouseTrackerDbContext(builder.Options);
        }
    }
}