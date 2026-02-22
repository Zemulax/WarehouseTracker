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
        private readonly IActivitySessionRebuilder _activitySessionRebuilder;
        private readonly IWorkDayService _workDayService;


        public EventService(
            IEventRepository eventRepository,
            IColleagueService colleagueService,
            IDepartmentService departmentService,
            IActivitySessionRebuilder activitySessionRebuilder, 
            IWorkDayService wokDayService)
        {
            _eventRepository = eventRepository;
            _colleagueService = colleagueService;
            _departmentService = departmentService;
            _activitySessionRebuilder = activitySessionRebuilder;
            _workDayService = wokDayService;
        }

        public async Task CreateEventAsync(Event request)
        {
            // 1. Resolve colleague
            var colleague = await _colleagueService.GetColleagueById(request.ColleagueId);
            if (colleague == null)
                throw new Exception("Colleague not found");

            // 2. Resolve shift
            var workDay = await _workDayService.GetActiveWorkDay(colleague.ColleagueId);
            
            if (workDay == null)
                throw new Exception("No active workday found");

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
                WorkDayId = workDay.Id,
                EventType = request.EventType,
                TimestampUtc = request.TimestampUtc,
                Source = "User"
            };

            await _eventRepository.AddAsync(evt);
            await _eventRepository.SaveChangesAsync();
            // 5. Rebuild sessions
            await _activitySessionRebuilder.RebuildForAsync(workDay.Id);

        }

        public async Task CreateBreakStartedEventAsync(WorkDay workDay)
        {
            var evt = new Event
            {
                ColleagueId = workDay.ColleagueId,
                WorkDayId = workDay.Id,
                EventType = EventTypes.BreakStarted,
                TimestampUtc = DateTimeOffset.UtcNow,
                
                Source = "System"
            };
            await _eventRepository.AddAsync(evt);
            await _eventRepository.SaveChangesAsync();
            await _activitySessionRebuilder.RebuildForAsync(workDay.Id);
        }

        public async Task CreateBreakEndedEventAsync(WorkDay workDay)
        {
            var evt = new Event
            {
                ColleagueId = workDay.ColleagueId,
                WorkDayId = workDay.Id,
                EventType = EventTypes.BreakEnded,
                TimestampUtc = DateTimeOffset.UtcNow, // ✅ Fixed: Was DateTime.Now
                Source = "System"
            };
            await _eventRepository.AddAsync(evt);
            await _eventRepository.SaveChangesAsync();
            await _activitySessionRebuilder.RebuildForAsync(workDay.Id);
        }
        public async Task<List<Event>> GetEventsByWorkDayAsync(int workDayId)
        {
            return await _eventRepository.GetByWorkDayAsync(workDayId);
        }
    }
}
