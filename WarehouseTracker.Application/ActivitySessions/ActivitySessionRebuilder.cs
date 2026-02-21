using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Application.Repositories;

namespace WarehouseTracker.Application.ActivitySessions
{
    public class ActivitySessionRebuilder : IActivitySessionRebuilder
    {
        private readonly ITaskAssignmentRepository _taskAssignmentRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IActivitySessionRepository _activitySessionRepository;
        private readonly IActivitySessionBuilder _activitySessionBuilder;

        public ActivitySessionRebuilder(ITaskAssignmentRepository assignmentRepository, IEventRepository eventRepository, IActivitySessionRepository activitySessionRepository, IActivitySessionBuilder activitySessionBuilder)
        {
            _taskAssignmentRepository = assignmentRepository;
            _eventRepository = eventRepository;
            _activitySessionRepository = activitySessionRepository;
            _activitySessionBuilder = activitySessionBuilder;
        }

        public async Task RebuildForAsync(int shiftId)
        {
            var shift = await _taskAssignmentRepository.GeByIdAsync(shiftId);
            if (shift == null)
            {
                throw new ArgumentNullException(nameof(shiftId));
            }
            var events = await _eventRepository.GetByShiftAsync(shiftId);

            var sessions = _activitySessionBuilder.Build(shift, events);

            await _activitySessionRepository.DeleteShiftAsync(shiftId);
            await _activitySessionRepository.AddRangeAsync(sessions);
            await _activitySessionRepository.SaveChangesAsync();
        }
    }
}
