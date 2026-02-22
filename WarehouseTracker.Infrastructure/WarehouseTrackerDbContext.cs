using Microsoft.EntityFrameworkCore;
using WarehouseTracker.Domain;

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

        

        public DbSet<Colleague> Colleagues { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<TaskAssignment> TaskAssignments { get; set; } = null!;
        public DbSet<   BreakRule> BreakRules { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<ActivitySession> ActivitySessions { get; set; } = null!;
        public DbSet<WorkDay> WorkDays { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Colleague>()
                .HasKey(c => c.ColleagueId);

            modelBuilder.Entity<WorkDay>()
                .HasOne(w => w.Colleague)
                .WithMany()
                .HasForeignKey(w => w.ColleagueId);
        }

    }
}
