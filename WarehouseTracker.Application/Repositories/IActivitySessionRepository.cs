using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Application.Repositories
{
    public interface IActivitySessionRepository
    {
        Task<List<ActivitySession>> GetByShiftAsync(int? shiftId, string? colleagueId);
        Task DeleteByWorkDayAsync(int shiftId);
        Task AddRangeAsync(List<ActivitySession> sessions);
        Task SaveChangesAsync();
    }
}
