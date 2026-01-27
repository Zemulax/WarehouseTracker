using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Application.Services;
using WarehouseTracker.Domain;
using WarehouseTracker.Infrastructure;

namespace WarehouseTracker.Application.Repositories
{
    public class ShiftAssignmentRepository : IShiftAssignmentService
    {

        private readonly WarehouseTrackerDbContext _dbContext;

        public ShiftAssignmentRepository(WarehouseTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // Implementation for ShiftAssignmentService goes here.
        public async Task AssignShiftAsync(int Id, int colleagueId,int departmentId, DateOnly shiftDate, 
            TimeOnly shiftStart, TimeOnly shiftEnd)
        {
            var shiftAssign = new ShiftAssignment
            {
                Id = Id,
                ColleagueId = colleagueId,
                DepartmentId = departmentId ,
                ShiftDate = shiftDate,
                ShiftStart = shiftStart,
                ShiftEnd = shiftEnd,
                
            };
            _dbContext.ShiftAssignments.Add(shiftAssign);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ShiftAssignment>> RetrieveShiftAssignmentsAsync()
        {
            var assignedShifts =_dbContext.ShiftAssignments.ToListAsync();
            return await assignedShifts;

        }

        public async Task<ShiftAssignment> RetrieveShiftAssignmentsByAttribute(int? Id, string? employeeId, 
            DateTime? shiftStart, DateTime? shiftEnd, string? shiftType)
        {
            var query = _dbContext.ShiftAssignments.AsQueryable();
            if (Id.HasValue)
            {
                query = query.Where(sa => sa.Id == Id.Value);
            }
            if (!string.IsNullOrEmpty(employeeId))
            {
                if (int.TryParse(employeeId, out int empId))
                {
                    query = query.Where(sa => sa.ColleagueId == empId);
                }
            }
            if (shiftStart.HasValue)
            {
                var startTimeOnly = TimeOnly.FromDateTime(shiftStart.Value);
                query = query.Where(sa => sa.ShiftStart >= startTimeOnly);
            }

            if (shiftEnd.HasValue)
            {
                var endTimeOnly = TimeOnly.FromDateTime(shiftEnd.Value);
                query = query.Where(sa => sa.ShiftEnd <= endTimeOnly);
            }
            
            var result = await query.FirstOrDefaultAsync();

            if (result == null)
            {
                throw new KeyNotFoundException("No matching shift assignment found.");
            }
            return result;
        }
    }
}
