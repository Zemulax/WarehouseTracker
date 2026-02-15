using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Application.Repositories;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IColleagueService _colleagueService;
        private readonly IDepartmentService _departmentService;
        private readonly IShiftAssignmentService _shiftService;
        

        public EventService(
            IEventRepository eventRepository,
            IColleagueService colleagueService,
            IDepartmentService departmentService,
            IShiftAssignmentService shiftService)
        {
            _eventRepository = eventRepository;
            _colleagueService = colleagueService;
            _departmentService = departmentService;
            _shiftService = shiftService;
        }

        public async Task CreateEventAsync(Event request)
        {
            // 1. Resolve colleague
            var colleague = await _colleagueService.GetColleagueById(request.ColleagueId);
            if (colleague == null)
                throw new Exception("Colleague not found");

            // 2. Resolve shift
            var shift = await _shiftService.GetShiftAssignment(colleague.ColleagueId);
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
            }

            // 5. Rebuild sessions
        }

        public async Task<List<Event>> GetEventsByShiftAsync(int shiftAssignmentId)
        {
            return await _eventRepository.GetByShiftAsync(shiftAssignmentId);
        }
    }
}
