using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseTracker.Api.Models;
using WarehouseTracker.Application;

namespace WarehouseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitySessionController : ControllerBase
    {
        private readonly IActivitySessionService _activitySessionService;
        public ActivitySessionController(IActivitySessionService activitySessionService)
        {
            _activitySessionService = activitySessionService;
        }

        [HttpPost]
        public async Task<IActionResult> StartActivitySession([FromBody] ActivitySession request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _activitySessionService.StartSessionAsync(
                
                request.ColleagueId,
                request.DepartmentId,
                request.ShiftAssignmentId,
                request.SessionType,
                request.SessionStart
                );
            return Ok("Activity session logged successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> RetrieveAllActivitySessions()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var activitySessions = await _activitySessionService.RetrieveActivitySessionsAsync();
            return Ok(activitySessions);
        }

        [HttpGet("search")]
        public async Task<IActionResult> RetrieveActivitySessionsByAttribute(
            [FromQuery] int? colleagueId = null,
            [FromQuery] int? departmentId = null,
            [FromQuery] int? shiftAssignmentId = null,
            [FromQuery] string? sessionType = null,
            [FromQuery] TimeOnly? activityStart = null,
            [FromQuery] TimeOnly? activityEnd = null
            
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var activitySessions = await _activitySessionService.RetrieveActivitySessionsByAttribute(
                
                colleagueId,
                departmentId,
                shiftAssignmentId,
                sessionType,
                activityStart,
                activityEnd
                );
            return Ok(activitySessions);
        }
    }
}
