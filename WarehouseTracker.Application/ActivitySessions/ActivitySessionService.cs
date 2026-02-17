using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Application.Repositories;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Application.ActivitySessions
{
    public class ActivitySessionService : IActivitySessionService
    {
        private readonly IActivitySessionRepository _activitySessionRepository;

        public ActivitySessionService(IActivitySessionRepository activitySessionRepository)
        {
            _activitySessionRepository = activitySessionRepository;
        }
        

        public async Task<List<ActivitySession>> GetByShiftAsync(int? ShiftId, string? colleagueId)
        {
            return await _activitySessionRepository.GetByShiftAsync(ShiftId, colleagueId);
        }
    }
}
