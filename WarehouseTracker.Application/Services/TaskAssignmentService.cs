using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Application.Repositories;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Application.Services
{
    public class TaskAssignmentService : ITaskAssignmentService
    {
        private readonly ITaskAssignmentRepository _repository;
        public TaskAssignmentService(ITaskAssignmentRepository taskAssignmentRepository) {
        
        _repository = taskAssignmentRepository;
        }

        public async Task CreateAsync(TaskAssignment assignment)
        {
            var shift = new TaskAssignment
            {
                ColleagueId = assignment.ColleagueId,
                ShiftStart = assignment.ShiftStart,
                ShiftEnd = assignment.ShiftEnd,
            };

            await _repository.AddAsync(shift);
            await _repository.SaveChangesAsync();
        }

        public Task<TaskAssignment?> GetShiftActiveShiftAsync(string colleagueId, DateTimeOffset timeStamp)
        {
            return _repository.GetActiveShiftAsync(colleagueId, timeStamp);
        }

        public async Task<List<TaskAssignment>>GetShiftsAsync(DateTimeOffset now)
        {
            return await _repository.GetShiftsAsync(now);
        }
    }
}
