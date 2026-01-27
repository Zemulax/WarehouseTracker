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
    public class ActivitySessionServiceRepository : IActivitySessionService
    {
        // Implementation for ActivitySessionService goes here.
        private readonly WarehouseTrackerDbContext _dbContext;

        public ActivitySessionServiceRepository(WarehouseTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ReplaceForShiftAsync(int ShiftAssignmentId, IReadOnlyList<ActivitySession> sessions)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var existing = await _dbContext.ActivitySessions
                    .Where(a => a.ShiftAssignmentId == ShiftAssignmentId)
                    .ToListAsync();
                _dbContext.ActivitySessions.RemoveRange(existing);
                _dbContext.ActivitySessions.AddRange(sessions);

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<ActivitySession>> RetrieveActivitySessionsAsync()
        {
            return await _dbContext.ActivitySessions.ToListAsync();
        }

        public async Task<List<ActivitySession>> RetrieveActivitySessionsByAttribute(int? ColleagueId, 
            int? DepartmentId, int? ShiftAssignmentId, string? SessionType, 
            TimeOnly? sessionStart, TimeOnly? sessionEnd)
        {
            return await _dbContext.ActivitySessions
                .Where(a =>
                    (!ColleagueId.HasValue || a.ColleagueId == ColleagueId.Value) &&
                    (!DepartmentId.HasValue || a.DepartmentId == DepartmentId.Value) &&
                    (!ShiftAssignmentId.HasValue || a.ShiftAssignmentId == ShiftAssignmentId.Value) &&
                    (string.IsNullOrEmpty(SessionType) || a.SessionType == SessionType) &&
                    (!sessionStart.HasValue || a.SessionStart >= sessionStart.Value) &&
                    (!sessionEnd.HasValue || a.SessionStart <= sessionEnd.Value)
                )
                .ToListAsync();
        }

        public async Task StartSessionAsync( int ColleagueId, int DepartmentId, int ShiftAssignmentId, 
            string SessionType, TimeOnly sessionStart)
        {
            var activitySession = new ActivitySession
            {
                ColleagueId = ColleagueId,
                DepartmentId = DepartmentId,
                ShiftAssignmentId = ShiftAssignmentId,
                SessionType = SessionType,
                SessionStart = sessionStart
            };

             _dbContext.ActivitySessions.Add(activitySession);
            await _dbContext.SaveChangesAsync();
        }
    }
}
