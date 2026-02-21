using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Application.Services
{
    public interface ITaskAssignmentService
    {
        Task CreateAsync(TaskAssignment assignment);
        Task<TaskAssignment?> GetShiftActiveShiftAsync(string colleagueId, DateTimeOffset timeStamp);
        Task<List<TaskAssignment>> GetShiftsAsync(DateTimeOffset now);
    }
}
