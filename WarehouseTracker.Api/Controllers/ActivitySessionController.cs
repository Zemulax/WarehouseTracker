using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseTracker.Api.Models;
using WarehouseTracker.Application.Services;

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
