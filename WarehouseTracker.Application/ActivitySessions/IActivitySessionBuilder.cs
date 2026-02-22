using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Application.ActivitySessions
{
    /// <summary>
    /// takes shift
    /// takes ordered events
    /// returns computes sessions
    /// </summary>
    public interface IActivitySessionBuilder
    {
        List<ActivitySession> Build(WorkDay workDay,
            IReadOnlyList<Event> events);
    }
}
