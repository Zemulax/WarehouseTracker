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
    public class EventRepository : IEventRepository
    {
        private readonly WarehouseTrackerDbContext _dbContext;

        public EventRepository(WarehouseTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Event evt)
        {
            await _dbContext.Events.AddAsync(evt);
        }

        public async Task<List<Event>> GetByColleagueAsync(string colleagueId)
        {
            return await _dbContext.Events
            .Where(e => e.ColleagueId == colleagueId)
            .OrderBy(e => e.TimestampUtc)
            .ToListAsync();
        }

        public async Task<List<Event>> GetByShiftAsync(int shiftAssignmentId)
        {
            return await _dbContext.Events
                .Where(e => e.TaskAssignmentId == shiftAssignmentId)
                .OrderBy(e => e.TimestampUtc)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
