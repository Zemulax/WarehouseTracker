using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Application.Repositories
{
    public interface IShiftAssignmentRepository
    {
        Task AddAsync(ShiftAssignment shiftAssignment);
        Task <ShiftAssignment>GetShiftAssignmentAsync(string colleagueId);
        Task SaveChangesAsync();
    }
}
