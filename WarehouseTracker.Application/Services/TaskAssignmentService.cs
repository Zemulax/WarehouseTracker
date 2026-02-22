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
            var existingTask = await _repository.GetActiveTaskAsync(assignment.ColleagueId);
            
            if (existingTask != null)
            {
                throw new InvalidOperationException($"Colleague {assignment.ColleagueId} already has an active task.");
            }

            var newTask = new TaskAssignment
            {
                ColleagueId = assignment.ColleagueId,
                WorkDayId = assignment.WorkDayId,
                DepartmentCode = assignment.DepartmentCode,
                TaskStart = DateTimeOffset.UtcNow,
                Status = "Active"
            };
            

            await _repository.AddAsync(newTask);
            await _repository.SaveChangesAsync();
        }

        public async Task EndTaskAsync(TaskAssignment taskAssignment)
        {
            taskAssignment.TaskEnd = DateTimeOffset.UtcNow;
            taskAssignment.Status = "Completed";
            await _repository.UpdateAsync(taskAssignment);
            await _repository.SaveChangesAsync();
        }

        public async Task<TaskAssignment?> GetActiveTaskAsync(string colleagueId)
        {
            return await _repository.GetActiveTaskAsync(colleagueId);
        }
    }
}
