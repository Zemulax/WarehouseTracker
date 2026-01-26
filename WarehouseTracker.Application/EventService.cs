using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Domain;
using WarehouseTracker.Infrastructure;

namespace WarehouseTracker.Application
{
    public class EventService : IEventService
    {
        private readonly WarehouseTrackerDbContext _dbContext;

        public EventService(WarehouseTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task LogEventAsync(string eventType, int colleagueId, int departmentId, DateTime eventTimestamp, string source)
        {
            var newEvent = new Event
            {
                EventType = eventType,
                ColleagueId = colleagueId,
                DepartmentId = departmentId,
                Timestamp = eventTimestamp,
                Source = source
            };

            await _dbContext.Events.AddAsync(newEvent);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Event>> RetrieveEventsAsync()
        {
            return await _dbContext.Events.ToListAsync();
        }

        public async Task<List<Event>> RetrieveEventsByAttribute(Guid? Id, int? colleagueId, 
            DateTime? eventTimestamp, string? eventType, int? departmentId, string? source)
        {
            return await _dbContext.Events
                .Where(e => (!Id.HasValue || e.Id == Id.Value) &&
                            (!colleagueId.HasValue || e.ColleagueId == colleagueId.Value) &&
                            (!eventTimestamp.HasValue || e.Timestamp == eventTimestamp.Value) &&
                            (string.IsNullOrEmpty(eventType) || e.EventType == eventType) &&
                            (!departmentId.HasValue || e.DepartmentId == departmentId.Value))
                .ToListAsync();
        }
    }
}
