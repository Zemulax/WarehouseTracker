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
            await _dbContext.ShiftAssignments.AddAsync(taskAssignment);
        }

        public async Task<TaskAssignment?> GeByIdAsync(int shiftId)
        {
            return await _dbContext.ShiftAssignments.
                  SingleOrDefaultAsync(s => s.Id == shiftId);
        }

        public async Task<TaskAssignment?> GetActiveShiftAsync(string colleagueId, DateTimeOffset timeStamp)
        {
            return await _dbContext.ShiftAssignments
                .FirstOrDefaultAsync(s =>
                s.ColleagueId == colleagueId &&
                s.ShiftStart <= timeStamp &&
                s.ShiftEnd >= timeStamp);

        }

        public async Task<List<TaskAssignment>> GetShiftsAsync(DateTimeOffset now)
        {
            return await _dbContext.ShiftAssignments
                .Where(s=> s.ShiftStart <= now && s.ShiftEnd >= now)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
