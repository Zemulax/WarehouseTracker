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
        private readonly IWorkDayRepository _workDayRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IActivitySessionRepository _activitySessionRepository;
        private readonly IActivitySessionBuilder _activitySessionBuilder;

        public ActivitySessionRebuilder(
            IWorkDayRepository workDayRepository, 
            IEventRepository eventRepository, 
            IActivitySessionRepository activitySessionRepository, 
            IActivitySessionBuilder activitySessionBuilder)
        {
            _workDayRepository = workDayRepository;
            _eventRepository = eventRepository;
            _activitySessionRepository = activitySessionRepository;
            _activitySessionBuilder = activitySessionBuilder;
        }

        public async Task RebuildForAsync(int workDayId)
        {
            var workday = await _workDayRepository.GetByIdAsync(workDayId);
            if (workday == null)
            {
                throw new ArgumentNullException(nameof(workDayId));
            }
            var events = await _eventRepository.GetByWorkDayAsync(workDayId);

            var sessions = _activitySessionBuilder.Build(workday, events);

            await _activitySessionRepository.DeleteByWorkDayAsync(workDayId);
            await _activitySessionRepository.AddRangeAsync(sessions);
            await _activitySessionRepository.SaveChangesAsync();
        }
    }
}
