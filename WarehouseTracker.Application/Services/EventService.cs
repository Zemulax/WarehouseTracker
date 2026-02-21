using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Api.Enums;
using WarehouseTracker.Application.ActivitySessions;
using WarehouseTracker.Application.Repositories;
using WarehouseTracker.Domain;
using WarehouseTracker.Domain.Enums;

namespace WarehouseTracker.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IColleagueService _colleagueService;
        private readonly IDepartmentService _departmentService;
        private readonly ITaskAssignmentService _taskService;
        private readonly IActivitySessionRebuilder _activitySessionRebuilder;
        

        public EventService(
            IEventRepository eventRepository,
            IColleagueService colleagueService,
            IDepartmentService departmentService,
            ITaskAssignmentService taskService,
            IActivitySessionRebuilder activitySessionRebuilder)
        {
            _eventRepository = eventRepository;
            _colleagueService = colleagueService;
            _departmentService = departmentService;
            _taskService = taskService;
            _activitySessionRebuilder = activitySessionRebuilder;
        }

        public async Task CreateEventAsync(Event request)
        {
            // 1. Resolve colleague
            var colleague = await _colleagueService.GetColleagueById(request.ColleagueId);
            if (colleague == null)
                throw new Exception("Colleague not found");

            // 2. Resolve shift
            var shift = await _taskService.GetShiftActiveShiftAsync(colleague.ColleagueId, request.TimestampUtc );
            Console.WriteLine(shift);
            if (shift == null)
                throw new Exception("No active shift");

            // 3. Resolve department if provided
            string? departmentCode = null;
            if (!string.IsNullOrEmpty(request.DepartmentCode))
            {
                var dept = await _departmentService.GetByCodeAsync(request.DepartmentCode);
                if (dept == null)
                    throw new Exception("Department not found");

                departmentCode = dept.DepartmentCode;
               
            }
            // 4. Create event
            var evt = new Event
            {
                ColleagueId = colleague.ColleagueId,
                DepartmentCode = departmentCode,
                TaskAssignmentId = shift.Id,
                EventType = request.EventType,
                TimestampUtc = request.TimestampUtc,
                Source = "User"
            };

            await _eventRepository.AddAsync(evt);
            await _eventRepository.SaveChangesAsync();
            // 5. Rebuild sessions
            await _activitySessionRebuilder.RebuildForAsync(shift.Id);

        }

        public async Task CreateBreakStartedEventAsync(TaskAssignment task)
        {
            var evt = new Event
            {
                ColleagueId = task.ColleagueId,
                TaskAssignmentId = task.Id,
                EventType = EventTypes.BreakStarted,
                TimestampUtc = DateTimeOffset.UtcNow,
                
                Source = "System"
            };
            await _eventRepository.AddAsync(evt);
            await _eventRepository.SaveChangesAsync();
            await _activitySessionRebuilder.RebuildForAsync(task.Id);
        }

        public async Task CreateBreakEndedEventAsync(TaskAssignment task)
        {
            var evt = new Event
            {
                ColleagueId = task.ColleagueId,
                TaskAssignmentId = task.Id,
                EventType = EventTypes.BreakEnded,
                TimestampUtc = DateTimeOffset.UtcNow, // ✅ Fixed: Was DateTime.Now
                Source = "System"
            };
            await _eventRepository.AddAsync(evt);
            await _eventRepository.SaveChangesAsync();
            await _activitySessionRebuilder.RebuildForAsync(task.Id);
        }
        public async Task<List<Event>> GetEventsByShiftAsync(int shiftAssignmentId)
        {
            return await _eventRepository.GetByShiftAsync(shiftAssignmentId);
        }
    }
}
