using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Application.Repositories;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Application.Services
{
    public class WorkDayService : IWorkDayService
    {
        private readonly IWorkDayRepository _workDayRepository;
        private readonly ITaskAssignmentService _taskAssignmentService;
        public WorkDayService(IWorkDayRepository workDayRepository, ITaskAssignmentService taskAssignmentService)
        {
            _workDayRepository = workDayRepository;
            _taskAssignmentService = taskAssignmentService;
        }
        public async Task EndWorkDayAsync(string colleagueId)
        {
            var active = await _workDayRepository.GetActiveWorkDayForColleagueAsync(colleagueId);
            if (active == null)
            {
                throw new InvalidOperationException("No active work day found for colleague.");
            }

            var activeTask = await _taskAssignmentService.GetActiveTaskAsync(colleagueId);
            if (activeTask != null)
            {
                await _taskAssignmentService.EndTaskAsync(activeTask);
            }

            active.WorkDayEnd = DateTimeOffset.UtcNow;
            active.Status = "Completed";
           await _workDayRepository.UpdateWorkDayAsync(active);
           await _workDayRepository.SaveChangesAsync();
        }

        

        public async Task<WorkDay?> GetActiveWorkDay(string colleagueId)
        {
            return await _workDayRepository.GetActiveWorkDayForColleagueAsync(colleagueId);
        }

        public async Task<List<WorkDay>> GetAllActiveWorkDaysAsync()
        {
            return await _workDayRepository.GetAllActiveWorkDaysAsync();
        }

        public async  Task StartWorkDayAsync(string colleagueId)
        {
            var existing = await _workDayRepository.GetActiveWorkDayForColleagueAsync(colleagueId);
            if (existing != null)
            {
                throw new InvalidOperationException("Colleague already has an active work day.");
            }

            var workDay = new WorkDay
            {
                ColleagueId = colleagueId,
                WorkDayStart = DateTimeOffset.UtcNow,
                Status = "Active"
            };
            await _workDayRepository.AddWorkDayAsync(workDay);
            await _workDayRepository.SaveChangesAsync();
        }

        
    }
}
