using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Application.ActivitySessionBuilder
{
    public interface IActivitySessionBuilder
    {
        List<ActivitySession> BuildActivitySessions(
            ShiftAssignment shift,
            IReadOnlyList<Event> events
            );  
    }
}
