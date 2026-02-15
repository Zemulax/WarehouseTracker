using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Application.Services
{
    public interface IEventService
    {
        Task CreateEventAsync(Event request);

        Task<List<Event>> GetEventsByShiftAsync(int shiftAssignmentId);
    }
}
