using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Application.Repositories
{
    public interface IEventRepository
    {
        Task AddAsync(Event evt);

        Task<List<Event>> GetByWorkDayAsync(int shiftAssignmentId);

        Task<List<Event>> GetByColleagueAsync(string colleagueId);

        Task SaveChangesAsync();
    }
}
