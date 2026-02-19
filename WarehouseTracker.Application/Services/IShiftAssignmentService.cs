using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Application.Services
{
    public interface IShiftAssignmentService
    {
        Task CreateAsync(ShiftAssignment assignment);
        Task<ShiftAssignment?> GetShiftActiveShiftAsync(string colleagueId, DateTimeOffset timeStamp);
        Task<List<ShiftAssignment>> GetShiftsAsync(DateTimeOffset now);
    }
}
