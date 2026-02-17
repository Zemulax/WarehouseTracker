using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTracker.Application.ActivitySessions
{
    /// <summary>
    /// loads shift
    /// loads events
    /// calls builder
    /// replaces sessions in DB
    /// </summary>
    public interface IActivitySessionRebuilder
    {
        Task RebuildForAsync(int shiftId);
    }
}
