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
        Task<TaskAssignment?> GetByIdAsync(int taskId);
        Task<TaskAssignment?> GetActiveTaskAsync(string colleagueId);
        Task<List<TaskAssignment>> GetTasksAsync(DateTimeOffset now);
        Task UpdateAsync(TaskAssignment taskAssignment);
        Task SaveChangesAsync();
    }
}
