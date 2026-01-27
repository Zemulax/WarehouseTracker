using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Application.Services
{
    public interface IShiftAssignmentService
    {
        Task AssignShiftAsync(
            int Id,
            int employeeId,
            int departmentId,
            DateOnly shiftDate,
            TimeOnly shiftStart,
            TimeOnly shiftEnd
            );
        Task<List<ShiftAssignment>> RetrieveShiftAssignmentsAsync();
        Task<ShiftAssignment> RetrieveShiftAssignmentsByAttribute(
            int? Id,
            string? employeeId,
            DateTime? shiftStart,
            DateTime? shiftEnd,
            string? shiftType
            );

        

    }
}
