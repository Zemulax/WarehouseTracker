using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Application.Repositories
{
    public interface ITaskAssignmentRepository
    {
        Task AddAsync(TaskAssignment taskAssignment);
        Task<TaskAssignment?> GeByIdAsync(int shiftId);
        Task <TaskAssignment?>GetActiveShiftAsync(string colleagueId, DateTimeOffset timeStamp);
        Task<List<TaskAssignment>> GetShiftsAsync(DateTimeOffset now);
        Task SaveChangesAsync();
    }
}
