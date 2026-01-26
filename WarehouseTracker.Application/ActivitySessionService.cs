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
    public class ActivitySessionService : IActivitySessionService
    {
        // Implementation for ActivitySessionService goes here.
        private readonly WarehouseTrackerDbContext _dbContext;

        public ActivitySessionService(WarehouseTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<ActivitySession>> RetrieveActivitySessionsAsync()
        {
            return await _dbContext.ActivitySessions.ToListAsync();
        }

        public async Task<List<ActivitySession>> RetrieveActivitySessionsByAttribute(int? Id, int? ColleagueId, int? DepartmentId, int? ShiftAssignmentId, string? SessionType, TimeOnly? sessionStart, TimeOnly? sessionEnd)
        {
            return await _dbContext.ActivitySessions
                .Where(a =>
                    (!Id.HasValue || a.Id == Id.Value) &&
                    (!ColleagueId.HasValue || a.ColleagueId == ColleagueId.Value) &&
                    (!DepartmentId.HasValue || a.DepartmentId == DepartmentId.Value) &&
                    (!ShiftAssignmentId.HasValue || a.ShiftAssignmentId == ShiftAssignmentId.Value) &&
                    (string.IsNullOrEmpty(SessionType) || a.SessionType == SessionType) &&
                    (!sessionStart.HasValue || a.SessionStart >= sessionStart.Value) &&
                    (!sessionEnd.HasValue || a.SessionStart <= sessionEnd.Value)
                )
                .ToListAsync();
        }

        public async Task StartSessionAsync(int Id, int ColleagueId, int DepartmentId, int ShiftAssignmentId, string SessionType, TimeOnly sessionStart, TimeOnly sessionEnd)
        {
            var activitySession = new ActivitySession
            {
                Id = Id,
                ColleagueId = ColleagueId,
                DepartmentId = DepartmentId,
                ShiftAssignmentId = ShiftAssignmentId,
                SessionType = SessionType,
                SessionStart = sessionStart,
                SessionEnd = sessionEnd
            };

             _dbContext.ActivitySessions.Add(activitySession);
            await _dbContext.SaveChangesAsync();
        }
    }
}
