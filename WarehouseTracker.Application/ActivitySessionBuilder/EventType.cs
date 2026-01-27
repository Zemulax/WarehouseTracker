using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTracker.Application.ActivitySessionBuilder
{
    public enum EventType
    {
        ShiftStarted,
        CheckedIntoDepartment,
        CheckedOutOfDepartment,
        BreakStarted,
        BreakEnded,
        ShiftEnded
    }
}
