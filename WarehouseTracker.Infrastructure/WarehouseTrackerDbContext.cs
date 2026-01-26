using Microsoft.EntityFrameworkCore;

namespace WarehouseTracker.Infrastructure
{
    /// <summary>
    /// This is the gateway to the database for the Warehouse Tracker application.
    /// It talks to the SQL Server
    /// The EF uses it to query and save instances of the Colleague entity.
    /// This is the database schema representation.
    /// </summary>
    public class WarehouseTrackerDbContext : DbContext
    {
        public WarehouseTrackerDbContext(DbContextOptions<WarehouseTrackerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.Colleague> Colleagues { get; set; } = null!;
        public DbSet<Domain.Department> Departments { get; set; } = null!;
        public DbSet<Domain.ShiftAssignment> ShiftAssignments { get; set; } = null!;
        public DbSet<Domain.BreakRule> BreakRules { get; set; } = null!;
        public DbSet<Domain.Event> Events { get; set; } = null!;
        public DbSet<Domain.ActivitySession> ActivitySessions { get; set; } = null!;
    }
}
