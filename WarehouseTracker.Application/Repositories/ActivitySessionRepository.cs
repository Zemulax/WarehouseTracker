using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Domain;
using WarehouseTracker.Infrastructure;

namespace WarehouseTracker.Application.Repositories
{
    public class ActivitySessionRepository : IActivitySessionRepository
    {
        private readonly WarehouseTrackerDbContext _dbContext;

        public ActivitySessionRepository(WarehouseTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddRangeAsync(List<ActivitySession> sessions)
        {
            await _dbContext.ActivitySessions.AddRangeAsync(sessions);
        }

        public async Task DeleteByWorkDayAsync(int workDayId)
        {
            var sessions = _dbContext.ActivitySessions
                .Where(s => s.WorkDayId == workDayId);

            _dbContext.ActivitySessions.RemoveRange(sessions);
            await Task.CompletedTask;
        }

        public async Task<List<ActivitySession>> GetByTaskAsync(int workId)
        {
            return await _dbContext.ActivitySessions
            .Where(a => a.WorkDayId == workId)
            .OrderBy(s => s.SessionStart)
            .ToListAsync();
        }

        public async Task<List<ActivitySession>> GetByShiftAsync(int? workId, string? colleagueId)
        {
            return await _dbContext.ActivitySessions
            .Where(a => a.WorkDayId == workId 
            || a.ColleagueId == colleagueId
            )
            .OrderBy(s => s.SessionStart)
            .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
