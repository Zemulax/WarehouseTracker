using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Application.ActivitySessions
{
    public interface IActivitySessionService
    {
        Task<List<ActivitySession>> GetByShiftAsync(int? ShiftId, string? ColleagueId);


    }
}
