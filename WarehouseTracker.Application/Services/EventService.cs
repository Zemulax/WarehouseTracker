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
        private readonly IShiftAssignmentService _shiftService;
        private readonly IActivitySessionRebuilder _activitySessionRebuilder;
        

        public EventService(
            IEventRepository eventRepository,
            IColleagueService colleagueService,
            IDepartmentService departmentService,
            IShiftAssignmentService shiftService,
            IActivitySessionRebuilder activitySessionRebuilder)
        {
            _eventRepository = eventRepository;
            _colleagueService = colleagueService;
            _departmentService = departmentService;
            _shiftService = shiftService;
            _activitySessionRebuilder = activitySessionRebuilder;
        }

        public async Task CreateEventAsync(Event request)
        {
            // 1. Resolve colleague
            var colleague = await _colleagueService.GetColleagueById(request.ColleagueId);
            if (colleague == null)
                throw new Exception("Colleague not found");

            // 2. Resolve shift
            var shift = await _shiftService.GetShiftActiveShiftAsync(colleague.ColleagueId, request.TimestampUtc );
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

                departmentCode = dept.DeparmentCode;
               
            }
            // 4. Create event
            var evt = new Event
            {
                ColleagueId = colleague.ColleagueId,
                DepartmentCode = departmentCode,
                ShiftAssignmentId = shift.Id,
                EventType = request.EventType,
                TimestampUtc = request.TimestampUtc,
                Source = "User"
            };

            await _eventRepository.AddAsync(evt);
            await _eventRepository.SaveChangesAsync();
            // 5. Rebuild sessions
            await _activitySessionRebuilder.RebuildForAsync(shift.Id);

        }

        public async Task CreateBreakStartedEventAsync(ShiftAssignment shift)
        {
            var evt = new Event
            {
                ColleagueId = shift.ColleagueId,
                ShiftAssignmentId = shift.Id,
                EventType = EventTypes.BreakStarted,
                TimestampUtc = DateTimeOffset.UtcNow,
                
                Source = "System"
            };
            await _eventRepository.AddAsync(evt);
            await _eventRepository.SaveChangesAsync();
            await _activitySessionRebuilder.RebuildForAsync(shift.Id);
        }

        public async Task CreateBreakEndedEventAsync(ShiftAssignment shift)
        {
            var evt = new Event
            {
                ColleagueId = shift.ColleagueId,
                ShiftAssignmentId = shift.Id,
                EventType = EventTypes.BreakEnded,
                TimestampUtc = DateTimeOffset.UtcNow, // ✅ Fixed: Was DateTime.Now
                Source = "System"
            };
            await _eventRepository.AddAsync(evt);
            await _eventRepository.SaveChangesAsync();
            await _activitySessionRebuilder.RebuildForAsync(shift.Id);
        }
        public async Task<List<Event>> GetEventsByShiftAsync(int shiftAssignmentId)
        {
            return await _eventRepository.GetByShiftAsync(shiftAssignmentId);
        }
    }
}
