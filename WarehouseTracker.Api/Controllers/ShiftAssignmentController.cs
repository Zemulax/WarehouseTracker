using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseTracker.Api.Models;
using WarehouseTracker.Application.Services;

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
        public async Task<IActionResult> AssignShift([FromBody] ShiftAssignment request)
        {
            // Implementation for assigning a shift goes here.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _shiftAssignmentService.AssignShiftAsync(
                request.Id,
                request.ColleagueId,
                request.DepartmentId,
                request.ShiftDate,
                request.ShiftStart,
                request.ShiftEnd
                );
            return Ok($"{request.ColleagueId} Assigned to: {request.DepartmentId}, at {request.ShiftStart}");

        }

        [HttpGet]
        public async Task<IActionResult> RetrieveAllShiftAssignments()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var shiftAssignments = await _shiftAssignmentService.RetrieveShiftAssignmentsAsync();
            return Ok(shiftAssignments);
        }

        [HttpGet("search")]
        public async Task<IActionResult> RetrieveShiftAssignmentsByAttribute(
            [FromQuery] int? id = null,
            [FromQuery] string? employeeId = null,
            [FromQuery] DateTime? shiftStart = null,
            [FromQuery] DateTime? shiftEnd = null,
            [FromQuery] string? shiftType = null
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var shiftAssignments = await _shiftAssignmentService.RetrieveShiftAssignmentsByAttribute(
                id,
                employeeId,
                shiftStart,
                shiftEnd,
                shiftType
                );
            return Ok(shiftAssignments);
        }
    }
}
