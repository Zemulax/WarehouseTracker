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
    public class ShiftAssignmentRepository : IShiftAssignmentRepository
    {
        private readonly WarehouseTrackerDbContext _dbContext;

        public ShiftAssignmentRepository(WarehouseTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(ShiftAssignment shiftAssignment)
        {
            await _dbContext.ShiftAssignments.AddAsync(shiftAssignment);
        }

        public async Task<ShiftAssignment> GetShiftAssignmentAsync(string colleagueId)
        {
          return await _dbContext.ShiftAssignments.
                 FirstAsync(s => s.ColleagueId == colleagueId);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
