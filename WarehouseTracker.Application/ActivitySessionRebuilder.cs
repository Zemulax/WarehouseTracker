using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Application.ActivitySessionBuilder;

namespace WarehouseTracker.Application
{
    public class ActivitySessionRebuilder : IActivitySessionRebuilder
    {
        private readonly IShiftAssignmentService _shiftAssignmentService;
        private readonly IEventService _eventService;
        private readonly IActivitySessionService _activitySessionService;
        private readonly IActivitySessionBuilder _activitySessionBuilder;

        public ActivitySessionRebuilder(
            IShiftAssignmentService shiftAssignmentService,
            IEventService eventService,
            IActivitySessionService activitySessionService,
            IActivitySessionBuilder activitySessionBuilder)
        {
            _shiftAssignmentService = shiftAssignmentService;
            _eventService = eventService;
            _activitySessionService = activitySessionService;
            _activitySessionBuilder = activitySessionBuilder;
        }

        public async Task RebuildForShiftAsync(int shiftAssignmentId)
        {
            var shift = await _shiftAssignmentService.RetrieveShiftAssignmentsByAttribute(
                shiftAssignmentId, null, 
                null, null, null);

            var events = await _eventService.RetrieveEventsByAttribute(
                shift.ColleagueId, null, null,null,null, 
                shiftAssignmentId);

            var activitySession = _activitySessionBuilder.BuildActivitySessions(shift, events);

            await _activitySessionService.ReplaceForShiftAsync(shiftAssignmentId,activitySession);
        }
    }
}
