using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Application.ActivitySessionBuilder
{
    public interface IActivitySessionRebuilder
    {
        Task RebuildForShiftAsync(int shiftAssignmentId);
    }
}
