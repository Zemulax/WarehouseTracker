using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Application.Repositories;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Application.Services
{
    public class ShiftAssignmentService : IShiftAssignmentService
    {
        private readonly IShiftAssignmentRepository _repository;
        public ShiftAssignmentService(IShiftAssignmentRepository shiftAssignmentRepository) {
        
        _repository = shiftAssignmentRepository;
        }

        public async Task CreateAsync(ShiftAssignment assignment)
        {
            var shift = new ShiftAssignment
            {
                ColleagueId = assignment.ColleagueId,
                ShiftStart = assignment.ShiftStart,
                ShiftEnd = assignment.ShiftEnd,
            };

            await _repository.AddAsync(shift);
            await _repository.SaveChangesAsync();
        }

        public Task<ShiftAssignment?> GetShiftActiveShiftAsync(string colleagueId, DateTimeOffset timeStamp)
        {
            return _repository.GetActiveShiftAsync(colleagueId, timeStamp);
        }

        public async Task<List<ShiftAssignment>>GetShiftsAsync(DateTimeOffset now)
        {
            return await _repository.GetShiftsAsync(now);
        }
    }
}
