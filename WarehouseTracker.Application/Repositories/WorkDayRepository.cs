using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WarehouseTracker.Domain;
using WarehouseTracker.Infrastructure;

namespace WarehouseTracker.Application.Repositories
{
    public class WorkDayRepository : IWorkDayRepository
    {
        private readonly WarehouseTrackerDbContext _dbContext;

        public WorkDayRepository(WarehouseTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddWorkDayAsync(WorkDay workDay)
        {
            await _dbContext.WorkDays.AddAsync(workDay);
        }

        public async Task<WorkDay?> GetActiveWorkDayForColleagueAsync(string colleagueId)
        {
            return await _dbContext.WorkDays
                .Include(w => w.TaskAssignments)
                .Where(c => c.ColleagueId == colleagueId && c.WorkDayEnd == null)
                .FirstOrDefaultAsync();
        }

        public async Task<List<WorkDay>> GetAllActiveWorkDaysAsync()
        {
            return await _dbContext.WorkDays
                .Where(w => w.WorkDayEnd == null && w.Status == "Active")
                .ToListAsync();
        }

        public async Task<List<WorkDay>> GetAllWorkDaysForColleague(string colleagueId)
        {
            return await _dbContext.WorkDays
                .Where(w => w.ColleagueId == colleagueId)
                .ToListAsync();
        }

        public async Task<WorkDay?> GetByIdAsync(int workDayId)
        {
          return await _dbContext.WorkDays.FindAsync(workDayId);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public Task UpdateWorkDayAsync(WorkDay workDay)
        {
            _dbContext.WorkDays.Update(workDay);
            return Task.CompletedTask;
        }
    }
}
