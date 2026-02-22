using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Application.Services;
using WarehouseTracker.Domain;
using WarehouseTracker.Infrastructure;

namespace WarehouseTracker.Application.Repositories
{
    public class TaskAssignmentRepository : ITaskAssignmentRepository
    {
        private readonly WarehouseTrackerDbContext _dbContext;

        public TaskAssignmentRepository(WarehouseTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(TaskAssignment taskAssignment)
        {
            await _dbContext.TaskAssignments.AddAsync(taskAssignment);
        }

        public async Task<TaskAssignment?> GetByIdAsync(int shiftId)
        {
            return await _dbContext.TaskAssignments.
                  SingleOrDefaultAsync(s => s.Id == shiftId);
        }

        public async Task<TaskAssignment?> GetActiveTaskAsync(string colleagueId)
        {
            return await _dbContext.TaskAssignments
                .FirstOrDefaultAsync(s =>
                s.ColleagueId == colleagueId &&
                s.TaskEnd == null);

        }

        public async Task<List<TaskAssignment>> GetTasksAsync(DateTimeOffset now)
        {
            return await _dbContext.TaskAssignments
                .Where(s=> s.TaskStart <= now && s.TaskEnd >= now)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public Task UpdateAsync(TaskAssignment taskAssignment)
        {
            _dbContext.TaskAssignments.Update(taskAssignment);
            return Task.CompletedTask;
        }
    }
}
