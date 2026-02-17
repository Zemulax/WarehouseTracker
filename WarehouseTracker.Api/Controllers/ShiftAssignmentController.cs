using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseTracker.Api.Models;
using WarehouseTracker.Application.Services;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftAssignmentController : ControllerBase
    {

        // Implementation for ShiftAssignmentController goes here.

        private readonly IShiftAssignmentService _shiftAssignmentService;

        public ShiftAssignmentController(IShiftAssignmentService shiftAssignmentService)
        {
            _shiftAssignmentService = shiftAssignmentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ShiftAssignmentDTO shiftAssignmentDTO)
        {
            var shiftAssignmentDomain = new ShiftAssignment
            {
                ColleagueId = shiftAssignmentDTO.ColleagueId,
                
                ShiftStart = shiftAssignmentDTO.ShiftStart.ToUniversalTime(),
                ShiftEnd = shiftAssignmentDTO.ShiftEnd.ToUniversalTime(),

            };

            await _shiftAssignmentService.CreateAsync(shiftAssignmentDomain);
            return Created("shift was created", null);
        }

        
        
    }
}
