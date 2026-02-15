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

        public async Task<ShiftAssignment> GetShiftAssignment(string colleagueId)
        {
            var shift = await _repository.GetShiftAssignmentAsync(colleagueId);
            return shift;

        }

        public async Task AddAsync(ShiftAssignment shiftAssignment)
        {
            await _repository.AddAsync(shiftAssignment);
            await _repository.SaveChangesAsync();
        }
    }
}
